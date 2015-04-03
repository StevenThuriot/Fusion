using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.ChangeExplorer
{
	public class ToggleViewAction : BaseAction<ChangeExplorerWindow, ChangeExplorerViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.ShowChangesets = !ViewModel.ShowChangesets;
			ViewModel.ShowWorkItems = !ViewModel.ShowWorkItems;

			ViewModel.ToggleText = ViewModel.ShowChangesets ? Properties.Resources.ViewWorkItems : Properties.Resources.ViewChanges;
		}
	}
}
