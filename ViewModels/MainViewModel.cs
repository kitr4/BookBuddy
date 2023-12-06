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

        [ObservableProperty]
        private User? _currentUser;

        [ObservableProperty]
        private Book? _currentBook;

        [ObservableProperty]
        private string? _username;
        [ObservableProperty]
        private string? _password;

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

        #region Commands
        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await QueryValidation(Username, Password);
            
        }

        [RelayCommand]
        public async Task ButtonCreateUser()
        {
            await DS.CreateUser(CurrentUser, Password);
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
