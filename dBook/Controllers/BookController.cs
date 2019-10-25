using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dBook.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult BooksList()
        {
            return View();
        }
        public ActionResult TheBook()
        {
            return View();
        }
    }
}