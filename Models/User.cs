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
        private string _passWord;
        private string _eMail;
        private DateTime _birthdate;


        // Properties
        public int UserId { get => _userId; set => _userId = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string PassWord { get => _passWord; set => _passWord = value; }
        public string EMail { get => _eMail; set => _eMail = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }

        public User(int userId, string userName, string passWord, string eMail, DateTime birthdate)
        {
            UserId = userId;
            UserName = userName;
            PassWord = passWord;
            EMail = eMail;
            Birthdate = birthdate;
        }

    }
}
