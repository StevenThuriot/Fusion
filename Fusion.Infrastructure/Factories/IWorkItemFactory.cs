using Moon.Factories;
using Fusion.Domain;

namespace Fusion.Infrastructure.Factories
{
	internal interface IWorkItemFactory : IFactory<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem, IWorkItem>
	{
	}
}