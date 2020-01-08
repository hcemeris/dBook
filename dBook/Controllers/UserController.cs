using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            if (db.Users.Where(x => x.USERNAME.ToLower().Contains(_username.ToLower())).Count() <= 0)
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
                    else
                    {
                        var photo_path = Path.GetFileName("~/img/DefaultPhoto/default_user.jpeg");
                        var upload_path = Path.Combine(Server.MapPath("~/img/UserPhoto/"), photo_path);
                        _userphoto.SaveAs(upload_path);
                        new_user.USER_PHOTO = photo_path;
                    }
                    new_user.ROLE = "User";
                    new_user.REGISTER_DATE = DateTime.Now;
                    db.Users.Add(new_user);
                    db.SaveChanges();
                    return RedirectToAction("HomePage", "Home");
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
        public ActionResult Login(string _username, string _password, bool rememberBox)
        {
            try
            {
                var user = db.Users.Where(x => x.USERNAME == _username).FirstOrDefault();
                if (user != null)
                {
                    if (user.PASSWORD == _password)
                    {
                        var authTicket = new FormsAuthenticationTicket(
                            1,
                            user.USERNAME,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(20),
                            rememberBox,
                            "",
                            "/"
                            );
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                        Response.Cookies.Add(cookie);
                        //FormsAuthentication.SetAuthCookie(_username, false);
                        //return RedirectToAction("MyPage","User",new { id= user.USER_ID });
                        return RedirectToAction("HomePage", "Home");
                    }
                    else
                    {
                        ViewBag.error = "Şifre yanlış";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.error = "Kayıt bulunamadı";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");

            }

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
                userViewModel.ReadBooksList = db.ReadBooksLists.Include(b => b.BOOK).Include(a => a.BOOK.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                userViewModel.WantReadBooksList = db.WantReadBooksLists.Include(b => b.BOOK).Include(a => a.BOOK.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                userViewModel.MyBooks = db.MyBooks.Include(b => b.Book).Include(a => a.Book.AUTHOR).Include(u => u.User).Where(x => x.User.USER_ID == user.USER_ID).ToList();
                userViewModel.User = user;
                return View(userViewModel);
            }
            return View();
        }
        [Authorize]
        public ActionResult MyPage(string username)
        {
            if (User.Identity.IsAuthenticated)
            {
                var current_username = User.Identity.Name;
                var current_user = db.Users.Where(x => x.USERNAME == current_username).FirstOrDefault();

                if (current_user.USERNAME == username)
                {
                    var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault();
                    UserViewModel userViewModel = new UserViewModel();
                    userViewModel.FavoriteAuthors = db.FavoriteAuthors.Include(u => u.USER).Include(a => a.AUTHOR).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                    userViewModel.ReadBooksList = db.ReadBooksLists.Include(b => b.BOOK).Include(a => a.BOOK.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                    userViewModel.WantReadBooksList = db.WantReadBooksLists.Include(b => b.BOOK).Include(a => a.BOOK.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID).ToList();
                    userViewModel.MyBooks = db.MyBooks.Include(b => b.Book).Include(a => a.Book.AUTHOR).Include(u => u.User).Where(x => x.User.USER_ID == user.USER_ID).ToList();
                    userViewModel.User = user;
                    return View(userViewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else return HttpNotFound();
        }
        [Authorize]
        public ActionResult Add_Library(int id)
        {
            var book = db.Books.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            MyBooks new_add = new MyBooks();
            new_add.Book = book;
            new_add.User = user;
            db.MyBooks.Add(new_add);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = book.BOOK_ID });
        }
        [Authorize]
        public ActionResult Drop_Library(int id)
        {
            var book = db.Books.Find(id);
            var mybook = db.MyBooks.Include(b => b.Book).Where(x => x.Book.BOOK_ID == book.BOOK_ID).FirstOrDefault();
            db.MyBooks.Remove(mybook);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = book.BOOK_ID });
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
            book.READ_NUMB++;
            db.ReadBooksLists.Add(new_read);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = id });
        }
        [Authorize]
        public ActionResult Drop_Readed(int id)
        {
            var book = db.Books.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            ReadBooksList delete = db.ReadBooksLists.Include(u => u.USER).Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id && x.USER.USER_ID == user.USER_ID).FirstOrDefault();
            book.READ_NUMB--;
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

        [Authorize]
        public ActionResult Drop_Wish(int id)
        {
            var book = db.Books.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            WantReadBooksList delete = db.WantReadBooksLists.Include(u => u.USER).Include(b => b.BOOK).Where(x => x.BOOK.BOOK_ID == id && x.USER.USER_ID == user.USER_ID).FirstOrDefault();
            db.WantReadBooksLists.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("TheBook", "Book", new { id = id });
        }
        [Authorize]
        public ActionResult AddFavorite(int id)
        {
            var author = db.Authors.Find(id);
            var username = User.Identity.Name;
            var user = db.Users.Where(x => x.USERNAME == username).FirstOrDefault();
            FavoriteAuthors new_favorite = new FavoriteAuthors();
            author.FAVORITE_COUNT++;
            new_favorite.AUTHOR = author;
            new_favorite.USER = user;
            db.FavoriteAuthors.Add(new_favorite);
            db.SaveChanges();
            return RedirectToAction("TheAuthor", "Author", new { id = id });
        }
        [Authorize]
        public ActionResult DropFavorite(int id)
        {
            var author = db.Authors.Find(id);
            var user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            var delete = db.FavoriteAuthors.Include(a => a.AUTHOR).Include(u => u.USER).Where(x => x.USER.USER_ID == user.USER_ID && x.AUTHOR.AUTHOR_ID == author.AUTHOR_ID).FirstOrDefault();
            author.FAVORITE_COUNT--;
            db.FavoriteAuthors.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("TheAuthor", "Author", new { id = id });
        }
        [Authorize]
        public ActionResult UserSettings(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        [Authorize]
        public ActionResult UserSettings(User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.GetFileName(file.FileName);
                    var upload_path = Path.Combine(Server.MapPath("~/img/UserPhoto/"), path);
                    file.SaveAs(upload_path);
                    user.USER_PHOTO = path;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyPage", "User", new { username = user.USERNAME });
            }
            return View();
        }
        [Authorize]
        public ActionResult TradeOffer(int id)
        {
            TradeViewModel tvm = new TradeViewModel();
            var send_user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            tvm.get_offer_user = db.Users.Find(id);
            tvm.send_offer_user = db.Users.Where(x => x.USER_ID == send_user.USER_ID).FirstOrDefault();
            tvm.get_offer_books = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == id).ToList();
            ViewBag.getbooks = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == id).ToList();
            tvm.send_offer_books = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == send_user.USER_ID).ToList();
            ViewBag.sendbooks = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == send_user.USER_ID).ToList();

            return View(tvm);
        }
        [HttpPost]
        [Authorize]
        public ActionResult TradeOffer(int id, string get_book, string send_book)
        {
            TradeViewModel tvm = new TradeViewModel();
            var send_user = db.Users.Where(x => x.USERNAME == User.Identity.Name).FirstOrDefault();
            tvm.get_offer_user = db.Users.Find(id);
            tvm.send_offer_user = db.Users.Where(x => x.USER_ID == send_user.USER_ID).FirstOrDefault();
            tvm.get_offer_books = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == id).ToList();
            ViewBag.getbooks = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == id).ToList();
            tvm.send_offer_books = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == send_user.USER_ID).ToList();
            ViewBag.sendbooks = db.MyBooks.Include(u => u.User).Include(b => b.Book).Where(x => x.User.USER_ID == send_user.USER_ID).ToList();

            var fromAddress = new MailAddress("dbook.info.mail@gmail.com");
            var toAddress = new MailAddress("dbook.info.mail@gmail.com");
            const string subject = "dBook | Takas İsteği";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "dbooksau")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = send_user.USERNAME + " kullanıcısından takas isteği." + get_book + " kitabınız için " + send_book })
                {
                    smtp.Send(message);
                }
            }

            return RedirectToAction("TradeOffer", "User", new { id = id });

        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}