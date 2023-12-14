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
        // Denne kunne evt også gøres til static, og så ville vi bruge den samme reference til at udøve servicerne.
        public DataService DS { get; set; }

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

        //public async Task InstantiateLibrary()
        //{
        //    await DS.RetrieveLibrary(CurrentUser.User.UserId);

        //}

        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await LogIn(EnteredUsername, EnteredPassword);
            // await InstantiateLibrary(); 
        }

    }
}
