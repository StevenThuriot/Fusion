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
