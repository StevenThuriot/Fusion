using System;
using System.Collections.Generic;
using System.Net;
using Fusion.Domain;

namespace Fusion.Infrastructure.Gateways
{
	public interface ITfsGateway : IDisposable
	{
		bool IsConnected { get; }

		Boolean Connect();
		void Disconnect();

		bool CanConnect(Uri tfsUri);
		bool CanConnect(Uri tfsUri, NetworkCredential credentials);

		IEnumerable<string> GetCollections();
		IEnumerable<string> GetProjects(String collection);

		IEnumerable<IChangeset> GetAssociatedChangesets(string collection, string project, int workItemId);
		IEnumerable<IChangeset> GetAssociatedChangesetsRecursively(string collection, string project, int workItemId);
	}
}