using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class BookViewModel
    {
        public Books Book { get; set; }
        public List<BookComments> Comments { get; set; }
        public Authors Author { get; set; }
    }
}