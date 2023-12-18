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

       

        public MyLibraryViewModel()
        {
            _dataService = DataService.stDS;
        }
        public DataService DataService { get { return _dataService; } }

        [ObservableProperty]
        private BookViewModel _selectedBook;

        public async Task AddToLibrary()
        {
            if (SelectedBook != null && MainViewModel.mvm.CurrentUser != null && !MainViewModel.mvm.CurrentUser.Library.Contains(SelectedBook))
            {
                MainViewModel.mvm.CurrentUser.Library.Add(SelectedBook);
                await DataService.AddToLibrary(SelectedBook.Book, MainViewModel.mvm.CurrentUser.User) ;
            }
        }
        public async Task RemoveFromLibrary()
        {
            // I dont think we need some conditionals for this operation. We find it under MyLibraryPage
            //, and all books shown on this page is guaranteed to be in both the database under users_books
            // and in Library collection on CurrentUser... but maybe there is a loophole somewhere.
            await DataService.RemoveFromLibrary(SelectedBook.Book, MainViewModel.mvm.CurrentUser.User);
        }

        public async Task RateBook()
        {
            await DataService.RateBook(SelectedBook.Book, MainViewModel.mvm.CurrentUser.User);
        }


        // Vi kunne jo overveje om vi bare skal skrive metoderne i kommandoerne... frem for at kalde metoderne i kommandoerne. 

        #region Commands


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
        #endregion


    }
}

