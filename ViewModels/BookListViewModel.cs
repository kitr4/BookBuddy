using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;

namespace BookBuddy.ViewModels
{
    public partial class BookListViewModel : ObservableObject
    {
        private BookList list;

        [ObservableProperty]
        private string _name;

        public BookListViewModel(BookList list)
        {
            this.list = list;
            this.Name = list.Name;
        }
        
    }
}
