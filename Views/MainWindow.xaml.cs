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

namespace BookBuddy.Views
{

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        { 
            InitializeComponent();
            MainViewModel mvm = new MainViewModel();
            DataContext = mvm;

            // The start page is loaded into the window 
            LoginPage.Navigate(new LoginPage());
        }
        
        public void NavigateToStartPage()
        {
            StartPage.Navigate(new StartPage());
        }

        public void NavigateToCreateUserPage()
        {
            CreateUserPage.Navigate(new CreateUserPage());
        }
    }
}
