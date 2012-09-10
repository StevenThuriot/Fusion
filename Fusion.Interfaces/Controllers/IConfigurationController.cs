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

namespace Fusion.Controllers
{
	public interface IConfigurationController
	{
		/// <summary>
		/// Sets the tfs uri
		/// </summary>
		/// <param name="uri">The uri pointing to the tfs server.</param>
		void SetTFSUri(Uri uri);

		/// <summary>
		/// Allows usage of the clipboard.
		/// </summary>
		/// <param name="allow">True if the program can use the system clipboard.</param>
		void AllowClipboardUsage(bool allow);

		/// <summary>
		/// Use the local account to access tfs.
		/// </summary>
		/// <param name="useLocal">True if the program should use the local account to access tfs, rather than the custom credentials.</param>
		void UseLocalAccount(bool useLocal);


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

		/// <summary>
		/// Disables the custom message and uses the default.
		/// </summary>
		void DisableCustomMergeMessage();

		/// <summary>
		/// Enables the custom message.
		/// </summary>
		/// <param name="message">The message to use.</param>
		void EnableCustomMergeMessage(string message);

		/// <summary>
		/// Loads the last state on startup.
		/// </summary>
		void LoadLastStateOnStartup();

		/// <summary>
		/// Disables loading the last state on startup.
		/// </summary>
		void DisableLoadLastStateOnStartup();

		/// <summary>
		/// Saves the state before closing.
		/// </summary>
		void SaveStateBeforeClosing();

		/// <summary>
		/// Disables saving the state before closing.
		/// </summary>
		void DisableSaveStateBeforeClosing();
	}
}
