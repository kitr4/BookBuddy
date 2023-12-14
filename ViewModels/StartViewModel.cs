﻿using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using BookBuddy.Services;
using CommunityToolkit.Mvvm.Input;

namespace BookBuddy.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        public DataService DS = new();

        public void NullifyCurrentUser()
        {
            MainViewModel.mvm.CurrentUser.Username = "";
            MainViewModel.mvm.CurrentUser.Library = null;
            MainViewModel.mvm.CurrentUser.Email = "";
            MainViewModel.mvm.CurrentUser.Birthdate = DateTime.Now;
        }

        public void ConvertBookToBVMAndPopulate(List<Book> bookList)
        {
            if (MainViewModel.mvm.CurrentUser != null)
            {
                foreach (Book book in bookList)
                {
                    BookViewModel bookViewModel = new(book);
                    MainViewModel.mvm.CurrentUser.Library.Add(bookViewModel);
                }
            }
        }


        [RelayCommand]
        public async Task InstantiateLibrary()
        {
            List<Book> retrievedList = await DS.RetrieveLibrary(MainViewModel.mvm.CurrentUser.User.UserId);
            ConvertBookToBVMAndPopulate(retrievedList);
        }


        [RelayCommand]
        public void LogOut()
        {
            NullifyCurrentUser();
        }

    }
}
