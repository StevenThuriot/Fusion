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
