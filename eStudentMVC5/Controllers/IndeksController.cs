using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    public class IndeksController : Controller
    {
        // GET: Indeks
        public ActionResult Index()
        {
            var dbContext = new estudentEntities();
            var user = dbContext.uporabnik.Find(1);
            string ime = user.ime;
            ViewBag.imeUporabnika = ime;
            return View();
        }
    }
}