using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class IndeksController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: Indeks
        // http://www.asp.net/mvc/overview/older-versions-1/controllers-and-routing/improving-performance-with-output-caching-cs
        [OutputCache(Duration = 30, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Index()
        {
            try
            {
                uporabnik tren = (from s in db.uporabnik where s.email.Equals(User.Identity.Name) select s).ToList().First();
                List<ocena> opravljeniPredmeti = (from s in db.ocena where (s.ocena1 > 5 && s.idStudenta == tren.idUporabnik) select s).ToList();
                ViewBag.opravljeniPredmeti = opravljeniPredmeti;

                double stOpravljenih = opravljeniPredmeti.Count();
                ViewBag.stOpravljenih = stOpravljenih;

                int sumKreditne = 0;
                foreach (ocena i in opravljeniPredmeti)
                {
                    sumKreditne += i.predmet.stKreditnih;
                }
                ViewBag.sumKreditne = sumKreditne;

                double povprecje = 0;
                foreach (ocena i in opravljeniPredmeti)
                {
                    povprecje += i.ocena1;
                }
                povprecje = povprecje / stOpravljenih;
                ViewBag.povprecje = povprecje;
            }
            catch
            {
                Log.Error("Napaka pri izdelavi elektronskega indeksa.");
            }

            return View();
        }
    }
}