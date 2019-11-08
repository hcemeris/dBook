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
    public class AuthorController : Controller
    {
        dBookContext db = new dBookContext();
        public ActionResult AuthorsList()
        {
            var authors = db.Authors.ToList();
            return View(authors);
        }
        public ActionResult TheAuthor(int id)
        {
            var author = db.Authors.Find(id);
            var books = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).Where(x => x.AUTHOR.AUTHOR_ID == id).ToList();
            AuthorViewModel AuthorView = new AuthorViewModel();
            AuthorView.Author = author;
            AuthorView.Books = books;
            return View(AuthorView);
        }

    }
}