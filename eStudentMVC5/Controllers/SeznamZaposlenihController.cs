using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class SeznamZaposlenihController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: SeznamZaposlenih
        public ActionResult Index()
        {
            var employees = from s in db.uporabnik where !s.idVloge.Equals(1) select s;
            return View(employees.ToList());
        }
    }
}