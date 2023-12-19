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
                DBAccess dBAccess = new DBAccess();
                MainViewModel mvm = new MainViewModel(dBAccess);
                MainWindow mainWindow = new MainWindow(mvm);
                mainWindow.DataContext = mvm;
            
                mainWindow.Show();
                mainWindow.Activate();
                base.OnStartup(e);
            }
        public App()
        {
            //InitializeComponent();
        }
    }
}
