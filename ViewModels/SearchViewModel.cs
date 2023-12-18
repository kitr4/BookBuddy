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
        private readonly DataService _dataService;

        public SearchViewModel()
        {
            _dataService = DataService.stDS;
        }
        public DataService DataService { get { return _dataService; } }
        [ObservableProperty]
        private BookViewModel? _selectedBook;

        [ObservableProperty]
        private string _searchText = "Search for booktitle, name of author....";

        [ObservableProperty]
        private ObservableCollection<BookViewModel> _bookList = new();

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
        public async Task AddToLibrary()
        {
            if (SelectedBook != null && MainViewModel.mvm.CurrentUser != null && MainViewModel.mvm.CurrentUser.Library.Contains(SelectedBook))
            {
                MainViewModel.mvm.CurrentUser.Library.Add(SelectedBook);
                await DataService.AddToLibrary(SelectedBook.Book, MainViewModel.mvm.CurrentUser.User);
            }
        }
    }
}
