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
