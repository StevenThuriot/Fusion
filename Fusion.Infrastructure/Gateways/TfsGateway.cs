#region License
// 
//  Copyright 2012 Steven Thuriot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Moon.Extensions;
using Moon.Helpers;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.VersionControl.Client;
using Fusion.Domain;
using Fusion.Infrastructure.Factories;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem;

namespace Fusion.Infrastructure.Gateways
{
	public class TfsGateway : ITfsGateway
	{
        private readonly List<CatalogNode> _Collections;
		private readonly List<TfsTeamProjectCollection> _TeamProjectCollections;

		private TfsConfigurationServer _Server;
		private readonly IChangesetFactory _ChangesetFactory;
	    private readonly IConfiguration _Configuration;

		public TeamFoundationIdentity AuthorizedIdentity
		{
			get { return _Server.AuthorizedIdentity; }
		}

    	public bool IsConnected { get; private set; }

		public TfsGateway(IChangesetFactory changesetFactory, IConfiguration configuration)
		{
		    _ChangesetFactory = changesetFactory;
		    _Configuration = configuration;
			_Collections = new List<CatalogNode>();
			_TeamProjectCollections = new List<TfsTeamProjectCollection>();
		}

		~TfsGateway()
		{
			Dispose(false);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_Server != null)
				{
					_Server.Dispose();
					_Server = null;
				}
			}
		}

		public Boolean Connect()
        {
			//Connect or reconnect if needed.
			if (!IsConnected || _Server == null || _Server.Uri != _Configuration.TFSUri)
			{
				Disconnect(); //Disconnect first if needed.

				_Server = _Configuration.UseLocalAccount || _Configuration.NetworkCredential == null
				          	? new TfsConfigurationServer(_Configuration.TFSUri)
				          	: new TfsConfigurationServer(_Configuration.TFSUri, _Configuration.NetworkCredential);

				_Server.EnsureAuthenticated();
				IsConnected = true;
			}

			return IsConnected;
        }

		public void Disconnect()
		{
			IsConnected = false;
			
			if (_Server == null) return;

			_Server.Dispose();
			_Server = null;
		}

		public bool CanConnect(Uri tfsUri)
		{
			Guard.NotNull(tfsUri);

			bool canConnect;
			using (var server = new TfsConfigurationServer(tfsUri))
			{
				try
				{
					server.Authenticate();
					canConnect = server.HasAuthenticated;
				}
				catch (Exception)
				{
					canConnect = false;
				}
			}

			return canConnect;
		}

		public bool CanConnect(Uri tfsUri, NetworkCredential credentials)
		{
			Guard.NotNull(tfsUri, credentials);

			bool canConnect;
			using (var server = new TfsConfigurationServer(tfsUri, credentials))
			{
				try
				{
					server.EnsureAuthenticated();
					canConnect = true;
				}
				catch (Exception)
				{
					canConnect = false;
				}
			}

			return canConnect;
		}

		private CatalogNode GetCollection(string collection)
		{
			if (!_Collections.Any())
			{
				GetCollections();
			}

			return _Collections.First(x => x.Resource.DisplayName == collection);
		}


        public IEnumerable<string> GetCollections()
		{
			Guard.True(IsConnected);

        	_Collections.Clear();

        	// Get the catalog of team project collections
        	ReadOnlyCollection<CatalogNode> collectionNodes = _Server.CatalogNode.QueryChildren(
        		new[] { CatalogResourceTypes.ProjectCollection },
        		false, CatalogQueryOptions.None);

        	_Collections.AddRange(collectionNodes);

			return _Collections.Select(x => x.Resource.DisplayName)
							   .OrderBy(x => x)
							   .AsReadOnly();
        }

        private TfsTeamProjectCollection GetTeamProjectCollection(string collection)
        {
			Guard.NotNullOrWhiteSpace(collection);

            return GetTeamProjectCollection(GetCollection(collection));
        }

        private TfsTeamProjectCollection GetTeamProjectCollection(CatalogNode node)
        {
			foreach (TfsTeamProjectCollection projectCollection in _TeamProjectCollections.Where(x => x.CatalogNode.Resource.DisplayName == node.Resource.DisplayName))
        	{
        		return projectCollection;
        	}

			Guid collectionId = GetCollectionGuid(node);
        	var collection = _Server.GetTeamProjectCollection(collectionId);
			_TeamProjectCollections.Add(collection);

            return collection;
        }

        private static Guid GetCollectionGuid(CatalogNode node)
        {
            return new Guid(node.Resource.Properties["InstanceId"]);
        }

        public IEnumerable<String> GetProjects(string collection)
        {
            Guard.True(IsConnected);
			Guard.NotNullOrWhiteSpace(collection);

        	CatalogNode collectionNode = GetCollection(collection);

        	// Get a catalog of team projects for the collection
        	ReadOnlyCollection<CatalogNode> projectNodes = collectionNode.QueryChildren(
        		new[] { CatalogResourceTypes.TeamProject },
        		false, CatalogQueryOptions.None);

        	// List the team projects in the collection
        	var list = projectNodes.Select(projectNode => projectNode.Resource.DisplayName)
								   .OrderBy(x => x);

        	return list.AsReadOnly();
        }
		
        public WorkItem GetWorkItem(string collection, string project, int id)
		{
			Guard.True(IsConnected);
			Guard.NotNullOrWhiteSpace(collection, project);

			TfsTeamProjectCollection tpc = GetTeamProjectCollection(collection);

			return GetWorkItem(tpc, project, id);
		}

		private static WorkItem GetWorkItem(TfsTeamProjectCollection teamProjectCollection, string project, int id)
		{
			var workItemStore = new WorkItemStore(teamProjectCollection);
			WorkItemCollection workItemCollection = workItemStore.Query
				(
					"SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
					"FROM WorkItems WHERE [System.TeamProject] = '" + project + "'  AND  [System.Id] = " + id
				);


			return workItemCollection.Count > 0 ? workItemCollection[0] : null;
		}

		public IEnumerable<IChangeset> GetAssociatedChangesets(string collection, string project, int workItemId)
		{
			Guard.True(IsConnected);
			Guard.NotNullOrWhiteSpace(collection, project);
			
			var tpc = GetTeamProjectCollection(collection);
			var vcs = (VersionControlServer)tpc.GetService(typeof(VersionControlServer));
			var workItem = GetWorkItem(tpc, project, workItemId);

			return GetAssociatedChangesets(vcs, workItem);
		}

		private IEnumerable<IChangeset> GetAssociatedChangesets(VersionControlServer vcs, WorkItem workItem)
		{
			if (workItem != null)
			{
				return workItem.Links.OfType<ExternalLink>()
									 .Where(x => String.Equals(LinkingUtilities.DecodeUri(x.LinkedArtifactUri).ArtifactType, "Changeset", StringComparison.Ordinal))
									 .Select(x => vcs.ArtifactProvider.GetChangeset(new Uri(x.LinkedArtifactUri)))
									 .Select(_ChangesetFactory.Create)
									 .OrderBy(x => x)
									 .AsReadOnly();
			}

			return Enumerable.Empty<IChangeset>();
		}

		public IEnumerable<IChangeset> GetAssociatedChangesetsRecursively(string collection, string project, int workItemId)
		{
			Guard.True(IsConnected);
			Guard.NotNullOrWhiteSpace(collection, project);

			var workItems = new Dictionary<int, WorkItem>();

			var tpc = GetTeamProjectCollection(collection);
			var vcs = (VersionControlServer)tpc.GetService(typeof(VersionControlServer));
			
			var headWorkItem = GetWorkItem(tpc, project, workItemId);

			if (headWorkItem != null)
			{
				workItems.Add(workItemId, headWorkItem);
				FindRecursive(headWorkItem, tpc, project, workItems);

				return workItems.Values
								.SelectMany(x => GetAssociatedChangesets(vcs, x))
								.Distinct()
								.OrderBy(x => x)
								.AsReadOnly();
			}

			return Enumerable.Empty<IChangeset>();
		}


		private static void FindRecursive(WorkItem item, TfsTeamProjectCollection tpc, string project, IDictionary<int, WorkItem> dict)
		{
			if (item == null || item.WorkItemLinks == null)
				return;

			var tempValues = new List<WorkItem>();

			var workItemIds = item.WorkItemLinks
								  .OfType<WorkItemLink>()
								  .Select(x => x.TargetId);

			foreach (var id in workItemIds)
			{
				if (dict.ContainsKey(id)) continue;

				var value = GetWorkItem(tpc, project, id);
				tempValues.Add(value);
				dict.Add(id, value);
			}

			foreach (var value in tempValues)
			{
				FindRecursive(value, tpc, project, dict);
			}
		}
    }
}