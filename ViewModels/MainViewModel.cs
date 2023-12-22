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
        // MainViewModel is dependant on various ViewModels. 
        // These ViewModels are being set up as observableproperties, and then set in the constructor. 
        // MainViewModel now serves as a hub for all ViewModels, and the various pages can access the ViewModels associated with the pages (through name)
        private readonly DataService _dataService;
        [ObservableProperty]
        private UserViewModel _currentUser;
        [ObservableProperty]
        private LoginViewModel _loginVM;
        [ObservableProperty]
        private CreateUserViewModel _createUserVM;
        [ObservableProperty]
        private StartViewModel _startVM;
        [ObservableProperty]
        private MyLibraryViewModel _myLibraryVM;
        [ObservableProperty]
        private SearchViewModel _searchVM;
        [ObservableProperty]
        private AdminBookViewModel _adminBookVM;

        public MainViewModel(IDBAccess DBAccess)
        {
            // The various dependencies are injected onto the instantiated ViewModels 
            _dataService = new DataService(DBAccess);
            CurrentUser = new UserViewModel();
            CreateUserVM = new(_dataService);
            LoginVM = new(CurrentUser, _dataService);
            CreateUserVM = new(_dataService);
            StartVM = new(CurrentUser, _dataService);
            MyLibraryVM = new(CurrentUser, _dataService);
            SearchVM = new(CurrentUser, _dataService);
            AdminBookVM = new(CurrentUser, _dataService);
        }
    }

}
 


 
