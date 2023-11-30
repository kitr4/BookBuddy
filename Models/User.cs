using System;
using System.Collections.Generic;
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
        private string _password;
        private string _email;
        private DateTime _birthdate;


        // Properties
        public int UserId { get => _userId; set => _userId = value; }
        public string Username { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public string Email { get => _email; set => _email = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }

        public User()
        {

        }
        public User(int userId, string userName, string passWord, string eMail, DateTime birthdate)
        {
            UserId = userId;
            Username = userName;
            Password = passWord;
            Email = eMail;
            Birthdate = birthdate;
        }
        // Just a test-constructor:
        public User(int userId, string userName, string passWord, string eMail)
        {
            UserId = userId;
            Username = userName;
            Password = passWord;
            Email = eMail;
        }

    }
}
