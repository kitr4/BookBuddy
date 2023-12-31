﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookBuddy.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        // Properties
        #region Properties
        [ObservableProperty]
        private User? _user;
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private bool _isAdmin;

        // List of books
        [ObservableProperty]
        private ObservableCollection<BookViewModel> _library = new ObservableCollection<BookViewModel>();
        #endregion

        public UserViewModel()
        {

        }

        public UserViewModel(User user)
        {
            this._user = user;
            this.Username = _user.Username;
            this.Email = _user.Email;
            this.IsAdmin = _user.isAdmin;
            // Populates UserViewModel Library with the retrieved library on User.
        }

        public void SetProperties(UserViewModel uvm)
        {
            this.User = uvm.User; 
            this.Username = uvm.Username; 
            this.Email = uvm.Email;
            this.IsAdmin = uvm.IsAdmin;
        }

        public void NullifyFields()
        {
            if (this.User != null && this.Username != "")
            {
                this.User.UserId = 0;
                this.User.Username = "";
                this.User.Email = "";
                this.Username = "";
                this.Email = "";
            }
          
        }
    }

}

