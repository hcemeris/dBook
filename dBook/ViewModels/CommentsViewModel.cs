using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class CommentsViewModel
    {
        public List<BookComments> BooksComments{ get; set; }
        public List<AuthorComments> AuthorComments{ get; set; }
    }
}