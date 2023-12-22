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
        private readonly MainViewModel viewModel;
        public MainWindow(MainViewModel mvm)
        {
            viewModel = mvm;
            InitializeComponent();
            NavigateToStartPage();
        }

        // If we have time, lets think about making a NavigationService instead of having it directly in the codebehind.
        #region Navigation
        public void NavigateToMyLibraryPage()
        {
            MyLibraryPage myLibraryPage = new MyLibraryPage(viewModel);
            MainFrame.Navigate(myLibraryPage);
        }
        public void NavigateToSearchBookPage()
        {
            SearchBookPage searchBookPage = new SearchBookPage(viewModel);
            MainFrame.Navigate(searchBookPage);
        }

        public void NavigateToStartPage()
        {
            StartPage startPage = new StartPage(viewModel);
            MainFrame.Navigate(startPage);
        }

        public void NavigateToCreateUserPage()
        {
            CreateUserPage createUserPage = new CreateUserPage(viewModel);
            MainFrame.Navigate(createUserPage);
        }

        public void NavigateToLoginPage()
        {
            LoginPage loginPage = new LoginPage(viewModel);
            MainFrame.Navigate(loginPage);
        }

        public void NavigateToAdminPage()
        {
            AdminPage adminPage = new AdminPage(viewModel);
            MainFrame.Navigate(adminPage);
        }
    
        public void GoBack()
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
        #endregion

    }
}
