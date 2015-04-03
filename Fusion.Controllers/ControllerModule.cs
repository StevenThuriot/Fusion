namespace Fusion.Controllers
{
    public class ControllerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITfsController>().To<TfsController>().InSingletonScope();
            Bind<IConfigurationController>().To<ConfigurationController>().InSingletonScope();
        }
    }
}
