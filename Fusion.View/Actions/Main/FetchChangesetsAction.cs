using System.Collections.Generic;
using System.Linq;
using Fusion.View.Validators;
using Moon.WPF.Extensions;
using Nova.Base;
using Nova.Validation;
using Fusion.Controllers;
using Fusion.Domain;
using Fusion.View.Entities;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class FetchChangesetsAction : BaseAction<MainWindow, MainViewModel>
	{
		private readonly ITfsController _TfsController;
		private IEnumerable<IChangeset> _Changesets;

		public FetchChangesetsAction()
		{
			_TfsController = App.Get<ITfsController>();
		}

		public override void Validate(ValidationResults results)
		{
			var validator = new FetchChangesetsValidator(results, ActionContext);
			validator.Validate(ViewModel);
		}

		public override bool CanExecute()
		{
			return !string.IsNullOrEmpty(ViewModel.SearchText);
		}

		public override bool Execute()
		{
			_TfsController.Connect();

			var id = ActionContext.GetValue<int>("ID");

		    _Changesets = ViewModel.SearchRecursive
		                          ? _TfsController.GetAssociatedChangesetsRecursively(ViewModel.SelectedCollection, ViewModel.SelectedProject, id)
								  : _TfsController.GetAssociatedChangesets(ViewModel.SelectedCollection, ViewModel.SelectedProject, id);

			return true;
		}

		public override void ExecuteCompleted()
		{
			ViewModel.Changesets = _Changesets.Select(x => new Changeset(x)).ToObservable();
		}
	}
}
