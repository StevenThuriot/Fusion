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
using System.Security;

namespace Fusion.Domain
{
	public interface IConfigurationDomain : IConfiguration
	{

		/// <summary>
		/// Gets the URI.
		/// </summary>
		new Uri TFSUri { get; set; }

		/// <summary>
		/// Use the current user to log in if true.
		/// </summary>
		new bool UseLocalAccount { get; set; }

		/// <summary>
		/// Indicates if usage of the clipboard is allowed.
		/// </summary>
		new bool EnableClipboard { get; set; }

		/// <summary>
		/// Indicates if the user is using a custom message.
		/// </summary>
		new bool UseCustomMergeMessage { get; set; }

		/// <summary>
		/// The custom message.
		/// </summary>
		new string CustomMergeMessage { get; set; }

		/// <summary>
		/// Load the last state on startup.
		/// </summary>
		new bool LoadLastStateOnStartup { get; set; }

		/// <summary>
		/// Save the state before closing.
		/// </summary>
		new bool SaveStateBeforeClosing { get; set; }

		/// <summary>
		/// Saves the current settings.
		/// </summary>
		void Save();

		/// <summary>
		/// Sets the custom credentials. These will be used to log in if UseCurrentUser is false
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="domain">The domain.</param>
        void SetCustomCredentials(string username, SecureString password, string domain);
	}
}
