using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class ChangeSettingsAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			using (var settingsWindow = new SettingsWindow())
			{
				settingsWindow.ShowDialog();
			}
		}
	}
}
