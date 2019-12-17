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
            var AuthorViewModel = new AuthorListViewModel();

            var authors_ordername = db.Authors.OrderBy(x=>x.AUTHOR_NAME).ToList();
            AuthorViewModel.OrderName = authors_ordername;
            var most_favorite = db.Authors.OrderByDescending(x => x.FAVORITE_COUNT).ToList();
            AuthorViewModel.MostFavorite = most_favorite;
            var most_readed = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).OrderByDescending(x => x.READ_NUMB).ToList();
            AuthorViewModel.MostReaded = most_readed;
            return View(AuthorViewModel);
        }
        public ActionResult TheAuthor(int id)
        {
            var author = db.Authors.Find(id);
            var books = db.Books.Include(a => a.AUTHOR).Include(c => c.CATEGORY).Where(x => x.AUTHOR.AUTHOR_ID == id).ToList();
            var comments = db.AuthorComments.Include(x => x.USER).Include(a => a.AUTHOR).Where(c => c.AUTHOR.AUTHOR_ID == author.AUTHOR_ID).ToList();
            User user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
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
        [HttpPost]
        public ActionResult CommentAuthor(int id , string _point,string _comment)
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
            return RedirectToAction("AuthorsList");
        }

    }
}