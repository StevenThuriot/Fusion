namespace Fusion.View.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			ChangesetsListBox.PreviewMouseDoubleClick += ViewModel.ChangesetsListBoxOnPreviewMouseDoubleClick;
		}
	}
}
