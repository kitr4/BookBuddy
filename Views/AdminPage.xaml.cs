using BookBuddy.ViewModels;
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

namespace BookBuddy.Views
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(MainViewModel mvm )
        {
            DataContext = mvm;
            InitializeComponent();
        }

        private void NavigateToStartPage_Click(object sender, RoutedEventArgs e)
        {
            // Assuming the parent window has a NavigateToStartPage method
            ((MainWindow)Window.GetWindow(this)).NavigateToStartPage();
        }
    }
}
