using Nova;
using Ninject;

namespace Fusion.View
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private static IKernel _Kernel;

		internal static T Get<T>()
		{
			if (_Kernel == null)
			{
				_Kernel = new StandardKernel();
				_Kernel.Load("Fusion.*.dll");
			}

			return _Kernel.Get<T>();
		}
		
		public App()
		{
			Startup += NovaFramework.Start;
			//Nova.Base.ExceptionHandler.ShowStackTrace = true;
		}
	}
}
