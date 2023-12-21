using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookBuddy.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        [ObservableProperty]
        private Author? _author;
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _nationality;

        public AuthorViewModel(Author? author)
        {
            Author = author;
            Name = author.Name;
            Nationality = author.Nationality;
        }
    }
}
