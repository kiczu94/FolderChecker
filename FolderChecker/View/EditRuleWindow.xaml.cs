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
        }
        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Nowa nazwa reguły",editRuleWindowViewModel.MyRuleName);
            editSimpleText.ShowDialog();
            editRuleWindowViewModel.MyRuleName=editSimpleText.GetName();
        }
        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AcceptEdition_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
