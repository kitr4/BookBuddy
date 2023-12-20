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
        [ObservableProperty]
        private string? _genre;
        [ObservableProperty]
        private string? _title;
        [ObservableProperty]
        private string? _description;
        [ObservableProperty]
        private int? _year;
        [ObservableProperty]
        private string? _language;
        [ObservableProperty]
        private int? _rating;
        [ObservableProperty]
        private byte[]? _image;

        public BookViewModel()
        {
            
        }
        public BookViewModel(Book book)
        {
            this.Book = book;
            this.Title = book.Title;
            this.Description = book.Description;
            this.Genre = book.Genre;
            this.Year = book.Year;
            this.Language = book.Language;
            this.Rating = book.Rating;
            this.Image = book.Image;
        }
    }
}
