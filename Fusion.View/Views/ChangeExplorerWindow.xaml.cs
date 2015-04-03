using Fusion.Domain;

namespace Fusion.View.Views
{
	/// <summary>
	/// Interaction logic for ChangeExplorerWindow.xaml
	/// </summary>
	public partial class ChangeExplorerWindow
	{
		public ChangeExplorerWindow(IChangeset changeset)
		{
			InitializeComponent();

			ViewModel.Init(changeset);
		}
	}
}
