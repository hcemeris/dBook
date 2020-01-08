using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class AuthorListViewModel
    {
        public List<Authors> MostFavorite{ get; set; }
        public List<Authors> Last_Added { get; set; }
        public List<Authors> MostReaded { get; set; }
        public List<Authors> Authors { get; set; }
    }
}