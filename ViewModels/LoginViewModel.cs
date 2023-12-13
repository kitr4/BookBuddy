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
        public DataService DS { get; set; }
        [ObservableProperty]
        private string? _username;
        [ObservableProperty]
        private string? _password;
        [ObservableProperty]
        public UserViewModel _currentUser;



        public async Task LogIn(string username, string password)
        {
            await Task.Run(async () =>
            {
                User RetrievedUser = await DS.VerifyAndInstantiate(username, password);
                if (RetrievedUser != null)
                {
                    CurrentUser = new UserViewModel(RetrievedUser);
                    MainViewModel mvm = new MainViewModel(CurrentUser);
                    Username = "";
                    Password = "";
                }
            });
        }

        //public async Task InstantiateLibrary()
        //{
        //    await DS.RetrieveLibrary(CurrentUser.User.UserId);

        //}

        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await LogIn(Username, Password);
            // await InstantiateLibrary();
           
        }

    }
}
