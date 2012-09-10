#region License
// 
//  Copyright 2012 Steven Thuriot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
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
