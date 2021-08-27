using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
    }
}
