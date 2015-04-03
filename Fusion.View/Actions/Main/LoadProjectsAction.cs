using System.Collections.Generic;
using Fusion.Controllers;
using Fusion.View.ViewModel;
using Fusion.View.Views;

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