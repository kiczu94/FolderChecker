using System;
using System.Collections.Generic;
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
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddRuleWindow.xaml
    /// </summary>
    public partial class AddRuleWindow : Window
    {
        private ViewModel.AddRuleWindowViewModel AddRuleWindowViewModel = new ViewModel.AddRuleWindowViewModel();
        public AddRuleWindow(List<Model.Rule> rules)
        {
            AddRuleWindowViewModel.MyRules = rules;
            InitializeComponent();
            DataContext = AddRuleWindowViewModel; 
        }
        
        private void Folder_button_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindowViewModel.ChooseFolder();   
        }

        private void File_button_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindowViewModel.ChooseFile();
        }
        
        private void AddMail_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindowViewModel.AddMail();
        }

        private void AddNewRule_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindowViewModel.AddRule();
            this.Close();
        }
        public Model.Rule GetRule()
        {
            return AddRuleWindowViewModel.MyWorkingRule;
        }
    }
}
