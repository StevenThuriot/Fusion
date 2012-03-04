using Ninject.Modules;

namespace Fusion.Domain
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<Configuration>().InSingletonScope();
        	Bind<IWorkItem>().To<WorkItem>();
            Bind<IChangeset>().To<Changeset>();
        }
    }
}
