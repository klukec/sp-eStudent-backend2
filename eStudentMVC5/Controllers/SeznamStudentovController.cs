using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    public class SeznamStudentovController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: SeznamStudentov
        public ActionResult Index()
        {
            var students = from s in db.uporabnik where s.idVloge.Equals(1) select s;
            return View(students.ToList());
            
            //var students = from s in db.uporabnik where s.idVloge.Equals(1) select s;
            //ViewData["students"] = students;
            //return View();

            //IEnumerable<uporabnik> students2 = students;
            //ViewData["students2"] = students2;
            //return View();

        }

    }
}