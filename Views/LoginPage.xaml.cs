﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookBuddy.ViewModels;
using BookBuddy.Services;

namespace BookBuddy.Views
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(MainViewModel mvm)
        {
            DataContext = mvm;
            InitializeComponent();
            
        }

        private void NavigateToStartPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToStartPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToStartPage();
        }

        private void NavigateToCreateUserPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a CreateUserPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToCreateUserPage();
        }

        private void AutoUpdatedCopyright()
        {
            int currentYear = DateTime.Now.Year;
            copyrightText.Content = $"Copyright &#169; {currentYear} The EJNar Group. All rights reserved.";
        }
    }
}
