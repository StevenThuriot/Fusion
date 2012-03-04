using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nova.Base;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class ChangeSettingsAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			new SettingsWindow().ShowDialog();
		}
	}
}
