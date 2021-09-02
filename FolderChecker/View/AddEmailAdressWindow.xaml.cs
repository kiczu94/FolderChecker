using System.Collections.Generic;
using System.Windows;


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }
        public List<string> Export ()
        {
            return addEmailAdress.exportEmailAdresses();
        }

    }
}
