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
            DataContext = MainViewModel.mvm;
            //LoginPage.Navigate(new LoginPage());
        }
        
        public void NavigateToLoginPage()
        {
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

        private void NavigateToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToStartPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToLoginPage();
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
    }
}
