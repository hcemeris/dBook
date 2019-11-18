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
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string _username, string _password, string _name, string _lastname, System.Web.HttpPostedFileBase _userphoto)
        {
            if(db.Users.Where(x=>x.USERNAME == _username).Count() <= 0)
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

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
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
        public ActionResult UserPage()
        {
            return View();
        }
        public ActionResult MyPage(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }
        public ActionResult UserSettings()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}