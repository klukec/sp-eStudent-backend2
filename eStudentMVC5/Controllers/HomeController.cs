using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Business;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["stIzpitnihRokov"] = BusinessLogic.stIzpitnihRokov();
            ViewData["vnosOcen"] = BusinessLogic.stNezakljucenihRokov();
            ViewData["opravili"] = BusinessLogic.stStudentovZakljucilo();
            ViewData["neopravili"] = BusinessLogic.stStudentovNezakljucilo();
            return View();
        }
    }
}