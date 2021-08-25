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
        public EditRuleWindow(Rule editableRule)
        {
            EditRuleWindowViewModel editRuleWindowViewModel = new EditRuleWindowViewModel( editableRule);
            InitializeComponent();
            DataContext = editRuleWindowViewModel;
        }

        private void AcceptEdition_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
