using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dBook.Models;
namespace dBook.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        dBookContext db = new dBookContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BooksControl()
        {
            var books = db.Books.ToList();
            return View(books);
        }
        public ActionResult CreateBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Books book,HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/img/BookPhoto"), file.FileName);
                file.SaveAs(path);
                book.BOOK_PHOTO = path;
            }
            db.Books.Add(book);
            db.SaveChanges();

            return View();
        }
        public ActionResult EditBook(int id)
        {
            var book = db.Books.Find(id);
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBook([Bind(Include ="BOOK_ID,BOOK_NAME,BOOK_DESCRIPTION,BOOK_PHOTO")]Books book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult DeleteBook(int id)
        {
            Books book = db.Books.Find(id);
            if(book == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AuthorsControl()
        {
            return View();
        }
        public ActionResult UsersControl()
        {
            return View();
        }
        public ActionResult CommentsControl()
        {
            return View();
        }

    }
}