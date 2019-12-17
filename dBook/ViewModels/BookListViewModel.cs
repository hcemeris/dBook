using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class BookListViewModel
    {
        public List<Books> MostReaded { get; set; }
        public List<Books> LastAdded { get; set; }
        public List<Books> Books { get; set; }
    }
}