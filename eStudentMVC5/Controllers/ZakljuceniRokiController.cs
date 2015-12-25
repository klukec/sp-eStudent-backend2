using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class ZakljuceniRokiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: ZakljuceniRoki
        public ActionResult Index()
        {
            try
            {
                List<izpitnirok> izpitniRoki = (from s in db.izpitnirok select s).ToList();
                
                List<ZakljuceniRokiModel> m = new List<ZakljuceniRokiModel>();
                foreach(izpitnirok i in izpitniRoki) {
                    m.Add(new ZakljuceniRokiModel(i.predmet, i, i.ocena.ToList()));
                }
                ViewBag.izpitniRoki = m;
            }
            catch
            {
                Log.Error("Napaka pri pripravi zakljucenih izpitnih rokov.");
            }

            return View();
        }
    }
}