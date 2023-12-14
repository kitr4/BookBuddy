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
        public DataService DS = new();

        [ObservableProperty]
        private string _searchText = "Search for booktitle, name of author....";

        [ObservableProperty]
        private ObservableCollection<BookViewModel> _bookList = new();

        [RelayCommand]
        public async Task SearchBook()
        {
            List<Book> bookConversionList = await DS.SearchBook(SearchText);
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
            if (MainViewModel.mvm.SelectedBook != null && MainViewModel.mvm.CurrentUser != null && MainViewModel.mvm.CurrentUser.Library.Contains(MainViewModel.mvm.SelectedBook))
            {
                MainViewModel.mvm.CurrentUser.Library.Add(MainViewModel.mvm.SelectedBook);
                await DS.AddToLibrary(MainViewModel.mvm.SelectedBook.Book, MainViewModel.mvm.CurrentUser.User);
            }
        }
    }
}
