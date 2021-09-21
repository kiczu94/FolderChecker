﻿using System.Windows;
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
            mainViewModel.Login();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.Settings();
        }
    }
}
