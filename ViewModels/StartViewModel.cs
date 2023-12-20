using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Services;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Windows;

namespace BookBuddy.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public StartViewModel(UserViewModel currentUser, DataService DS)
        {
            _dataService = DS;
            _currentUser = currentUser;
        }

        [ObservableProperty]
        private UserViewModel _currentUser;

      

        [RelayCommand]
        public void LogOut()
        {
            CurrentUser.NullifyFields();
        }

    }
}
