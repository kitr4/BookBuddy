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
            NavigateToLoginPage();
        }

        // If we have time, lets think about making a NavigationService instead of having it directly in the codebehind.
        #region Navigation
        public void NavigateToMyLibraryPage()
        {
            MainFrame.Navigate(new MyLibraryPage());
        }
        public void NavigateToSearchBookPage()
        {
            MainFrame.Navigate(new SearchBookPage());
        }

        public void NavigateToStartPage()
        {
            MainFrame.Navigate(new StartPage());
        }

        public void NavigateToCreateUserPage()
        {
            MainFrame.Navigate(new CreateUserPage());
        }

        // TO-DO: Fix
        public void NavigateToLoginPage() 
        {
            MainFrame.Navigate(new LoginPage());
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
