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
using dBook.ViewModels;
namespace dBook.Controllers
{
    [Authorize(Roles ="Admin")]
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
            var books = db.Books.OrderBy(x => x.BOOK_ID).ToPagedList<Books>(_pageNo, 5);
            return View(books);
        }
        public ActionResult CreateBook()
        {
            var categories = db.Categories.ToList();
            ViewBag.category = new SelectList(categories, "CATEGORY_ID", "NAME");
            var authors = db.Authors.ToList();
            ViewBag.authors = new SelectList(authors, "AUTHOR_ID", "AUTHOR_NAME");
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Books book, HttpPostedFileBase file)
        {
            var authors = db.Authors.ToList();
            ViewBag.authors = new SelectList(authors, "AUTHOR_ID", "AUTHOR_NAME");
            var categories = db.Categories.ToList();
            ViewBag.category = new SelectList(categories, "CATEGORY_ID", "NAME");

            var category = db.Categories.Find(book.CATEGORY.CATEGORY_ID);
            var author = db.Authors.Find(book.AUTHOR.AUTHOR_ID);
            book.AUTHOR = author;
            book.CATEGORY = category;
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
                var library = db.MyBooks.Include(b => b.Book).Include(u => u.User).Where(x => x.Book.BOOK_ID == id).ToList();

                db.MyBooks.RemoveRange(library);
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
        //ÜYE PANELİ
        public ActionResult UsersControl()
        {
            return View(db.Users.ToList());
        }
        public ActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            var author_comments = db.AuthorComments.Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
            foreach (var item in author_comments)
            {
                db.AuthorComments.Remove(item);
            }
            var book_comments = db.BookComments.Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
            foreach(var item in book_comments)
            {
                db.BookComments.Remove(item);
            }
            var read_booklist = db.ReadBooksLists.Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
            foreach(var item in read_booklist)
            {
                db.ReadBooksLists.Remove(item);
            }
            var want_booklist = db.WantReadBooksLists.Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
            foreach (var item in want_booklist)
            {
                db.WantReadBooksLists.Remove(item);
            }
            var favorite_authors = db.FavoriteAuthors.Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
            foreach (var item in favorite_authors)
            {
                db.FavoriteAuthors.Remove(item);
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UsersControl","Admin");
        }
        public ActionResult ChangeRole(int id)
        {
            var user = db.Users.Find(id);
            if(user.ROLE == "Admin")
            {
                user.ROLE = "User";
            }else if (user.ROLE == "User")
            {
                user.ROLE = "Admin";
            }
            db.SaveChanges();
            return RedirectToAction("UsersControl", "Admin");
        }

        public ActionResult CommentsControl()
        {
            CommentsViewModel cvm = new CommentsViewModel();

            var bookComments = db.BookComments.Include(b => b.BOOK).Include(u => u.USER).ToList();
            var authorComments = db.AuthorComments.Include(a => a.AUTHOR).Include(u => u.USER).ToList();
            cvm.AuthorComments = authorComments;
            cvm.BooksComments = bookComments;
            return View(cvm);
        }
        public ActionResult DeleteComment_Book(int id)
        {
            var comment = db.BookComments.Find(id);
            db.BookComments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("CommentsControl", "Admin");
        }
        public ActionResult DeleteComment_Author(int id)
        {
            var comment = db.AuthorComments.Find(id);
            db.AuthorComments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("CommentsControl", "Admin");
        }
        public ActionResult CategoryPanel()
        {
            return View(db.Categories.ToList());
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
        public ActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            var books = db.Books.Include(c => c.CATEGORY).Where(x => x.CATEGORY.CATEGORY_ID == category.CATEGORY_ID).ToList();
            foreach (var item in books)
            {
                db.Books.Remove(item);
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("CategoryPanel","Admin");
        }
    }
}