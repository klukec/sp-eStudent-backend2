using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class RazpisaniRokiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: RazpisaniRoki
        public ActionResult Index()
        {
            try
            {
                List<izpitnirok> izpitniRoki = (from s in db.izpitnirok select s).ToList();
                ViewBag.izpitniRoki = izpitniRoki;
            }
            catch
            {
                Log.Error("Napaka pri pripravi izpitnih rokov.");
            }

            return View();
        }

        public ActionResult IzbrisiRok(int idRoka)
        {
            try
            {
                izpitnirok temp = db.izpitnirok.Find(idRoka);
                db.izpitnirok.Remove(temp);
                db.SaveChanges();
            }
            catch
            {
                Log.Error("Napaka pri izbrisu izpitnega roka.");
            }
            return RedirectToAction("Index");
        }
    }
}