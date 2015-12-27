using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        [OutputCache(CacheProfile = "CacheEstudentStatic")]
        public ActionResult Index()
        {
            return View();
        }
    }
}