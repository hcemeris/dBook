using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dBook.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BooksControl()
        {
            return View();
        }
        public ActionResult AuthorsControl()
        {
            return View();
        }
        public ActionResult UsersControl()
        {
            return View();
        }
        public ActionResult CommentsControl()
        {
            return View();
        }

    }
}