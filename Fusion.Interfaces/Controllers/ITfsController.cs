using System;
using System.Collections.Generic;
using System.Net;
using Fusion.Domain;

namespace Fusion.Controllers
{
    public interface ITfsController
    {
		bool Connect();
    	void Disconnect();

    	bool IsConnected { get; }
    	bool CanConnect(Uri tfsUri);
    	bool CanConnect(Uri tfsUri, NetworkCredential credentials);

    	IEnumerable<string> PopulateCollections();
		IEnumerable<string> GetProjects(String collection);
		IEnumerable<IChangeset> GetAssociatedChangesets(string collection, string project, int workItemId);
		IEnumerable<IChangeset> GetAssociatedChangesetsRecursively(string collection, string project, int workItemId);
    }
}