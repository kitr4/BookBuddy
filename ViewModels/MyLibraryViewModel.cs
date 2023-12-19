using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Services;
using BookBuddy.Models;

namespace BookBuddy.ViewModels
{
    public partial class MyLibraryViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public MyLibraryViewModel(UserViewModel currentUser, DataService DS)
        {
            _dataService = DS;
            CurrentUser = currentUser;
        }
        public DataService DataService { get { return _dataService; } }

        [ObservableProperty]
        private BookViewModel _selectedBook;
        [ObservableProperty]
        private UserViewModel _currentUser;


        #region Commands
        [RelayCommand]
        public async Task ButtonAddToLibrary()
        {
            if (SelectedBook != null && CurrentUser != null && !CurrentUser.Library.Contains(SelectedBook))
            {
                CurrentUser.Library.Add(SelectedBook);
                await DataService.AddToLibrary(SelectedBook.Book, CurrentUser.User);

            }
        }
        [RelayCommand]
        public async Task ButtonRemoveFromLibrary()
        {
            // I dont think we need some conditionals for this operation. We find it under MyLibraryPage
            //, and all books shown on this page is guaranteed to be in both the database under users_books
            // and in Library collection on CurrentUser... but maybe there is a loophole somewhere.
            
            if (SelectedBook.Book != null)
            {
                await DataService.RemoveFromLibrary(SelectedBook.Book, CurrentUser.User);
            }
            CurrentUser.Library.Remove(SelectedBook);
        }
        [RelayCommand]
        public async Task ButtonRateBook()
        {
            await DataService.RateBook(SelectedBook.Rating, SelectedBook.Book, CurrentUser.User);
        }
        #endregion


    }
}

