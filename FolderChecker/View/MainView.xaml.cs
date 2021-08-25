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
        private ViewModel.MainViewModel mainViewModel = new ViewModel.MainViewModel();
        public MainView()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddRuleWindow addRuleWindow = new AddRuleWindow();
            addRuleWindow.ShowDialog();
            mainViewModel.AddRule(addRuleWindow.GetRule());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.DeleteRule(ruleDetails.SelectedItem);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ruleDetails.SelectedItems.Count!=0)
            {
                foreach (object item in ruleDetails.SelectedItems)
                {
                    EditRuleWindow editRuleWindow = new EditRuleWindow((Model.Rule)item);
                    editRuleWindow.ShowDialog();
                }

            }

        }
    }
}
