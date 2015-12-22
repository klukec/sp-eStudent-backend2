using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;

namespace eStudentMVC5.Controllers
{
    public class PredmetiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: Predmeti
        public ActionResult Index()
        {
            // Zgornja tabela predmetov.
            var predmeti = from s in db.predmet select s;
            List<predmet> predmetiR = predmeti.ToList();

            // Spodnje polje za urejanje predmeta.
            var predmet = new predmet();
            predmet.seznamIzvajalcev = VrniVseProfesorje();
            //return View(predmet);

            var model = new PredmetiModel { seznamPredmetov = predmetiR, predmetEdit = predmet };
            return View(model);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(PredmetiModel p)
        {
            if (ModelState.IsValid)
            {
                if (p.predmetEdit.stKreditnih > 0)
                {
                    try
                    {
                        db.predmet.Add(p.predmetEdit);
                        int id = db.SaveChanges();
                        ModelState.Clear();
                    }
                    catch
                    {
                        Console.WriteLine("Predmet ni bil vstavljen v bazo.");
                    }
                }
                else
                {
                    Console.WriteLine("Predmet mora imeti veljavno stevilo kreditnih tock.");
                }
            }
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> VrniVseProfesorje()
        {
            var prof = from s in db.uporabnik where s.idVloge.Equals(2) select s;
            List<uporabnik> profesorji = prof.ToList();

            IEnumerable<SelectListItem> selectProfesor = from c in profesorji
                                                         select new SelectListItem
                                                         {
                                                             Selected = (c.idUporabnik == 0),
                                                             Text = c.ime + " " + c.priimek,
                                                             Value = c.idUporabnik.ToString()
                                                         };
            return selectProfesor;
        }
    }
}