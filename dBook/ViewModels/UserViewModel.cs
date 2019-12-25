using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class UserViewModel
    {
        public List<WantReadBooksList> WantReadBooksList{ get; set; }

        public List<ReadBooksList> ReadBooksList { get; set; }
        public List<FavoriteAuthors> FavoriteAuthors { get; set; }
        public List<MyBooks> MyBooks{ get; set; }
        public User User { get; set; }
    }
}