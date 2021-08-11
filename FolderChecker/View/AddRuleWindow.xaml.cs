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

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddRuleWindow.xaml
    /// </summary>
    public partial class AddRuleWindow : Window
    {
        public AddRuleWindow()
        {
            InitializeComponent();
        }

        private void Browse_button_Click(object sender, RoutedEventArgs e)
        { /*
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ValidateNames = false;
            dlg.CheckFileExists = false;
            dlg.CheckPathExists = true;
            dlg.FileName = "Select folder";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string extension = System.IO.Path.GetExtension(filename);
                if (extension == "")
                {
                    textbox1.Text = System.IO.Path.GetDirectoryName(dlg.FileName);
                }
                else
                {
                    textbox1.Text = filename;
                }
                
            }
            */
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker=true,
                Title="Select folder..."
            };
            if (dialog.ShowDialog()==CommonFileDialogResult.Ok)
            {
                textbox1.Text = dialog.FileName;
            }
        }
    }
}
