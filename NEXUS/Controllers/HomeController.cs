using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEXUS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Homepage()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Product()
        {
            ViewBag.Title = "Product";

            return View();
        }

        public ActionResult ProductDetail()
        {
            ViewBag.Title = "Product Detail";

            return View();
        }

        public ActionResult History()
        {
            ViewBag.Title = "History";

            return View();
        }

        public ActionResult Pay()
        {
            ViewBag.Title = "Pay";

            return View();
        }

        public ActionResult UserProfile()
        {
            ViewBag.Title = "User Profile";

            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Title = "About Us";

            return View();
        }

    }
}
