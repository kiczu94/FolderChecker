using System.Windows;
using FolderChecker.ViewModel;

namespace FolderChecker.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private MainViewModel mainViewModel = new MainViewModel();
        public MainView()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.AddRule();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.DeleteRule(ruleDetails.SelectedItem);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.EditRule(ruleDetails.SelectedItems);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.Login(passwordBox1.Password);
            if (mainViewModel.IsEmailCorrect)
            {
                LoginPanel.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Collapsed;
                EmailTextBlock.Visibility = Visibility.Visible;
                EmailTextBox.Visibility = Visibility.Collapsed;
                passwordBox1.Visibility = Visibility.Collapsed;
                PasswordTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Nieprawidłowy email lub hasło!");
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.Settings();
        }

    }
}
