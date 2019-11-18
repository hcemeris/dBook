using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using dBook.Models;
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
            //if (User.Identity.IsAuthenticated)
            //{
            //    var current_username = User.Identity.Name;
            //    var current_user = db.Users.Where(x => x.USERNAME == current_username).FirstOrDefault();
            //    var user = db.Users.Find(id);

            //}
            return View();
        }
        [Authorize]
        public ActionResult MyPage(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var current_username = User.Identity.Name;
                var current_user = db.Users.Where(x => x.USERNAME == current_username).FirstOrDefault();

                if (current_user.USER_ID == id)
                {
                    var user = db.Users.Find(id);
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}