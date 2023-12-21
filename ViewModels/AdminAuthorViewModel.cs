using BookBuddy.Models;
using BookBuddy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.ViewModels
{
    public partial class AdminAuthorViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public AdminAuthorViewModel(DataService DS)
        {
            _dataService = DS;
        }
        public DataService DataService { get { return _dataService; } }
        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private AuthorViewModel _selectedAuthor;

        private ObservableCollection<AuthorViewModel> _authorList;
        public ObservableCollection<AuthorViewModel> AuthorList { get { return _authorList; } }



        [RelayCommand]
        public async Task UpdateAuthor()
        {
            if (!AuthorList.Contains(SelectedAuthor) && SelectedAuthor.Author != null)
            {
                SelectedAuthor.Author.Name = SelectedAuthor.Name;
                SelectedAuthor.Author.Nationality = SelectedAuthor.Nationality;
                await DataService.UpdateAuthor(SelectedAuthor.Author);
            }
        }

        [RelayCommand]
        public async Task DeleteAuthor()
        {
            if (!AuthorList.Contains(SelectedAuthor) && SelectedAuthor.Author != null)
            {
                await DataService.DeleteAuthor(SelectedAuthor.Author);
                AuthorList.Remove(SelectedAuthor);
            }
        }
        [RelayCommand]
        public async Task SearchAuthors()
        {
            List<Author> authorConversionList = await DataService.SearchAuthor(SearchText);
            foreach (Author author in authorConversionList)
            {
                AuthorViewModel authorviewmodel = new(author);
                AuthorList.Add(authorviewmodel);
            }
        }

    }
}

