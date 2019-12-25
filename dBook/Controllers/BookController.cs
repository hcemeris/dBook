using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dBook.Models;
using dBook.ViewModels;
using System.Data.Entity;
namespace dBook.Controllers
{
    public class BookController : Controller
    {
        dBookContext db = new dBookContext();
        public ActionResult BooksList()
        {
            var BookListViewModel = new BookListViewModel();

            var books = db.Books.Include(x => x.AUTHOR).Include(k => k.CATEGORY).Take(10).ToList();
            var most_read = db.Books.Include(x => x.AUTHOR).Include(c => c.CATEGORY).OrderByDescending(o => o.READ_NUMB).Take(6).ToList();
            var last_add = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).OrderByDescending(x => x.BOOK_ID).Take(6).ToList();

            BookListViewModel.LastAdded = last_add;
            BookListViewModel.MostReaded = most_read;
            BookListViewModel.Books = books;
            return View(BookListViewModel);
        }
        [HttpPost]
        public ActionResult BooksList(string search)
        {
            var BookListViewModel = new BookListViewModel();

            var books = db.Books.Include(x => x.AUTHOR).Include(k => k.CATEGORY).Where(n=>n.BOOK_NAME.Contains(search)).ToList();
            var most_read = db.Books.Include(x => x.AUTHOR).Include(c => c.CATEGORY).OrderByDescending(o => o.READ_NUMB).Take(6).ToList();
            var last_add = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).OrderByDescending(x => x.BOOK_ID).Take(6).ToList();

            BookListViewModel.LastAdded = last_add;
            BookListViewModel.MostReaded = most_read;
            BookListViewModel.Books = books;
            return View(BookListViewModel);
        }
        public ActionResult TheBook(int id)
        {
            var theBook = db.Books.Include(a => a.AUTHOR).Include(k => k.CATEGORY).Where(x => x.BOOK_ID == id).FirstOrDefault();
            var auhtor = db.Authors.Find(theBook.AUTHOR.AUTHOR_ID);
            var comments = db.BookComments.Include(b => b.BOOK).Include(u => u.USER).Where(x => x.BOOK.BOOK_ID == id).ToList();
            var username = User.Identity.Name;
            var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault();

            if (user != null)
            {
                var isRead = db.ReadBooksLists.Include(u => u.USER).Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == theBook.BOOK_ID && x.USER.USER_ID == user.USER_ID).Count();
                var isWant = db.WantReadBooksLists.Include(u => u.USER).Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == theBook.BOOK_ID && x.USER.USER_ID == user.USER_ID).Count();
                var isLibrary = db.MyBooks.Include(b => b.Book).Include(u => u.User).Where(x=>x.Book.BOOK_ID == theBook.BOOK_ID && x.User.USER_ID == user.USER_ID ).Count();
                BookViewModel BookModel = new BookViewModel();
                BookModel.Author = auhtor;
                BookModel.Book = theBook;
                BookModel.Comments = comments;
                if (isRead > 0)
                {
                    BookModel.isRead = true;
                }
                else
                {
                    BookModel.isRead = false;
                }
                if (isWant > 0)
                {
                    BookModel.isWant = true;
                }
                else
                {
                    BookModel.isWant = false;
                }
                if (isLibrary>0)
                {
                    BookModel.isLibrary = true;
                }
                else
                {
                    BookModel.isLibrary = false;
                }

                return View(BookModel);
            }
            else
            {
                BookViewModel BookModel = new BookViewModel();
                BookModel.Author = auhtor;
                BookModel.Book = theBook;
                BookModel.Comments = comments;
                return View(BookModel);

            }


        }
        [HttpPost]
        public ActionResult CommentBook(int id, string _point, string _comment)
        {
            try
            {
                Books book = db.Books.Find(id);
                User user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
                BookComments new_comment = new BookComments();
                new_comment.BOOK = book;
                new_comment.USER = user;
                new_comment.COMMENT = _comment;
                new_comment.POINT = Convert.ToInt32(_point);
                db.BookComments.Add(new_comment);
                db.SaveChanges();
                return RedirectToAction("TheBook","Book", new { id = book.BOOK_ID });

            }
            catch (Exception)
            {
                return HttpNotFound();

            }

        }
        public ActionResult DeleteComment(int id)
        {
            try
            {
                var comment = db.BookComments.Include(b => b.BOOK).Where(x => x.BOOK_COMMENT_ID == id).FirstOrDefault();
                var book = db.Books.Find(comment.BOOK.BOOK_ID);
                db.BookComments.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("TheBook","Book", new { id = book.BOOK_ID});
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}