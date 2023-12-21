using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookBuddy.Models
{
    public partial class User : ObservableObject
    {
        // Private backingfields
        
        private int _userId;
        private string _userName;
        private string _email;
        private bool _isAdmin = false;
 
        private ObservableCollection<Book> _library = new ObservableCollection<Book>();

        // Properties
        public int UserId { get => _userId; set => _userId = value; }
        public string Username { get => _userName; set => _userName = value; }
        public string Email { get => _email; set => _email = value; }

        public ObservableCollection<Book>? Library { get => _library; set => _library = value; }
        public bool isAdmin { get => _isAdmin; set => _isAdmin = value; }

        public User()
        {

        }
        public User(int userId, string userName, string eMail, bool admin)
        {
            UserId = userId;
            Username = userName;
            Email = eMail;
            isAdmin = admin;
        }

    }
}
