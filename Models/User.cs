﻿using System;
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
        private string _email;
        private DateTime _birthdate;


        // Properties
        public int UserId { get => _userId; set => _userId = value; }
        public string Username { get => _userName; set => _userName = value; }
        public string Email { get => _email; set => _email = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }

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

        public User(int userId, string userName, string eMail)
        {
            UserId = userId;
            Username = userName;
            Email = eMail;
        }

    }
}
