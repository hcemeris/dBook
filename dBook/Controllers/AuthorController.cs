using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dBook.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult AuthorsList()
        {
            return View();
        }
        public ActionResult TheAuthor()
        {
            return View();
        }

    }
}