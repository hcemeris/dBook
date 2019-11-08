using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class AuthorViewModel
    {
        public Authors Author { get; set; }
        public List<Books> Books { get; set; }
    }
}