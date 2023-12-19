using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookBuddy.ViewModels
{
    public partial class BookViewModel : ObservableObject
    {
        [ObservableProperty]
        private Book _book;
        private string? _genre;
        private string? _title;
        private string? _description;
        private int? _year;
        private string? _language;
        [ObservableProperty]
        private int? _rating;

        // Properties
        public string? Genre { get => _genre; set => _genre = value; }
        public string? Title { get => _title; set => _title = value; }
        public string? Description { get => _description; set => _description = value; }
        public int? Year { get => _year; set => _year = value; }
        public string? Language { get => _language; set => _language = value; }
        
        

        public BookViewModel()
        {
            
        }
        public BookViewModel(Book book)
        {
            this.Book = book;
            this.Genre = book.Genre;
            this.Title = book.Title;
            this.Description = book.Description;
            this.Year = book.Year;
            this.Language = book.Language;
            this.Rating = book.Rating;
        }
    }
}
