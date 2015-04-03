using System.Collections.Generic;
using Fusion.Domain;
using Fusion.View.Actions.ChangeExplorer;
using Fusion.View.Views;
using RESX = Fusion.View.Properties.Resources;

namespace Fusion.View.ViewModel
{
	public class ChangeExplorerViewModel
		: BaseViewModel<ChangeExplorerWindow, ChangeExplorerViewModel>
	{
		public ChangeExplorerViewModel()
		{
			_ShowChangesets = true;
			_ShowWorkItems = false;
			_ToggleText = Properties.Resources.ViewWorkItems;
		}

		protected override void OnCreated()
		{
			SetKnownActionTypes(typeof(ToggleViewAction));
		}

		public void Init(IChangeset changeset)
		{
			TitleMessage = RESX.ChangesetByHeader.FormatWith(changeset.ID, changeset.Committer);
			Comment = changeset.Comment;

			Changes = changeset.Changes;
			WorkItems = changeset.Workitems;
		}
		
		private IEnumerable<IChange> _Changes;
		public IEnumerable<IChange> Changes
		{
			get { return _Changes; }
			set { SetValue(ref _Changes, value, () => Changes); }
		}

		private string _TitleMessage;
		public string TitleMessage
		{
			get { return _TitleMessage; }
			set { SetValue(ref _TitleMessage, value, () =>  TitleMessage); }
		}

		private string _Comment;

		public string Comment
		{
			get { return _Comment; }
			set { SetValue(ref _Comment, value, () => Comment); }
		}

		private bool _ShowChangesets;
		public bool ShowChangesets
		{
			get { return _ShowChangesets; }
			set { SetValue(ref _ShowChangesets, value, () => ShowChangesets); }
		}

		private bool _ShowWorkItems;
		public bool ShowWorkItems
		{
			get { return _ShowWorkItems; }
			set { SetValue(ref _ShowWorkItems, value, () => ShowWorkItems); }
		}

		private string _ToggleText;
		public string ToggleText
		{
			get { return _ToggleText; }
			set { SetValue(ref _ToggleText, value, () => ToggleText); }
		}

		private IEnumerable<IWorkItem> _WorkItems;
		public IEnumerable<IWorkItem> WorkItems
		{
			get { return _WorkItems; }
			set { SetValue(ref _WorkItems, value, () => WorkItems); }
		}
	}
}
