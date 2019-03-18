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
        public ActionResult Contract()
        {
            ViewBag.Title = "Contract";

            return View();
        }
        public ActionResult ContractDetail()
        {
            ViewBag.Title = "Contract Detail";

            return View();
        }
        public ActionResult AdminProduct()
        {
            ViewBag.Title = "Product";

            return View();
        }
        public ActionResult AdminProductDetail()
        {
            ViewBag.Title = "Product Detail";

            return View();
        }
        public ActionResult User()
        {
            ViewBag.Title = "User";

            return View();
        }
        public ActionResult UserDetail()
        {
            ViewBag.Title = "User Detail";

            return View();
        }
        public ActionResult Shop()
        {
            ViewBag.Title = "Shop";

            return View();
        }
        public ActionResult ShopDetail()
        {
            ViewBag.Title = "Shop Detail";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Title = "Login";

            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.Title = "Dashboard";

            return View();
        }
        public ActionResult Feedback()
        {
            ViewBag.Title = "Feedback";

            return View();
        }
        public ActionResult Employee()
        {
            ViewBag.Title = "Employee";

            return View();
        }
        public ActionResult EmployeeDetail()
        {
            ViewBag.Title = "Employee Detail";

            return View();
        }
    }
}
