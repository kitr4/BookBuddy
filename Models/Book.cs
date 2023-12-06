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

     
        // Properties
        public string? Genre { get => _genre; set => _genre = value; }
        public string? Title { get => _title; set => _title = value; }
        public string? Description { get => _description; set => _description = value; }
        public int? BookId { get => _bookId; set => _bookId = value; }
        public int? Year { get => _year; set => _year = value; }
        public string? Language { get => _language; set => _language = value; }


        // private Author author;
        // Ville det give mening at tage denne med? Hvis vi istedet laver en liste på hver author der creates og tilføjer bog med et hvis ID (hentet fra author_books) og tilføjer bogen til instantiering af author, er det vel ikke nødvendigt at have bogen tilkoblet. private string _author;

        public Book()
        {

        }
        public Book(int bookId, string genre, string title, string description, int year, string language)
        {
            _bookId = bookId;
            _genre = genre;
            _title = title;
            _description = description;
            _year = year;
            _language = language;
        }
    }
}
