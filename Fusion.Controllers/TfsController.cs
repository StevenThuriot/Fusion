using System;
using System.Collections.Generic;
using System.Net;
using Fusion.Domain;
using Fusion.Infrastructure.Gateways;

namespace Fusion.Controllers
{
    internal class TfsController : ITfsController
    {
    	private readonly ITfsGateway _Gateway;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Fusion.Controllers.TfsController"/> class.
		/// </summary>
		/// <param name="gateway">The tfs gateway</param>
    	public TfsController(ITfsGateway gateway)
    	{
    		_Gateway = gateway;
    	}

    	public bool Connect()
        {
            return _Gateway.Connect();
        }

    	public void Disconnect()
        {
			_Gateway.Disconnect();
        }

    	public bool IsConnected
    	{
			get { return _Gateway.IsConnected; }
    	}

    	public bool CanConnect(Uri tfsUri)
    	{
    		return _Gateway.CanConnect(tfsUri);
    	}

    	public bool CanConnect(Uri tfsUri, NetworkCredential credentials)
		{
			return _Gateway.CanConnect(tfsUri, credentials);
    	}

    	public IEnumerable<String> PopulateCollections()
    	{
    		return _Gateway.GetCollections();
    	}

		public IEnumerable<String> GetProjects(String collection)
		{
			return _Gateway.GetProjects(collection);
		}

    	public IEnumerable<IChangeset> GetAssociatedChangesets(string collection, string project, int workItemId)
    	{
    		return _Gateway.GetAssociatedChangesets(collection, project, workItemId);
    	}

    	public IEnumerable<IChangeset> GetAssociatedChangesetsRecursively(string collection, string project, int workItemId)
    	{
    		return _Gateway.GetAssociatedChangesetsRecursively(collection, project, workItemId);
    	}
    }
}
