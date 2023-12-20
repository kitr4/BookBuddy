using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Services;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.Input;

namespace BookBuddy.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        // Dependencies
        private readonly DataService _dataService;
        public DataService DataService { get { return _dataService; } }

        public SearchViewModel(UserViewModel currentUser, DataService DS)
        {
            _dataService = DS;
            CurrentUser = currentUser;
        }

        
        #region Properties
        [ObservableProperty]
        private UserViewModel _currentUser;
        [ObservableProperty]
        private BookViewModel? _selectedBook;
        [ObservableProperty]
        private string _searchText = "Search for booktitle, name of author....";
        [ObservableProperty]
        private ObservableCollection<BookViewModel> _bookList = new();
        #endregion
        #region Commands
        [RelayCommand]
        public async Task SearchBook()
        {
            List<Book> bookConversionList = await DataService.SearchBook(SearchText);
            foreach(Book book in bookConversionList)
            {
                BookViewModel bookviewmodel = new(book);
                BookList.Add(bookviewmodel);
            }
        }

        [RelayCommand]
        public void ClearSearchText()
        {
            SearchText = string.Empty;
        }
        [RelayCommand]
        public async Task ButtonAddToLibrary()
        {
            if (SelectedBook != null && CurrentUser != null && !CurrentUser.Library.Contains(SelectedBook))
            {
                CurrentUser.Library.Add(SelectedBook);
                await DataService.AddToLibrary(SelectedBook.Book, CurrentUser.User);
            }
        }
        #endregion
    }
}
