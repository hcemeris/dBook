using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
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
        public ActionResult BooksControl(int? PageNo)
        {
            int _pageNo = PageNo ?? 1;
            var books = db.Books.OrderBy(x => x.BOOK_ID).ToPagedList<Books>(_pageNo, 1);
            return View(books);
        }
        public ActionResult CreateBook()
        {
            var authors = db.Authors.ToList();
            ViewBag.authors = new SelectList(authors, "AUTHOR_ID", "AUTHOR_NAME");
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Books book, HttpPostedFileBase file)
        {
            var authors = db.Authors.ToList();
            ViewBag.authors = new SelectList(authors, "AUTHOR_ID", "AUTHOR_NAME");

            var author = db.Authors.Find(book.AUTHOR.AUTHOR_ID);
            book.AUTHOR = author;
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.GetFileName(file.FileName);
                var upload_path = Path.Combine(Server.MapPath("~/img/BookPhoto/"), path);
                file.SaveAs(upload_path);
                book.BOOK_PHOTO = path;
            }
            db.Books.Add(book);
            db.SaveChanges();

            return View();
        }
        public ActionResult EditBook(int id)
        {
            var book = db.Books.Find(id);
            ViewBag.bookname = book.BOOK_NAME;
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBook([Bind(Include = "BOOK_ID,BOOK_NAME,BOOK_DESCRIPTION")]Books book)
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
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
                var read_list = db.ReadBooksLists.Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id).ToList();
                var want_list = db.WantReadBooksLists.Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id).ToList();
                var comments = db.BookComments.Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id).ToList();
                db.ReadBooksLists.RemoveRange(read_list);
                db.WantReadBooksLists.RemoveRange(want_list);
                db.BookComments.RemoveRange(comments);
                db.Books.Remove(book);
                db.SaveChanges();

            }
            return RedirectToAction("BooksControl");
        }
        public ActionResult AuthorsControl()
        {
            var authors = db.Authors.ToList();
            return View(authors);
        }
        public ActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAuthor(Authors author, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.GetFileName(file.FileName);
                var upload_path = Path.Combine(Server.MapPath("~/img/AuthorPhoto/"), path);
                file.SaveAs(upload_path);
                author.AUTHOR_PHOTO = path;
            }
            db.Authors.Add(author);
            db.SaveChanges();
            return View();
        }
        public ActionResult EditAuthor(int id)
        {
            Authors author = db.Authors.Find(id);
            return View(author);
        }
        [HttpPost]
        public ActionResult EditAuthor([Bind(Include = "AUTHOR_ID,AUTHOR_NAME,AUTHOR_LASTNAME,AUTHOR_DESCRIPTION")]Authors author, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.GetFileName(file.FileName);
                    var upload_path = Path.Combine(Server.MapPath("~/img/AuthorPhoto/"), path);
                    file.SaveAs(upload_path);
                    author.AUTHOR_PHOTO = path;
                }
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult DeleteAuthor(int id)
        {
            var author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UsersControl()
        {
            return View();
        }
        public ActionResult CommentsControl()
        {
            return View();
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category new_category)
        {
            db.Categories.Add(new_category);
            db.SaveChanges();
            return View();
        }
    }
}