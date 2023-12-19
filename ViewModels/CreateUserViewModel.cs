using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookBuddy.Models;
using BookBuddy.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookBuddy.ViewModels
{
    public partial class CreateUserViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public CreateUserViewModel(DataService DS)
        {
            _dataService = DS;
        }

        public DataService DataService { get { return _dataService; } }

        [ObservableProperty]
        private string? _username;
        [ObservableProperty]
        private string? _password1;
        [ObservableProperty] 
        private string? _password2;
        [ObservableProperty]
        private string? _identicalPassword;
        [ObservableProperty]
        private string? _email;
        [ObservableProperty]
        private DateTime _birthdate = DateTime.Now;

        

        private bool _isValidated = false;
        public bool isValidated
        {
            get => _isValidated;
            set => _isValidated = value;
        }

        // TO-DO:
        // Make selection of birthdate logic
     
        public void NullifyFields()
        {
            if (isValidated)
            {
                Username = "";
                Password1 = "";
                Password2 = "";
                Email = "";
                IdenticalPassword = "";
            }
        }

        public async Task CreateUser()
        {
            isValidated = await CreateUserValidation(Username, Email, Password1, Password2);

            if (isValidated)
            {
                await DataService.CreateUser(Username, IdenticalPassword, Email, Birthdate);
                NullifyFields();
            }
        }

        private async Task<bool> CreateUserValidation(string username, string email, string password1, string password2)
        {
            if (password1 != password2)
            {
                // TO-DO: Error message, passwords not identical and break
            }
          
                // TO-DO: Error messages on Email and/or username not filled out
            // Logic to check if the values are in the Users table in the database.
            bool isExisting = await DataService.IfUserExists(username, email);

            if (isExisting)
            {
                // TO-DO: error message, a user with that email or username was already found.
                return false;
            }
            IdenticalPassword = Password1;
            return true;
        }

        // Method called when the "Create User" button is clicked
        [RelayCommand]
        private async Task ButtonCreateUser()
        {
            await CreateUser();
        }
    }
    
}
