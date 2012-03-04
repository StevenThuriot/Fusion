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
