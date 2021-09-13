using System.Windows;
using FolderChecker.ViewModel;

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy EditSimpleText.xaml
    /// </summary>
    public partial class EditSimpleTextWindow : Window
    {
        public EditSimpleTextViewModel editSimpleTextViewModel = new EditSimpleTextViewModel();
        public EditSimpleTextWindow(string propertyName, string thingToChange)
        {
            editSimpleTextViewModel.MyPropertyName = propertyName;
            editSimpleTextViewModel.MyOldText = thingToChange;
            InitializeComponent();
            DataContext = editSimpleTextViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string GetNewText()
        {
            return editSimpleTextViewModel.MyNewText;
        }
    }
}
