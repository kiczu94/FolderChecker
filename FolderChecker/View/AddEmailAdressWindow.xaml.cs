using System.Collections.Generic;
using System.Windows;
using FolderChecker.ViewModel;


namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddEmailAdressWindow.xaml
    /// </summary>
    public partial class AddEmailAdressWindow : Window
    {
        AddEmailAdressViewModel addEmailAdressViewModel = new AddEmailAdressViewModel();
        public AddEmailAdressWindow()
        {
            InitializeComponent();
            DataContext = addEmailAdressViewModel;
        }

        private void AddEmail_Click(object sender, RoutedEventArgs e)
        {
            addEmailAdressViewModel.AddEmail();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }
        public List<string> Export ()
        {
            return addEmailAdressViewModel.exportEmailAdresses();
        }

    }
}
