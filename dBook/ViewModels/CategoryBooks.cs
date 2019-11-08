using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;

namespace dBook.ViewModels
{
    public class CategoryBooks
    {
        public Category Category { get; set; }
        public List<Books> Books { get; set; }
    }
}