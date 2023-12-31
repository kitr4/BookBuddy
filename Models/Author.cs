﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Models
{
    public class Author
    {
        // Private backingfields
        private int _authorId;
        private ObservableCollection<Book> _booksWritten = new ObservableCollection<Book>();
        private string _name;
        private string _nationality;

        // Properties
        public int AuthorId { get => _authorId; set => _authorId = value; }
        public ObservableCollection<Book> BooksWritten { get => _booksWritten; set => _booksWritten = value; }
        public string Name { get => _name; set => _name = value; }
        public string Nationality { get => _nationality; set => _nationality = value; }

        public Author(int authorId, ObservableCollection<Book> booksWritten, string name, string nationality)
        {
            AuthorId = authorId;
            BooksWritten = booksWritten;
            Name = name;
            Nationality = nationality;
        }

       
    }
}
