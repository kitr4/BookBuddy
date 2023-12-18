using BookBuddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BookBuddy.Models;
using BookBuddy.Services;
using BookBuddy.Views;


namespace BookBuddy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            
            // Create LoginPage and set it as the content of MainWindow
            // Show the MainWindow 
            mainWindow.Show();
        }
        public App()
        {
            InitializeComponent();
        }
    }
}
