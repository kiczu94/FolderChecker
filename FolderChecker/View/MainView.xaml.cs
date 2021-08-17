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

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private List<Model.Rule> mainListOfRules = new List<Model.Rule>();
        public MainView()
        {
            InitializeComponent();
            mainListOfRules = PopulateList();
            DataContext = new ViewModel.MainViewModel(mainListOfRules);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindow addRuleWindow = new AddRuleWindow();
            addRuleWindow.ShowDialog();
        }
        private List<Model.Rule> PopulateList()
        {
            List<Model.Rule> rules = new List<Model.Rule>();
            for (int i = 0; i < 10; i++)
            {
                string rulename = $"zasada " + (i+1);
                string path = $"ścieżka " + (i + 1);
                List<string> adressList = new List<string>();
                for (int j = 0; j < 3; j++)
                {
                    adressList.Add($"adres " + (j + 1));
                }
                Model.Rule rule = new Model.Rule(rulename, path, adressList);
                rules.Add(rule);
            }
            return rules;
        }
    }
}
