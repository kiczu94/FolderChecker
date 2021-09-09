using System.Collections.Generic;
using System.Windows;
using FolderChecker.Model;

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddRuleWindow.xaml
    /// </summary>
    public partial class AddRuleWindow : Window
    {
        public ViewModel.AddRuleWindowViewModel AddRuleWindowViewModel = new ViewModel.AddRuleWindowViewModel();
        public AddRuleWindow(List<Rule> rules)
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
        public Rule GetRule()
        {
            return AddRuleWindowViewModel.MyWorkingRule;
        }
    }
}
