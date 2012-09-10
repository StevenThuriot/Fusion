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
using System.Net;
using Fusion.Controllers;
using Fusion.View.Properties;
using Fusion.View.ViewModel;
using Moon.Extensions;
using Nova.Base;
using Nova.Validation;

namespace Fusion.View.Validators
{
	public class SaveSettingsValidator : BaseValidator<SettingsViewModel>
	{
		private readonly ITfsController _TFSController;
		private Uri _TfsUri;

		public SaveSettingsValidator(ValidationResults results, ActionContext actionContext, ITfsController tfsController) 
			: base(results, actionContext)
		{
			_TFSController = tfsController;
		}

		public override void Validate(SettingsViewModel entity)
		{
			ValidateMergeMessage(entity);
			ValidateTFSUri(entity);

			if (!entity.UseLocalAccount)
				ValidateCustomAccount(entity);

			if (!IsValid) return; //Don't validate the rest if the model is already invalid.

			ValidateCredentials(entity);

			if (!IsValid) return; //Don't disconnect if the model is invalid.

			_TFSController.Disconnect();
		}



		private void ValidateCredentials(SettingsViewModel entity)
		{
			if (entity.UseLocalAccount)
			{
				if (!_TFSController.CanConnect(_TfsUri))
				{
					Add(Fields.LocalAccount, Resources.TFSConnectError);
				}
			}
			else
			{
				var credentials = new NetworkCredential(entity.Username, entity.Password, entity.Domain);
				if (!_TFSController.CanConnect(_TfsUri, credentials))
				{
					Add(Fields.User, Resources.TFSConnectError);
					Add(Fields.Password, Resources.TFSConnectError);
					Add(Fields.Domain, Resources.TFSConnectError);
				}
			}
		}

		private void ValidateCustomAccount(SettingsViewModel entity)
		{
			if (string.IsNullOrWhiteSpace(entity.Username))
			{
				AddRequired(Fields.User);
			}

			if (entity.Password.Length == 0)
			{
				AddRequired(Fields.Password);
			}

			if (string.IsNullOrWhiteSpace(entity.Domain))
			{
				AddRequired(Fields.Domain);
			}
		}

		private void ValidateTFSUri(SettingsViewModel entity)
		{
			if (string.IsNullOrEmpty(entity.TFSUriString))
			{
				AddRequired(Fields.TFSURI);
				return;
			}

			if (Uri.TryCreate(entity.TFSUriString, UriKind.Absolute, out _TfsUri))
			{
				ActionContext.Add("TfsUri", _TfsUri);
			}
			else
			{
				var message = Resources.UriParseError.FormatWith(entity.TFSUriString);
				Add(Fields.TFSURI, message);
			}
		}

		private void ValidateMergeMessage(SettingsViewModel entity)
		{
			if (!entity.UseCustomMergeMessage) return;

			if (string.IsNullOrWhiteSpace(entity.CustomMergeMessage))
			{
				AddRequired(Fields.CustomMergeMessage);
			}
		}
	}
}
