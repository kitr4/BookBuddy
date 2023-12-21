using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Models
{
    public class Book
    {
        // Private backingfields
        private int? _bookId;
        private string? _genre;
        private string? _title;
        private string? _description;
        private int? _year;
        private string? _language;
        private int? _rating;
        private byte[]? _image; 

        // Properties
        public string? Genre { get => _genre; set => _genre = value; }
        public string? Title { get => _title; set => _title = value; }
        public string? Description { get => _description; set => _description = value; }
        public int? BookId { get => _bookId; set => _bookId = value; }
        public int? Year { get => _year; set => _year = value; }
        public string? Language { get => _language; set => _language = value; }
        public int? Rating { get => _rating; set => _rating = value; }
        public byte[]? Image { get => _image; set => _image = value; }
        
        public Book()
        {

        }
        public Book(int bookId, string genre, string title, string description, int year, string language, int rating)
        {
            _bookId = bookId;
            _genre = genre;
            _title = title;
            _description = description;
            _year = year;
            _language = language;
            _rating = rating;
            
        }
    }
}
