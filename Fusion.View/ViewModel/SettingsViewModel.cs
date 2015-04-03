using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using Fusion.Domain;
using Fusion.View.Actions.Settings;
using Fusion.View.Properties;
using Fusion.View.Views;

namespace Fusion.View.ViewModel
{
    public class SettingsViewModel : BaseViewModel<SettingsWindow, SettingsViewModel>
	{
		private readonly IConfiguration _Configuration;

    	private bool _PreviousUsageOfMergeMessage;
    	private string _PreviousMergeMessage;

    	public SettingsViewModel()
    	{
    		_Configuration = App.Get<IConfiguration>();
    	}
		
    	protected override void OnCreated()
    	{
    		base.OnCreated();
			
			_UseLocalAccount = _Configuration.UseLocalAccount;
			_EnableClipboard = _Configuration.EnableClipboard;
			_TFSUriString = Convert.ToString(_Configuration.TFSUri);
			_UseCustomMergeMessage = _Configuration.UseCustomMergeMessage;
			_CustomMergeMessage = _UseCustomMergeMessage ? _Configuration.CustomMergeMessage : Resources.MergeMessage;

			_PreviousUsageOfMergeMessage = _UseCustomMergeMessage;
			_PreviousMergeMessage = _CustomMergeMessage;

			_LoadLastSavedStateAtStartup = _Configuration.LoadLastStateOnStartup;
			_SaveBeforeClose = _Configuration.SaveStateBeforeClosing;

			if (!_UseLocalAccount)
			{
				_Username = _Configuration.Username;
				_Domain = _Configuration.Domain;
			}

			View.Closing += OnClosing;

    		SetKnownActionTypes(typeof (SaveSettingsAction));
    	}

    	public bool IsInitialized
    	{
    		get { return _Configuration.IsInitialized; }
    	}

    	private bool _UseLocalAccount;
    	public bool UseLocalAccount
    	{
    		get { return _UseLocalAccount; }
    		set
    		{
				if (_UseLocalAccount != value)
				{
					_UseLocalAccount = value;
					OnPropertyChanged(() => UseLocalAccount);

					if (_UseLocalAccount)
						ClearTFSSettings();
					else
						ResetTFSSettings();
    			}
    		}
    	}

		private void ClearTFSSettings()
		{
			Username = string.Empty;
			View.PasswordControl.Clear();
			Domain = string.Empty;
		}

		private void ResetTFSSettings()
		{
			Username = _Configuration.Username;
			Domain = _Configuration.Domain;
		}

    	private bool _EnableClipboard;
    	public bool EnableClipboard
    	{
			get { return _EnableClipboard; }
			set
			{
				if (SetValue(ref _EnableClipboard, value, () => EnableClipboard))
				{
					if (_EnableClipboard)
					{
						UseCustomMergeMessage = _PreviousUsageOfMergeMessage;
						CustomMergeMessage = _PreviousMergeMessage;
					}
					else
					{
						UseCustomMergeMessage = false;
					}
				}
			}
    	}

    	private string _TFSUriString;
    	public string TFSUriString
    	{
    		get { return _TFSUriString; }
    		set { SetValue(ref _TFSUriString, value, () => TFSUriString); }
    	}

    	private string _Username;
    	public string Username
    	{
    		get { return _Username; }
			set { SetValue(ref _Username, value, () => Username); }
    	}

    	public SecureString Password
    	{
			get { return View.PasswordControl.SecurePassword; }
    	}

    	private string _Domain;
    	public string Domain
    	{
    		get { return _Domain; }
    		set { SetValue(ref _Domain, value, () => Domain); }
    	}

    	private bool _UseCustomMergeMessage;
    	public bool UseCustomMergeMessage
    	{
    		get { return _UseCustomMergeMessage; }
			set
			{
				if (SetValue(ref _UseCustomMergeMessage, value, () => UseCustomMergeMessage))
				{
					if (!_UseCustomMergeMessage)
					{
						CustomMergeMessage = Resources.MergeMessage;
					}
				}
			}
    	}

    	private bool _SaveBeforeClose;
    	public bool SaveBeforeClose
    	{
    		get { return _SaveBeforeClose; }
    		set { SetValue(ref _SaveBeforeClose, value, () => SaveBeforeClose); }
    	}

    	private bool _LoadLastSavedStateAtStartup;
    	public bool LoadLastSavedStateAtStartup
    	{
    		get { return _LoadLastSavedStateAtStartup; }
    		set { SetValue(ref _LoadLastSavedStateAtStartup, value, () => LoadLastSavedStateAtStartup); }
    	}

    	private string _CustomMergeMessage;
    	public string CustomMergeMessage
    	{
    		get { return _CustomMergeMessage; }
    		set { SetValue(ref _CustomMergeMessage, value, () => CustomMergeMessage); }
    	}

		public void OnAfterSaveSettings(ActionContext context)
		{
			if (!context.IsSuccessful) return;

			var message = context.GetValue<string>("SettingsSaved");
			MessageBox.Show(message, Resources.SettingsLabel, MessageBoxButton.OK, MessageBoxImage.Information);

			View.DialogResult = true;
		}

    	public void OnClosing(object sender, CancelEventArgs e)
    	{
			if (IsInitialized)
			{
				View.Closing -= OnClosing;
				return;
			}

    		var result = MessageBox.Show(Resources.CloseInvalidSettings, Resources.SettingsLabel, MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				View.Closing -= OnClosing;
				Application.Current.Shutdown();
			}
			else
			{
				e.Cancel = true;
			}
    	}
	}
}
