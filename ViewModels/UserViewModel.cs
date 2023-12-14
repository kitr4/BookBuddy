 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookBuddy.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        // Backingfields
        private User _user;
        private string _username;
        private string _email;
        private DateTime _birthdate;
        
        // Properties
        public string Username { get => _username; set => _username = value; }
        public string Email { get => _email; set => _email = value; }
        public User User
        {
            get => _user;
        }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }
        // List of books
        [ObservableProperty]
        private ObservableCollection<BookViewModel> _library = new ObservableCollection<BookViewModel>();

        public UserViewModel(User user)
        {
            this._user = user;
            this.Username = _user.Username;
            this.Email = _user.Email;
            this.Birthdate = _user.Birthdate;
            // Populates UserViewModel Library with the retrieved library on User.
        }
    }

}

