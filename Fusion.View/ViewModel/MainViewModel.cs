using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Moon.Extensions;
using Moon.Helpers;
using Nova.Base;
using Fusion.View.Actions.Main;
using Fusion.View.Entities;
using Fusion.View.Views;
using Fusion.Domain;
using Fusion.Controllers;

namespace Fusion.View.ViewModel
{
	public class MainViewModel : BaseViewModel<MainWindow, MainViewModel>
	{
		private CollectionViewSource _ChangesetsCollectionView;
		private ICommand _LoadProjectsAction;

		public MainViewModel()
		{
			_ShowCommitMessages = true;
		    _SearchRecursive = true;

			Configuration = App.Get<IConfiguration>();

			_Collections = new List<string>();
			_Projects = new ObservableCollection<string>();

			_Changesets = new ObservableCollection<Changeset>();
		}

		protected override void OnCreated()
		{
			View.Initialized += OnViewInitialized;
			View.Loaded += WindowLoaded;
			Application.Current.Exit += ApplicationClosing;

			_ExploreChanges = RoutedAction.New<ExploreChangesAction, MainWindow, MainViewModel>(View, this);

			SetKnownActionTypes(typeof(ChangeSettingsAction),
								typeof(ClearChangesetsAction),
								typeof(CloseAction),
								typeof(CompleteSelectedAction),
								typeof(CopyAction),
								typeof(DeleteSelectedAction),
								typeof(ExploreChangesAction),
								typeof(FetchChangesetsAction),
								typeof(LoadAction),
								typeof(SaveAction),
								typeof(ToggleCommitMessagesAction),
								typeof(ToggleDeletedAction),
								typeof(UndoSelectedAction),
								typeof(ViewAboutAction),
								typeof(LoadCollectionsAction),
								typeof(LoadProjectsAction));

			_LoadProjectsAction = ActionManager.LoadProjects as ICommand;

			Guard.NotNull(_LoadProjectsAction);
		}

		private ICommand _ExploreChanges;
		public void ChangesetsListBoxOnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			if (_ExploreChanges.CanExecute(null))
			{
				_ExploreChanges.Execute(null);
			}
		}

		public IConfiguration Configuration { get; private set; }

		private bool _ShowDeleted;
		public bool ShowDeleted
		{
			get { return _ShowDeleted; }
			set
			{
				if (SetValue(ref _ShowDeleted, value, () => ShowDeleted))
				{
					RefreshChangesets();
				}
			}
		}

		private bool _ShowCommitMessages;
		public bool ShowCommitMessages
		{
			get { return _ShowCommitMessages; }
			set { SetValue(ref _ShowCommitMessages, value, () => ShowCommitMessages); }
		}

		private string _SearchText;
		public string SearchText
		{
			get { return _SearchText; }
			set { SetValue(ref _SearchText, value, () => SearchText); }
		}

		private bool _SearchRecursive;
        public bool SearchRecursive
        {
            get { return _SearchRecursive; }
            set { SetValue(ref _SearchRecursive, value, () => SearchRecursive); }
        }
		
		private ObservableCollection<Changeset> _Changesets;
        public ObservableCollection<Changeset> Changesets
        {
            get { return _Changesets; }
			set { SetValue(ref _Changesets, value, () => Changesets); }
        }

		private IEnumerable<string> _Collections;
		public IEnumerable<string> Collections
		{
			get { return _Collections; }
			internal set { SetValue(ref _Collections, value, () => Collections); }
		}

		private ObservableCollection<string> _Projects;
		public ObservableCollection<string> Projects
		{
			get { return _Projects; }
			private set { SetValue(ref _Projects, value, () => Projects); }
		}

		private string _SelectedCollection;
		public string SelectedCollection
		{
			get { return _SelectedCollection; }
			set
			{
				if (SetValue(ref _SelectedCollection, value, () => SelectedCollection) &&
				    _LoadProjectsAction.CanExecute(SelectedCollection))
				{
					_LoadProjectsAction.Execute(SelectedCollection);
				}
			}
		}

		private string _SelectedProject;
		public string SelectedProject
		{
			get { return _SelectedProject; }
			set
			{
				if (SetValue(ref _SelectedProject, value, () => SelectedProject))
				{
					Changesets.Clear();
					RefreshChangesets();
					SearchText = string.Empty;
				}
			}
		}

		private void ApplicationClosing(object sender, ExitEventArgs e)
		{
			if (Configuration.SaveStateBeforeClosing)
			{
				Save();
			}
		}

		public void WindowLoaded(object sender, RoutedEventArgs e)
		{
			View.Loaded -= WindowLoaded;

			if (!Configuration.IsInitialized)
			{
				View.Hide();

				using (var settingsWindows = new SettingsWindow())
				{
					settingsWindows.ShowDialog();
				}

				View.Show();
			}

			var loadCollectionsAction = ActionManager.LoadCollections as ICommand;

			if (loadCollectionsAction != null && loadCollectionsAction.CanExecute(null))
			{
				loadCollectionsAction.Execute(null);
			}
		}

		public void OnViewInitialized(object sender, EventArgs e)
		{
			View.Initialized -= OnViewInitialized;
			_ChangesetsCollectionView = View.Resources["ChangesetsCollectionView"] as CollectionViewSource;

			if (_ChangesetsCollectionView != null)
			{
				_ChangesetsCollectionView.Filter += FilterChangesets;

				if (Configuration.LoadLastStateOnStartup)
				{
					try
					{
						Load();
					}
					catch (Exception exception)
					{
						ExceptionHandler.Handle(exception, Properties.Resources.ErrorOcurred, Properties.Resources.RestoreError);
					}
				}
			}
		}

		private void FilterChangesets(object sender, FilterEventArgs e)
		{
			if (ShowDeleted)
				e.Accepted = true;
			else
			{
				var changeset = e.Item as Changeset;
				e.Accepted = changeset != null && !changeset.IsDeleted;
			}
		}

		public void RefreshChangesets()
		{
			Changesets.ForEach(x => x.IsSelected = false);

			if (_ChangesetsCollectionView.View != null)
			{
				_ChangesetsCollectionView.View.Refresh();
			}
		}

		protected override void DisposeManagedResources()
		{
			View.Initialized -= OnViewInitialized;
			View.Loaded -= WindowLoaded;
			View.ChangesetsListBox.PreviewMouseDoubleClick -= ChangesetsListBoxOnPreviewMouseDoubleClick;
		}

		protected override void Save(dynamic value)
		{
			value.ShowDeleted = ShowDeleted;
			value.ShowCommitMessages = ShowCommitMessages;
			value.SearchRecursive = SearchRecursive;

			value.Collections = Collections;
			value.SelectedCollection = SelectedCollection;

			value.Projects = new List<string>(Projects);
			value.SelectedProject = SelectedProject;

			value.SearchText = SearchText;
			value.Changesets = new List<Changeset>(Changesets);
		}

		protected override void Load(dynamic value)
		{
			ShowDeleted = value.ShowDeleted;
			ShowCommitMessages = value.ShowCommitMessages;
			SearchRecursive = value.SearchRecursive;

			Collections = value.Collections;
			SelectedCollection = value.SelectedCollection;

			Projects = new ObservableCollection<string>(value.Projects);
			SelectedProject = value.SelectedProject;

			SearchText = value.SearchText;
			
			Changesets = new ObservableCollection<Changeset>(value.Changesets);
		}
	}
}
