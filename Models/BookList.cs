using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Models
{
    public partial class BookList
    {
        private int _listId;
        private string _name;
        public string Name
        {
            get => _name; set => _name = value;
        }

        public BookList(int listid, string name)
        {
            _listId = listid;
            _name = name;
        }
    }
}
