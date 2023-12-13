 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookBuddy.ViewModels
{
    public class UserViewModel
    {
        // Backingfields
        private User _user;
        private string _username;
        private string _email;
        private DateTime _birthdate;
        // Properties
        public string Username { get => _username; set => _username = value; }
        public string Email { get => _email; set => _email = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }
        // List of books
        public ObservableCollection<BookViewModel> Books { get; } = new();

        public UserViewModel(User user)
        {
            this._user = user;
            this.Username = _user.Username;
            this.Email = _user.Email;
            this.Birthdate = _user.Birthdate;
            // Populates UserViewModel Library with the retrieved library on User.
             if (_user.Library != null)
            {
                foreach(var book in _user.Library)
                {
                    Books.Add(new BookViewModel(book));
                }
               
            }
        }

    }
}
