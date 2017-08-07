using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLifecycle.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            List<string> events = HttpContext.Application["events"] as List<string>;
            string result = "<ul>";
            foreach (string e in events)
                result += "<li>" + e + "</li>";
            result += "</ul>";
            return result;
        }

        /*public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}