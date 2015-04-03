using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class ToggleCommitMessagesAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.ShowCommitMessages = !ViewModel.ShowCommitMessages;
		}
	}
}
