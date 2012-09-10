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
