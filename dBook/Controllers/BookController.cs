using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dBook.Models;
using System.Data.Entity;
namespace dBook.Controllers
{
    public class BookController : Controller
    {
        dBookContext db = new dBookContext();
        public ActionResult BooksList()
        {
            var books = db.Books.Include(x=>x.AUTHOR).Include(k=>k.CATEGORY).ToList();
            return View(books);
        }
        public ActionResult TheBook(int id)
        {
            var theBook = db.Books.Find(id);

            return View(theBook);
        }
    }
}