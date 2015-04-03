using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class ToggleDeletedAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.ShowDeleted = !ViewModel.ShowDeleted;
		}
	}
}
