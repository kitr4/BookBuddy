using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Models
{
    public class UserCreate
    {
        // Private backingfields
        private int _userId;
        private string _userName;
        private string _email;
        private DateTime _birthdate;
        // Two password properties are made, to be able to make a bool condition on comparing the two strings in Password enter 2x times.
        // The password property is ONLY used for the instance of CREATING a user on CreateUserPage. When user has been created, the password field will be nullified, so the program does not contain password anywhere, aside from the moment it creates a user.
        private string? _password1;
        private string? _password2;



        // Properties
        public string? Username { get => _userName; set => _userName = value; }
        public string? Email { get => _email; set => _email = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }
        public string? Password1 { get => _password1; set => _password1 = value; }
        public string? Password2 { get => _password2; set => _password2 = value; }

        public UserCreate()
        {
            this.Birthdate = DateTime.Now;
        }

        // TO-DO: Show UI message if fields are not filled out. Could be a isEnabled = false if not, I guess.
        //public UserCreate(string userName, string password, string eMail, DateTime birthdate)
        //{
        //    if (isIdenticalPassword())
        //    {
        //        Username = userName;
        //        Password1 = password;
        //        Email = eMail;
        //        Birthdate = birthdate;
        //    }
            
        //}

        // TO-DO: Find out default syntax for how datetime is entered dd/mm/year whatever
        public bool isFilledOut()
        {
            if (this.Username != null || this.Username != "" && this.Email != null || this.Email != "")
            {
                return true;
            }
            else
                return false;
        }
        
        // TO-DO: Show error message for: First: Password has to be longer than 8 chars. And passwords have to be equal.
        public bool isIdenticalPassword(UserCreate createdUser)
        {
            if (createdUser.Password1 != "" && createdUser.Password1.Length > 7 && createdUser.Password1 == createdUser.Password2) 
            {
                return true;
            }
            return false;
        }

        public void NullifyCreateUser()
        {
            this.Username = "";
            this.Password1 = "";
            this.Password2 = "";
            this.Email = "";
            this.Birthdate = DateTime.MinValue;
        }
    }
}
