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
using System.Dynamic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace BookBuddy.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public DataService DS = new DataService();
        // En måde at binde alle op på SAMME objekt, men jaer... ikke bedste praksis når man multithreader over samme objekt, åbenbart 
        public static MainViewModel mvm { get; } = new MainViewModel();
        // This user is blank aside from when being filled out on CreateUserPage. Upon creation it will again be blank, so that the properties wont be set if another user is created in the same instance of the program.
        [ObservableProperty]
        private UserCreate? _createdUser = new();
        // Validated user will be set to CurrentUser on login.
        [ObservableProperty]
        private User? _currentUser = new();
        // Selected book on a given list
        [ObservableProperty]
        private Book? _currentBook;
        // properties used only on Log-in frame
        [ObservableProperty]
        private string? _username;
        [ObservableProperty]
        private string? _password;
        [ObservableProperty]
        private string? _searchText = "....";

        // Represents a given list at a given time, blank to start with
        [ObservableProperty]
        IEnumerable<Book> _bookList = new ObservableCollection<Book>();

        
        // METHODS
        public async Task QueryValidation(string username, string password)
        {
            await Task.Run(async () =>
            {
                CurrentUser = await DS.LogIn(username, password);
                
            });
        }

        // TO-DO:
        public async Task InstantiateLibrary()
        {
            CurrentUser.Library = await DS.RetrieveLibrary(CurrentUser.UserId); 
        }
        // TO-DO: NEXT THING TO DO AFTER PC RESET AND BREAK (07/12)
        public async Task SearchBooksAndInstantiate()
        {
            await DS.SearchBook(SearchText)
        }

        #region Commands
        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await QueryValidation(Username, Password);
            await InstantiateLibrary();
        }
        [RelayCommand]
        public async Task ButtonSearchBook()
        {
            
        }

        [RelayCommand]
        public void ClearSearchText()
        {
            SearchText = string.Empty;
        }
        [RelayCommand]
        public async Task ButtonCreateUser()
        {
            if(CreatedUser.isIdenticalPassword(CreatedUser))
            {
                if(CreatedUser.isFilledOut())
                {
                    CurrentUser.Username = CreatedUser.Username;
                    CurrentUser.Email = CreatedUser.Email;
                    CurrentUser.Birthdate = CreatedUser.Birthdate;
                   await DS.CreateUser(CreatedUser, CreatedUser.Password1);
                }
            }
           //TO-DO: Show error message for given slots not filled out or password length not being over 7.
           
            // await DS.CreateUser(CurrentUser);
        }
        [RelayCommand]
        public async Task ButtonSearchBook()
        {
            BookList = await DS.SearchBook(CurrentBook);
        }
        
        

        #endregion;
    }
}
