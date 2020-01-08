using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    public class HomePageViewModel
    {
        public List<Books> Last_Added { get; set; }
        public List<Books> Most_Read{ get; set; }
        public List<Authors> Most_Favorite { get; set; }
    }
}