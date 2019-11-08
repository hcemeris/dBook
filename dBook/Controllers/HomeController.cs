using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dBook.Models;
using dBook.ViewModels;
namespace dBook.Controllers
{
    public class HomeController : Controller
    {
        dBookContext db = new dBookContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult CategoryResult(string s)
        {
            if (s != null)
            {
                var category = db.Categories.Where(x => x.NAME == s).FirstOrDefault();
                var books = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).Where(x => x.CATEGORY.NAME == s).ToList();
                CategoryBooks CategoryBooks = new CategoryBooks();
                CategoryBooks.Books = books;
                CategoryBooks.Category = category;
                return View(CategoryBooks);

            }
            else
            {
                return View();
            }


        }
        
    }
}