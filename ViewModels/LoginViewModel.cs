using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Services;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookBuddy.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DataService DS;

        public LoginViewModel(DataService dataService)
        {
            DS = dataService;
        }

        [ObservableProperty]
        private string? _enteredUsername;
        [ObservableProperty]
        private string? _enteredPassword;

        public async Task LogIn(string username, string password)
        {
            await Task.Run(async () =>
            {
                User RetrievedUser = await DS.VerifyAndInstantiate(username, password);
                if (RetrievedUser != null)
                {
                    MainViewModel.mvm.CurrentUser = new(RetrievedUser);
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
