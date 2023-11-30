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

namespace BookBuddy.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public DataService DS = new DataService();

        // En måde at binde alle op på SAMME objekt, men jaer... ikke bedste praksis når man multithreader over samme objekt, åbenbart 
        public static MainViewModel mvm { get; } = new MainViewModel();

        [ObservableProperty]
        private User? _currentUser;
        

        [ObservableProperty]
        private string? username;
        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool isValidated = false;

       
        
        // METHODS
        public async Task QueryValidation(string username, string password)
        {

            await Task.Run(async () =>
            {
                CurrentUser = await DS.LogIn(username, password);
                Password = CurrentUser.Username;
            });
        }


        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await QueryValidation(Username, Password);
        }
    }
}
