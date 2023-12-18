using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Services;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookBuddy.Views;

namespace BookBuddy.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public LoginViewModel()
        {
            _dataService = DataService.stDS;
        }
        public DataService DataService { get { return _dataService; } }

        [ObservableProperty]
        private string? _enteredUsername;
        [ObservableProperty]
        private string? _enteredPassword;
        [ObservableProperty]
        private UserViewModel _currentUser;
       

        public async Task LogIn(string username, string password)
        {
            await Task.Run(async () =>
            {
                User RetrievedUser = await DataService.VerifyAndInstantiate(username, password);
                if (RetrievedUser != null)
                {
                    CurrentUser = new(RetrievedUser);
                    StartViewModel svm = new();
                    svm.CurrentUser = CurrentUser;
                    EnteredUsername = "";
                    EnteredPassword = "";
                }
            });
        }

        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await LogIn(EnteredUsername, EnteredPassword);
            // await InstantiateLibrary(); 
        }


    }
}
