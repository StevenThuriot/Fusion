using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class SaveAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.Save();
		}
	}
}
