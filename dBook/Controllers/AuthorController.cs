using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dBook.Models;
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
            return View(author);
        }

    }
}