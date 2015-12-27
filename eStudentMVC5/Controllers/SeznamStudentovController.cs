using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class SeznamStudentovController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: SeznamStudentov
        [OutputCache(CacheProfile = "CacheEstudent")]
        public ActionResult Index()
        {
            var students = from s in db.uporabnik where s.idVloge.Equals(1) select s;
            return View(students.ToList());
        }

    }
}