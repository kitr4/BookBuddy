using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Models
{
    public class User
    {
        // Private backingfields
        private int _userId;
        private string _userName;
        private string _email;
        private DateTime _birthdate;
        // The password property is ONLY used for the instance of CREATING a user on CreateUserPage. When user has been created, the password field will be nullified, so the program does not contain password anywhere, aside from the moment it creates a user.
        private string? _password;
        private ObservableCollection<Book> _library = new ObservableCollection<Book>();


        // Properties
        public int UserId { get => _userId; set => _userId = value; }
        public string Username { get => _userName; set => _userName = value; }
        public string Email { get => _email; set => _email = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }
        public string? Password { get => _password; set => _password = value; }

        public ObservableCollection<Book>? Library { get => _library; set => _library = value; }
        


        public User()
        {

        }

        public User(int userId, string userName, string eMail, DateTime birthdate)
        {
            UserId = userId;
            Username = userName;
            Email = eMail;
            Birthdate = birthdate;

        }
    }
}
