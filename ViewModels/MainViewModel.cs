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

        private User currentUser { get; set; }

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
                currentUser = await DS.LogIn(username, password);
                Password = currentUser.Username;
            });
        }
        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await QueryValidation(Username, Password);
        }
    }
}
