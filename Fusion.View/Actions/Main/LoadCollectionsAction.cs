using System.Collections.Generic;
using Fusion.Controllers;
using Fusion.Domain;
using Fusion.View.ViewModel;
using Fusion.View.Views;
using Moon.Helpers;
using Nova.Base;

namespace Fusion.View.Actions.Main
{
	public class LoadCollectionsAction : BaseAction<MainWindow, MainViewModel>
	{
		private readonly IConfiguration _Configuration;
		private readonly ITfsController _TfsController;
		private IEnumerable<string> _Collections;

		public LoadCollectionsAction()
		{
			_Configuration = App.Get<IConfiguration>();
			_TfsController = App.Get<ITfsController>();

			Guard.NotNull(_Configuration, _TfsController);
		}

		public override bool CanExecute()
		{
			return _Configuration.IsInitialized;
		}

		public override bool Execute()
		{
			_TfsController.Connect();
			_Collections = _TfsController.PopulateCollections();

			return _Collections != null;
		}

		public override void ExecuteCompleted()
		{
			ViewModel.Collections = _Collections;
		}
	}
}
