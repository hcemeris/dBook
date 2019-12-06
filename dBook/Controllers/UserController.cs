using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using dBook.Models;
using dBook.ViewModels;
namespace dBook.Controllers
{
    public class UserController : Controller
    {
        dBookContext db = new dBookContext();
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(string _username, string _password, string _name, string _lastname, HttpPostedFileBase _userphoto)
        {
            if(db.Users.Where(x=>x.USERNAME.ToLower().Contains(_username.ToLower())).Count() <= 0)
            {
                try
                {
                    var new_user = new User();
                    new_user.NAME = _name;
                    new_user.LAST_NAME = _lastname;
                    new_user.USERNAME = _username;
                    new_user.PASSWORD = _password;
                    if (_userphoto != null)
                    {
                        string photo_path = Path.GetFileName(_userphoto.FileName);
                        var upload_path = Path.Combine(Server.MapPath("~/img/UserPhoto/"), photo_path);
                        _userphoto.SaveAs(upload_path);
                        new_user.USER_PHOTO = photo_path;
                    }
                    new_user.ROLE = "User";
                    new_user.REGISTER_DATE = DateTime.Now;
                    db.Users.Add(new_user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
                catch (Exception)
                {

                }
            }
            else
            {
                ViewBag.username_error = "Bu kullanıcı adı alınmış";
            }

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string _username,string _password)
        {
            try
            {
                var user = db.Users.Where(x => x.USERNAME == _username).FirstOrDefault();
                if (user != null)
                {
                    if (user.PASSWORD == _password)
                    {
                        FormsAuthentication.SetAuthCookie(_username, false);
                        //return RedirectToAction("MyPage","User",new { id= user.USER_ID });
                        return RedirectToAction("HomePage","Home");
                    }
                    else
                    {
                        ViewBag.error = "Şifre yanlış";
                    }
                }
                else
                {
                    ViewBag.error = "Kayıt bulunamadı";
                }
            }
            catch (Exception)
            {

            }

            return View();
        }
        [Authorize]
        public ActionResult UserPage(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var current_username = User.Identity.Name;
                var current_user = db.Users.Where(x => x.USERNAME == current_username).FirstOrDefault();
                var user = db.Users.Find(id);
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.FavoriteAuthors = db.FavoriteAuthors.Include(u => u.USER).Include(a => a.AUTHOR).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                userViewModel.ReadBooksList = db.ReadBooksLists.Include(b => b.BOOK).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                userViewModel.WantReadBooksList = db.WantReadBooksLists.Include(b => b.BOOK).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                userViewModel.User = user;
                return View(userViewModel);
            }
            return View();
        }
        //public ActionResult UserPage()
        //{

        //    return View();
        //}
        //[Authorize]
        public ActionResult MyPage(string username)
        {
            if (User.Identity.IsAuthenticated)
            {
                var current_username = User.Identity.Name;
                var current_user = db.Users.Where(x => x.USERNAME == current_username).FirstOrDefault();

                if (current_user.USERNAME == username)
                {
                    var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault() ;
                    return View(user);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else return HttpNotFound();
        }
        [Authorize]
        public ActionResult UserSettings()
        {
            return View();
        }

        [Authorize]
        public ActionResult Add_Readed(int id)
        {
            var book = db.Books.Find(id);
            var username = User.Identity.Name;
            var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault();
            ReadBooksList new_read = new ReadBooksList();
            new_read.BOOK = book;
            new_read.USER = user;
            db.ReadBooksLists.Add(new_read);
            db.SaveChanges();
            return RedirectToAction("TheBook","Book",new { id=id});
        }
        public ActionResult Drop_Readed(int id)
        {
            var book = db.Books.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            ReadBooksList delete = db.ReadBooksLists.Include(u => u.USER).Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id && x.USER.USER_ID == user.USER_ID).FirstOrDefault();
            db.ReadBooksLists.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = id });
        }
        [Authorize]
        public ActionResult Add_Wish(int id)
        {
            var book = db.Books.Find(id);
            var username = User.Identity.Name;
            var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault();
            WantReadBooksList new_read = new WantReadBooksList();
            new_read.BOOK = book;
            new_read.USER = user;
            db.WantReadBooksLists.Add(new_read);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = id });
        }

        public ActionResult Drop_Wish(int id)
        {
            var book = db.Books.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            WantReadBooksList delete = db.WantReadBooksLists.Include(u => u.USER).Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id && x.USER.USER_ID == user.USER_ID).FirstOrDefault();
            db.WantReadBooksLists.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = id });
        }
        public ActionResult AddFavorite(int id)
        {
            var author = db.Authors.Find(id);
            var username = User.Identity.Name;
            var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault();
            FavoriteAuthors new_favorite = new FavoriteAuthors();
            new_favorite.AUTHOR = author;
            new_favorite.USER = user;
            db.FavoriteAuthors.Add(new_favorite);
            db.SaveChanges();
            return RedirectToAction("TheAuthor", "Author", new { id = id });
        }
        public ActionResult DropFavorite(int id)
        {
            var author = db.Authors.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            var delete = db.FavoriteAuthors.Include(a => a.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID && x.AUTHOR.AUTHOR_ID == author.AUTHOR_ID).FirstOrDefault();
            db.FavoriteAuthors.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("TheAuthor", "Author", new { id = id });
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}