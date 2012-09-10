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
using Nova.Base;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.ChangeExplorer
{
	public class ToggleViewAction : BaseAction<ChangeExplorerWindow, ChangeExplorerViewModel>
	{
		public override void ExecuteCompleted()
		{
			ViewModel.ShowChangesets = !ViewModel.ShowChangesets;
			ViewModel.ShowWorkItems = !ViewModel.ShowWorkItems;

			ViewModel.ToggleText = ViewModel.ShowChangesets ? Properties.Resources.ViewWorkItems : Properties.Resources.ViewChanges;
		}
	}
}
