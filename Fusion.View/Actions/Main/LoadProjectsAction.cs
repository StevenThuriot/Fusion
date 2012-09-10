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
using Fusion.Controllers;
using Fusion.View.ViewModel;
using Fusion.View.Views;
using Moon.Extensions;
using Moon.Helpers;
using Nova.Base;

namespace Fusion.View.Actions.Main
{
	public class LoadProjectsAction : BaseAction<MainWindow, MainViewModel>
	{
		private readonly ITfsController _TfsController;
		private IEnumerable<string> _Projects;

		public LoadProjectsAction()
		{
			_TfsController = App.Get<ITfsController>();

			Guard.NotNull(_TfsController);
		}

		public override bool Execute()
		{
			var selection = ActionContext.GetValue<string>(RoutedAction.CommandParameter);

			if (!string.IsNullOrWhiteSpace(selection))
			{
				_TfsController.Connect();
				_Projects = _TfsController.GetProjects(selection);
			}

			return true;
		}

		public override void ExecuteCompleted()
		{
			ViewModel.Projects.Clear();

			if (_Projects == null) return;

			ViewModel.Projects.AddRange(_Projects);
		}
	}
}