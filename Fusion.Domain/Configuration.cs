using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Security;
using Moon.Helpers;

namespace Fusion.Domain
{
	internal class Configuration : IConfigurationDomain
	{
		private const string RootElement = "Fusion";
		private const string SettingsElement = "Settings";
		private const string TFSUriElement = "TFSUri";
		private const string EnableClipboardElement = "EnableClipboard";
		private const string UseLocalAccountElement = "UseLocalAccount";
		private const string UserElement = "User";
		private const string UsernameElement = "Username";
		private const string DomainElement = "Domain";
		private const string UseCustomMergeMessageElement = "UseCustomMergeMessage";
		private const string CustomMergeMessageElement = "CustomMergeMessage";
		private const string LoadLastStateOnStartupElement = "LoadLastStateOnStartup";
		private const string SaveStateBeforeClosingElement = "SaveStateBeforeClosing";
		
		private readonly FileInfo _FileInfo;

		/// <summary>
		/// Returns a value wether the configuration settings have been made or not.
		/// </summary>
		public bool IsInitialized { get; private set; }

		/// <summary>
		/// Gets or sets the URI.
		/// </summary>
		/// <value>The URI.</value>
		public Uri TFSUri { get; set; }
		
		/// <summary>
		/// Gets or sets the username
		/// </summary>
		public string Username { get; private set; }

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		public string Domain { get; private set; }

		/// <summary>
		/// Enables or disable usage of the Clipboard.
		/// </summary>
		public bool EnableClipboard { get; set; }

		/// <summary>
		/// Load the last state on startup.
		/// </summary>
		public bool LoadLastStateOnStartup { get; set; }

		/// <summary>
		/// Save the state before closing.
		/// </summary>
		public bool SaveStateBeforeClosing { get; set; }

		/// <summary>
		/// Gets or sets the network credential.
		/// </summary>
		/// <value>The network credential.</value>
		public NetworkCredential NetworkCredential { get; private set; }
		
		/// <summary>
		/// Use the current user to log in if true.
		/// </summary>
		public bool UseLocalAccount { get; set; }

		/// <summary>
		/// Indicates if the user is using a custom message.
		/// </summary>
		public bool UseCustomMergeMessage { get; set; }

		/// <summary>
		/// The custom message.
		/// </summary>
		public string CustomMergeMessage { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Configuration"/> class.
		/// </summary>
		public Configuration()
		{
			EnableClipboard = true;
			UseLocalAccount = true;

			var path = Path.Combine(GetUserDataPath(), "Fusion.config");
			_FileInfo = new FileInfo(path);

			Read();
		}

		/// <summary>
		/// Sets the custom credentials. These will be used to log in if UseCurrentUser is false
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="domain">The domain.</param>
		public void SetCustomCredentials(string username, SecureString password, string domain)
		{
			Guard.NotNullOrWhiteSpace(username, domain);

			Username = username;
			Domain = domain;

			NetworkCredential = new NetworkCredential(username, password, domain);
		}

		private void Read()
		{
			IsInitialized = false;

			if (!_FileInfo.Exists) return;

			var doc = XDocument.Load(_FileInfo.FullName);

			if (doc.Root == null) return;

			var settings = doc.Root.Descendants().First();

			var tfsElement = settings.Attribute(TFSUriElement);

			if (tfsElement == null) return;

			var uri = new Uri(tfsElement.Value);

			var clipboardElement = settings.Element(EnableClipboardElement);
			if (clipboardElement == null) return;
			bool enableClipboard;
			if (!Boolean.TryParse(clipboardElement.Value, out enableClipboard)) return;

			var loadStateElement = settings.Element(LoadLastStateOnStartupElement);
			if (loadStateElement == null) return;
			bool loadState;
			if (!Boolean.TryParse(loadStateElement.Value, out loadState)) return;

			var saveStateElement = settings.Element(SaveStateBeforeClosingElement);
			if (saveStateElement == null) return;
			bool saveState;
			if (!Boolean.TryParse(saveStateElement.Value, out saveState)) return;

			var useLocalElement = settings.Element(UseLocalAccountElement);
			if (useLocalElement == null) return;
			bool useLocal;
			if (!Boolean.TryParse(useLocalElement.Value, out useLocal)) return;
			
			TFSUri = uri;
			EnableClipboard = enableClipboard;
			UseLocalAccount = useLocal;
			LoadLastStateOnStartup = loadState;
			SaveStateBeforeClosing = saveState;

			if (useLocal)
			{
				IsInitialized = true;
			}
			else
			{
				var user = settings.Element(UserElement);

				if (user == null) return;

				var usernameElement = user.Element(UsernameElement);
				if (usernameElement == null) return;
				Username = usernameElement.Value;

				var domainElement = user.Element(DomainElement);
				if (domainElement == null) return;
				Domain = domainElement.Value;
			}

			var useCustomMessageElement = settings.Element(UseCustomMergeMessageElement);
			bool useCustomMessage;

			if (useCustomMessageElement == null || !Boolean.TryParse(useCustomMessageElement.Value, out useCustomMessage) || !useCustomMessage)
			{
				UseCustomMergeMessage = false;
			}
			else
			{
				var customMessageElement = settings.Element(CustomMergeMessageElement);
				if (customMessageElement == null || string.IsNullOrWhiteSpace(customMessageElement.Value))
				{
					UseCustomMergeMessage = false;
				}
				else
				{
					UseCustomMergeMessage = true;
					CustomMergeMessage = customMessageElement.Value;
				}

			}
		}
		
		/// <summary>
		/// Saves the current settings.
		/// </summary>
		public void Save()
		{
			var doc = new XDocument();

			var root = new XElement(RootElement);
			doc.Add(root);

			var settings = new XElement(SettingsElement);
			root.Add(settings);

			var tfsUri = new XAttribute(TFSUriElement, TFSUri);
			settings.Add(tfsUri);

			var clipboard = new XElement(EnableClipboardElement, EnableClipboard);
			settings.Add(clipboard);

			var loadState = new XElement(LoadLastStateOnStartupElement, LoadLastStateOnStartup);
			settings.Add(loadState);

			var saveState = new XElement(SaveStateBeforeClosingElement, SaveStateBeforeClosing);
			settings.Add(saveState);

			var useLocal = new XElement(UseLocalAccountElement, UseLocalAccount);
			settings.Add(useLocal);

			if (!UseLocalAccount)
			{
				var user = new XElement(UserElement);
				settings.Add(user);

				var username = new XElement(UsernameElement, Username);
				user.Add(username);

				var domain = new XElement(DomainElement, Domain);
				user.Add(domain);
			}

			var useCustomMessage = new XElement(UseCustomMergeMessageElement, UseCustomMergeMessage);
			settings.Add(useCustomMessage);

			if (UseCustomMergeMessage)
			{
				var customMessage = new XElement(CustomMergeMessageElement, CustomMergeMessage);
				settings.Add(customMessage);
			}

			doc.Save(_FileInfo.FullName);
			IsInitialized = true;
		}

		private static string GetUserDataPath()
		{
			var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var filename = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName) ?? AppDomain.CurrentDomain.FriendlyName;

			var directory = Path.Combine(path, filename);

			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			return directory;
		}
	}
}
