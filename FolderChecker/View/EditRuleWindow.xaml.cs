using System.Windows;
using FolderChecker.ViewModel;
using FolderChecker.Model;

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy EditRuleWindow.xaml
    /// </summary>
    public partial class EditRuleWindow : Window
    {
        public EditRuleWindowViewModel editRuleWindowViewModel = new EditRuleWindowViewModel();
        public EditRuleWindow(Rule editableRule)
        {
            editRuleWindowViewModel.MyRuleToEdit = editableRule;
            InitializeComponent();
            DataContext = editRuleWindowViewModel;
            this.Closed += editRuleWindowViewModel.onClosed;
        }
        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            editRuleWindowViewModel.EditName();
        }
        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            editRuleWindowViewModel.EditChoosenFolder();
        }
        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            editRuleWindowViewModel.EditChoosenFile();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            editRuleWindowViewModel.EditMail(RuleListBox.SelectedItems);
        }
        private void AcceptEdition_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMailButton_Click(object sender, RoutedEventArgs e)
        {
            editRuleWindowViewModel.AddEmail();
        }
    }
}
