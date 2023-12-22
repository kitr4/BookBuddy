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

        // Dependency
        private readonly DataService _dataService;

        public CreateUserViewModel(DataService DS)
        {
            _dataService = DS;
        }

        public DataService DataService { get { return _dataService; } }

        // Observable properties 
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

        
        private bool _isValidated = false;
        public bool isValidated
        {
            get => _isValidated;
            set => _isValidated = value;
        }

        // Nullifies the fields of Create User if validationprocess of creating user (eg. username was not already found in database) It is being nullified, so that if CreateUser page is navigated to in same app lifetime, the fields wont be filled out
        public void NullifyFields()
        {
            if (isValidated)
            {
                this.Username = "";
                this.Password1 = "";
                this.Password2 = "";
                this.Email = "";
                this.IdenticalPassword = "";
            }
        }

        // If passwords did not match, or user already did exist in database (by email or username), bool false/true is returned respectively and isValidated takes the value of this in the ButtonCreateUserCommand.
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

        

        // Nullify, but bound up on a backbutton with no validation bool.
        [RelayCommand]
        private void ButtonBack()
        {
            this.Username = "";
            this.Password1 = "";
            this.Password2 = "";
            this.Email = "";
            this.IdenticalPassword = "";
        }

        // Method called when the "Create User" button is clicked
        // if validation bool is set to true, it means that the credentials were valid, and no user was found, and based on this, dataservice saves these credentials in the database on the user table.
        [RelayCommand]
        private async Task ButtonCreateUser()
        {
            isValidated = await CreateUserValidation(Username, Email, Password1, Password2);

            if (isValidated)
            {
                await DataService.CreateUser(Username, IdenticalPassword, Email);
                NullifyFields();
            }
        }
        
    }
    
}
