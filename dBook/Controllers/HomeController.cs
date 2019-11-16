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
        [HttpPost]
        public ActionResult Search(string _search)
        {
            SearchViewModel search_model = new SearchViewModel();
            search_model.Authors = db.Authors.Where(x => x.AUTHOR_NAME.Contains(_search) || x.AUTHOR_LASTNAME.Contains(_search)).ToList();
            search_model.Books = db.Books.Where(x => x.BOOK_NAME.Contains(_search)).ToList();
            search_model.Users = db.Users.Where(x => x.USERNAME.Contains(_search) || x.NAME.Contains(_search) || x.LAST_NAME.Contains(_search)).ToList();

            return View(search_model);
        }
        
    }
}