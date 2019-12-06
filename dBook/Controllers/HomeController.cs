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
            var books = db.Books.ToList().OrderBy(x => x.BOOK_ID);
            return View(books);
        }
        public ActionResult CategoryResult()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = db.Categories.ToList();
            categoryViewModel.LastAdded = db.Books.Include(a => a.AUTHOR).OrderByDescending(x => x.BOOK_ID).Take(4).ToList();
            categoryViewModel.MostReaded = db.Books.OrderByDescending(x => x.READ_NUMB).Take(4).ToList();
            return View(categoryViewModel);
        }
        public ActionResult TheCategory(int id)
        {
            CategoryBooks categoryBooks = new CategoryBooks();
            categoryBooks.Books = db.Books.Include(c => c.CATEGORY).Where(x => x.CATEGORY.CATEGORY_ID == id).ToList();
            categoryBooks.Category = db.Categories.Find(id);
            return View(categoryBooks);
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