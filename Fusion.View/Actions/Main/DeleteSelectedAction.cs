using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class DeleteSelectedAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.Changesets.Where(changeset => changeset.IsSelected)
								.ForEach(changeset => changeset.Delete());

			ViewModel.RefreshChangesets();
		}
	}
}
