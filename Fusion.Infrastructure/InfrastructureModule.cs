using Fusion.Infrastructure.Factories;
using Fusion.Infrastructure.Gateways;

namespace Fusion.Infrastructure
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
		{
			Bind<IChangeFactory>().To<ChangeFactory>().InSingletonScope();
			Bind<IWorkItemFactory>().To<WorkItemFactory>().InSingletonScope();
            Bind<IChangesetFactory>().To<ChangesetFactory>().InSingletonScope();
            Bind<ITfsGateway>().To<TfsGateway>().InSingletonScope();
        }
    }
}
