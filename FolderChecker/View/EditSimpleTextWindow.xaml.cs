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
using FolderChecker.ViewModel;
using FolderChecker.Model;

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy EditSimpleText.xaml
    /// </summary>
    public partial class EditSimpleTextWindow : Window
    {
        public EditSimpleTextViewModel editSimpleTextViewModel = new EditSimpleTextViewModel();
        public EditSimpleTextWindow(string propertyName, string thingToChange)
        {
            editSimpleTextViewModel.MyPropertyName = propertyName;
            editSimpleTextViewModel.MyOldText = thingToChange;
            InitializeComponent();
            DataContext = editSimpleTextViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            editSimpleTextViewModel.editText();
            this.Close();
        }
        public string GetName()
        {
            return editSimpleTextViewModel.MyNewText;
        }
    }
}
