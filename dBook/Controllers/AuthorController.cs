﻿using System;
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
        [AllowAnonymous]
        public ActionResult AuthorsList()
        {
            var AuthorViewModel = new AuthorListViewModel();

            var authors_lastadd = db.Authors.OrderBy(x => x.AUTHOR_ID).Take(6).ToList();
            AuthorViewModel.Last_Added= authors_lastadd;
            var most_favorite = db.Authors.OrderByDescending(x => x.FAVORITE_COUNT).Take(6).ToList();
            AuthorViewModel.MostFavorite = most_favorite;
            var most_readed = db.Authors.OrderByDescending(x=>x.FAVORITE_COUNT).Take(6).ToList();
            AuthorViewModel.MostReaded = most_readed;
            AuthorViewModel.Authors = db.Authors.Take(10).ToList();
            return View(AuthorViewModel);
        }
        [HttpPost]
        public ActionResult AuthorsList(string search)
        {
            var AuthorViewModel = new AuthorListViewModel();

            var authors_lastadd = db.Authors.OrderBy(x => x.AUTHOR_ID).Take(6).ToList();
            AuthorViewModel.Last_Added = authors_lastadd;
            var most_favorite = db.Authors.OrderByDescending(x => x.FAVORITE_COUNT).Take(6).ToList();
            AuthorViewModel.MostFavorite = most_favorite;
            var most_readed = db.Authors.OrderByDescending(x=>x.FAVORITE_COUNT).Take(6).ToList();
            AuthorViewModel.MostReaded = most_readed;
            var authors = db.Authors.Where(x => x.AUTHOR_NAME.Contains(search) || x.AUTHOR_LASTNAME.Contains(search)).ToList();
            AuthorViewModel.Authors = authors;
            return View(AuthorViewModel);
        }
        public ActionResult TheAuthor(int id)
        {
            var author = db.Authors.Find(id);
            var books = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).Where(x => x.AUTHOR.AUTHOR_ID == id).ToList();
            var comments = db.AuthorComments.Include(x => x.USER).Include(a => a.AUTHOR).Where(c => c.AUTHOR.AUTHOR_ID == author.AUTHOR_ID).ToList();
            User user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var isFavorite = db.FavoriteAuthors.Include(a => a.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID && x.AUTHOR.AUTHOR_ID == id).Count();
                AuthorViewModel AuthorView = new AuthorViewModel();
                AuthorView.Author = author;
                AuthorView.Books = books;
                AuthorView.AuthorComments = comments;
                if (isFavorite > 0)
                {
                    AuthorView.isFavorite = true;
                }
                else
                {
                    AuthorView.isFavorite = false;

                }
                return View(AuthorView);
            }
            else
            {
                AuthorViewModel AuthorView = new AuthorViewModel();
                AuthorView.Author = author;
                AuthorView.Books = books;
                AuthorView.AuthorComments = comments;
                return View(AuthorView);
            }

        }
        [HttpPost]
        public ActionResult CommentAuthor(int id, string _point, string _comment)
        {
            try
            {

                Authors author = db.Authors.Find(id);
                User user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
                AuthorComments new_comment = new AuthorComments();
                new_comment.AUTHOR = author;
                new_comment.USER = user;
                new_comment.COMMENT = _comment;
                new_comment.POINT = Convert.ToInt32(_point);
                db.AuthorComments.Add(new_comment);
                db.SaveChanges();
                return RedirectToAction("TheAuthor", new { id = author.AUTHOR_ID });
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
                var comment = db.AuthorComments.Include(a=>a.AUTHOR).Where(x=>x.AUTHOR_COMMENT_ID==id).FirstOrDefault();
                var author = db.Authors.Where(x => x.AUTHOR_ID == comment.AUTHOR.AUTHOR_ID).FirstOrDefault();
                db.AuthorComments.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("TheAuthor",new { id=author.AUTHOR_ID});
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}