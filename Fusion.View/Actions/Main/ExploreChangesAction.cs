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
using System.Linq;
using Nova.Base;
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
