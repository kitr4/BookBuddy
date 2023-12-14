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
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartViewModel startVM = new();
        public StartPage()
        {
            InitializeComponent();
            DataContext = MainViewModel.mvm;
        }
        private void NavigateToSearchBookPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToStartPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToSearchBookPage();
        }
        private void NavigateToMyLibraryPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToStartPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToMyLibraryPage();
        }
        private void NavigateToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToStartPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToLoginPage();
        }

    }
}
