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
using Moon.Factories;
using Fusion.Domain;
using ServerWorkItem = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem;
using ClientWorkItem = Fusion.Domain.WorkItem;

namespace Fusion.Infrastructure.Factories
{
	internal class WorkItemFactory : BaseFactory<ServerWorkItem, IWorkItem>, IWorkItemFactory
	{
		public override IWorkItem Create(ServerWorkItem workItem)
		{
			var result = new ClientWorkItem(workItem.Id, 
											workItem.Description,
											workItem.Title,
											workItem.State,
			                                workItem.CreatedBy,
											workItem.CreatedDate,
											workItem.AreaPath,
											workItem.IterationPath);

			return result;
		}
	}
}
