using System;
using Fusion.Controllers;
using Fusion.View.Properties;
using Fusion.View.Validators;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Settings
{
	public class SaveSettingsAction : BaseAction<SettingsWindow, SettingsViewModel>
	{
		private readonly IConfigurationController _ConfigurationController;
		private readonly ITfsController _TFSController;

		public SaveSettingsAction()
		{
			_ConfigurationController = App.Get<IConfigurationController>();
			_TFSController = App.Get<ITfsController>();
		}

		public override void Validate(ValidationResults results)
		{
			var validator = new SaveSettingsValidator(results, ActionContext, _TFSController);
			validator.Validate(ViewModel);
		}

		public override bool Execute()
		{
			var tfsUri = ActionContext.GetValue<Uri>("TfsUri");

			_ConfigurationController.SetTFSUri(tfsUri);
			_ConfigurationController.UseLocalAccount(ViewModel.UseLocalAccount);
			_ConfigurationController.AllowClipboardUsage(ViewModel.EnableClipboard);

			if (!ViewModel.UseLocalAccount)
			{
				_ConfigurationController.SetCustomCredentials(ViewModel.Username, ViewModel.Password, ViewModel.Domain);
			}

			if (ViewModel.UseCustomMergeMessage)
			{
				_ConfigurationController.EnableCustomMergeMessage(ViewModel.CustomMergeMessage);
			}
			else
			{
				_ConfigurationController.DisableCustomMergeMessage();
			}

			if (ViewModel.LoadLastSavedStateAtStartup)
			{
				_ConfigurationController.LoadLastStateOnStartup();
			}
			else
			{
				_ConfigurationController.DisableLoadLastStateOnStartup();
			}

			if (ViewModel.SaveBeforeClose)
			{
				_ConfigurationController.SaveStateBeforeClosing();
			}
			else
			{
				_ConfigurationController.DisableSaveStateBeforeClosing();
			}

			_ConfigurationController.Save();

			return true;
		}

		public override void ExecuteCompleted()
		{
			var message = Resources.SettingsSavedMessage;

			if (!ViewModel.UseLocalAccount)
			{
				message = string.Concat(message, Environment.NewLine, Environment.NewLine, Resources.PasswordWillNotBeSaved);
			}

			ActionContext.Add("SettingsSaved", message);
		}
	}
}
