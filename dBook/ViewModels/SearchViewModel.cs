using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    
    public class SearchViewModel
    {
        public List<Books> Books { get; set; }
        public List<User> Users { get; set; }
        public List<Authors> Authors { get; set; }
        public string Searched { get; set; }
    }
}