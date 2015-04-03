using Fusion.View.Entities;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class ExploreChangesAction : BaseAction<MainWindow, MainViewModel>
	{
		private Changeset _SelectedChangeset;

		public override bool Execute()
		{
			_SelectedChangeset = ViewModel.Changesets.FirstOrDefault(x => x.IsSelected);

			return _SelectedChangeset != null;
		}

		public override void ExecuteCompleted()
		{
			new ChangeExplorerWindow(_SelectedChangeset).ShowDialog();
		}
	}
}
