using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class ViewAboutAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			new AboutWindow().ShowDialog();
		}
	}
}
