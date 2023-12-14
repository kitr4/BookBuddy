using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using BookBuddy.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Dynamic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace BookBuddy.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        public static MainViewModel mvm { get; } = new MainViewModel();
        
        public MainViewModel ()
        {
           
        }
        #region Properties and backing fields
        [ObservableProperty]
        public UserViewModel? _currentUser;

        // Selected book on a given list
        [ObservableProperty]
        private BookViewModel? _selectedBook;

        // Represents a given list at a given time, blank to start with
        [ObservableProperty]
        private ObservableCollection<Book> _bookList = new ObservableCollection<Book>();
        #endregion

    }
 }


 
