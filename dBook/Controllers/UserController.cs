using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dBook.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult UserPage()
        {
            return View();
        }
        public ActionResult MyPage()
        {
            return View();
        }
        public ActionResult UserSettings()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");

        }
    }
}