using System;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        public CreateUserPage()
        {
            InitializeComponent();
            DataContext = MainViewModel.mvm;
        }

        private void NavigateToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToLoginPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToLoginPage();
        }
        private void NavigateToStartPage_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).NavigateToStartPage();
        }
    }
}
