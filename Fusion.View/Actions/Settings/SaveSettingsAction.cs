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
using System;
using Fusion.View.Validators;
using Nova.Base;
using Nova.Validation;
using Fusion.Controllers;
using Fusion.View.Properties;
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
