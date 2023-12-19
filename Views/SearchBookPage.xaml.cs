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
    /// Interaction logic for SearchBookPage.xaml
    /// </summary>
    public partial class SearchBookPage : Page
    {
        public SearchBookPage(MainViewModel mvm)
        {
            DataContext = mvm;
            InitializeComponent();
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).GoBack();
        }
    }
}
