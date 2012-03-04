using System;
using System.Security;
using Fusion.Domain;

namespace Fusion.Controllers
{
	internal class ConfigurationController : IConfigurationController
	{
		private readonly IConfigurationDomain _Configuration;

		public ConfigurationController(IConfiguration configuration)
		{
			_Configuration = (IConfigurationDomain) configuration;
		}

		/// <summary>
		/// Sets the tfs uri
		/// </summary>
		/// <param name="uri">The uri pointing to the tfs server.</param>
		public void SetTFSUri(Uri uri)
		{
			_Configuration.TFSUri = uri;
		}

		/// <summary>
		/// Allows usage of the clipboard.
		/// </summary>
		/// <param name="allow">True if the program can use the system clipboard.</param>
		public void AllowClipboardUsage(bool allow)
		{
			_Configuration.EnableClipboard = allow;
		}

		/// <summary>
		/// Use the local account to access tfs.
		/// </summary>
		/// <param name="useLocal">True if the program should use the local account to access tfs, rather than the custom credentials.</param>
		public void UseLocalAccount(bool useLocal)
		{
			_Configuration.UseLocalAccount = useLocal;
		}

		/// <summary>
		/// Saves the current settings.
		/// </summary>
		public void Save()
		{
			_Configuration.Save();
		}

		/// <summary>
		/// Sets the custom credentials. These will be used to log in if UseCurrentUser is false
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="domain">The domain.</param>
        public void SetCustomCredentials(string username, SecureString password, string domain)
		{
			_Configuration.SetCustomCredentials(username, password, domain);
		}

		/// <summary>
		/// Disables the custom message and uses the default.
		/// </summary>
		public void DisableCustomMergeMessage()
		{
			_Configuration.UseCustomMergeMessage = false;
		}

		/// <summary>
		/// Enables the custom message.
		/// </summary>
		/// <param name="message">The message to use.</param>
		public void EnableCustomMergeMessage(string message)
		{
			_Configuration.CustomMergeMessage = message;
			_Configuration.UseCustomMergeMessage = true;
		}

		/// <summary>
		/// Loads the last state on startup.
		/// </summary>
		public void LoadLastStateOnStartup()
		{
			_Configuration.LoadLastStateOnStartup = true;
		}

		/// <summary>
		/// Disables loading the last state on startup.
		/// </summary>
		public void DisableLoadLastStateOnStartup()
		{
			_Configuration.LoadLastStateOnStartup = false;
		}

		/// <summary>
		/// Saves the state before closing.
		/// </summary>
		public void SaveStateBeforeClosing()
		{
			_Configuration.SaveStateBeforeClosing = true;
		}

		/// <summary>
		/// Disables saving the state before closing.
		/// </summary>
		public void DisableSaveStateBeforeClosing()
		{
			_Configuration.SaveStateBeforeClosing = false;
		}
	}
}
