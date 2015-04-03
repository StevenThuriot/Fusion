using System;
using System.Net;

namespace Fusion.Domain
{
    public interface IConfiguration
	{
		/// <summary>
		/// Returns a value wether the configuration settings have been made or not.
		/// </summary>
    	bool IsInitialized { get; }

    	/// <summary>
		/// Gets the URI.
		/// </summary>
		Uri TFSUri { get; }

		/// <summary>
		/// Use the current user to log in if true.
		/// </summary>
		bool UseLocalAccount { get; }

		/// <summary>
		/// Gets the username
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Gets the domain.
		/// </summary>
		string Domain { get; }

		/// <summary>
		/// Indicates if usage of the clipboard is allowed.
		/// </summary>
		bool EnableClipboard { get; }

    	/// <summary>
    	/// Indicates if the user is using a custom message.
    	/// </summary>
    	bool UseCustomMergeMessage { get; }

		/// <summary>
		/// The custom message.
		/// </summary>
		string CustomMergeMessage { get; }
		
		/// <summary>
		/// Load the last state on startup.
		/// </summary>
		bool LoadLastStateOnStartup { get; }

		/// <summary>
		/// Save the state before closing.
		/// </summary>
		bool SaveStateBeforeClosing { get; }

		/// <summary>
		/// Gets the network credential.
		/// </summary>
		NetworkCredential NetworkCredential { get; }
    }
}