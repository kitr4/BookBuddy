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

        [ObservableProperty]
        private string? username;
        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool? isValidated = false;



        // METHODS
        public async Task QueryValidation(string Username, string Password)
        {
            await Task.Run(async () =>
            {
                IsValidated = await DS.VerifyCredentials(Username, Password);
            });
        }
        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await QueryValidation(Username, Password);
        }
    }
}
