using System.Linq;
using Moon.Extensions;
using Nova.Base;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class UndoSelectedAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.Changesets.Where(changeset => changeset.IsSelected)
								.ForEach(changeset => changeset.Reset());

			ViewModel.RefreshChangesets();
		}
	}
}
