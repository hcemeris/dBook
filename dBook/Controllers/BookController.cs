﻿using System;
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
            var books = db.Books.Include(x=>x.AUTHOR).Include(k=>k.CATEGORY).ToList();
            return View(books);
        }
        public ActionResult TheBook(int id)
        {
            var theBook = db.Books.Include(a => a.AUTHOR).Include(k => k.CATEGORY).Where(x => x.BOOK_ID == id).FirstOrDefault() ;
            var auhtor = db.Authors.Find(theBook.AUTHOR.AUTHOR_ID);
            var comments = db.BookComments.Include(b => b.BOOK).Include(u => u.USER).Where(x => x.BOOK.BOOK_ID == id).ToList();
            BookViewModel BookModel = new BookViewModel();
            BookModel.Author = auhtor;
            BookModel.Book = theBook;
            BookModel.Comments = comments;
            return View(BookModel);
        }
        [HttpPost]
        public ActionResult CommentBook(int id, string _point, string _comment)
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
            return RedirectToAction("BooksList");
        }
    }
}