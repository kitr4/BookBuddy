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
        #region Properties and backing fields
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
        private Book? _currentBook = new();
        // properties used only on Log-in frame
        [ObservableProperty]
        private string? _username;
        [ObservableProperty]
        private string? _password;
        [ObservableProperty]
        private string? _searchText = "Search for booktitle, name of author....";
        

        // Represents a given list at a given time, blank to start with
        [ObservableProperty]
        private ObservableCollection<Book> _bookList = new ObservableCollection<Book>();
        #endregion

        #region Methods
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
        public async Task SearchBookAndInstantiate()
        {
            BookList = await DS.SearchBook(SearchText);
        }
        public async Task AddToLibrary()
        {
            if(CurrentBook != null && !CurrentUser.Library.Contains(CurrentBook))
            {
                CurrentUser.Library.Add(CurrentBook);
                await DS.AddToLibrary(CurrentBook, CurrentUser);
            }
        }
        public async Task RemoveFromLibrary()
        {
            // I dont think we need some conditionals for this operation. We find it under MyLibraryPage
            //, and all books shown on this page is guaranteed to be in both the database under users_books
            // and in Library collection on CurrentUser... but maybe there is a loophole somewhere.
            await DS.RemoveFromLibrary(CurrentBook, CurrentUser);
        }
        public async Task RateBook()
        {
            await DS.RateBook(CurrentBook, CurrentUser);
        }
        #endregion

        // Vi kunne jo overveje om vi bare skal skrive metoderne i kommandoerne... frem for at kalde metoderne i kommandoerne. 
       
        #region Commands
        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await QueryValidation(Username, Password);
            await InstantiateLibrary();
        }
        [RelayCommand]
        public async Task ButtonRateBook()
        {
            RateBook();
        }
        [RelayCommand]
        public async Task ButtonSearchBook()
        {
            await SearchBookAndInstantiate();
        }
        [RelayCommand]
        public async Task ButtonAddToLibrary()
        {
            await AddToLibrary();
        }
        [RelayCommand]
        public async Task ButtonRemoveFromLibrary()
        {
            await RemoveFromLibrary();
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

        #endregion;
    }
}
