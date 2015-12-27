using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    public class LokacijaFakulteteController : Controller
    {
        // GET: LokacijaFakultete
        [OutputCache(CacheProfile = "CacheEstudentStatic")]
        public ActionResult Index()
        {
            return View();
        }
    }
}