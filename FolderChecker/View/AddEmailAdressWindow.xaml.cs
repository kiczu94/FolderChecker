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
    /// Logika interakcji dla klasy AddEmailAdressWindow.xaml
    /// </summary>
    public partial class AddEmailAdressWindow : Window
    {
        ViewModel.AddEmailAdressViewModel addEmailAdress = new ViewModel.AddEmailAdressViewModel();
        public AddEmailAdressWindow()
        {
            InitializeComponent();
            DataContext = addEmailAdress;
        }

        private void AddEmail_Click(object sender, RoutedEventArgs e)
        {
            addEmailAdress.AddEmail();
        }
    }
}
