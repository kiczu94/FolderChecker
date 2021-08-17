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
        ViewModel.AddRuleWindowViewModel addNewRule = new ViewModel.AddRuleWindowViewModel();
        public AddRuleWindow()
        {
            InitializeComponent();
            DataContext = addNewRule;
        }

        private void Folder_button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "Select folder..."
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                addNewRule.MyNewRulePath = dialog.FileName;
            }
        }

        private void File_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                addNewRule.MyNewRulePath = openFileDialog.FileName;
            }
        }

        private void AddMail_Click(object sender, RoutedEventArgs e)
        {
            AddEmailAdressWindow emailAdressWindow = new AddEmailAdressWindow();
            emailAdressWindow.ShowDialog();
        }
    }
}
