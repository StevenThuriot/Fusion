using System.Windows;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class CloseAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			Application.Current.Shutdown();
		}
	}
}
