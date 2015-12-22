using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;
using System.Diagnostics;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class PredmetiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: Predmeti
        public ActionResult Index(string idPredmet = "")
        {
            if (idPredmet.Length == 0)
            {
                var model = new PredmetiModel { seznamPredmetov = vrniVsePredmete(), predmetEdit = new predmet() };
                return View(model);
            }
            else
            {
                predmet p = db.predmet.Find(Int32.Parse(idPredmet));
                var model = new PredmetiModel { seznamPredmetov = vrniVsePredmete(), predmetEdit = p };
                return View(model);
            }  
        }

        [HttpPost]
        public ActionResult Index(PredmetiModel p)
        {
            if (ModelState.IsValid) // Ali ustreza konstraintom v modelu?
            {
                try
                {
                    if (p.predmetEdit.idPredmet == 0)
                    {
                        db.predmet.Add(p.predmetEdit);
                        int uspeh = db.SaveChanges();
                        int newId = p.predmetEdit.idPredmet;
                        Log.Info("Nov predmet ima ID: " + newId);
                        ModelState.Clear();
                    }
                    else
                    {
                        db.Entry(p.predmetEdit).State = System.Data.Entity.EntityState.Modified;
                        int uspeh = db.SaveChanges();
                    }
                }
                catch
                {
                    Log.Error("Predmet ni bil vstavljen v bazo.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Odpravite napake v obrazcu.");
                Log.Debug("Predmetu je bilo nastavljeno " + p.predmetEdit.stKreditnih + " kreditnih tock.");
                var model = new PredmetiModel { seznamPredmetov = vrniVsePredmete(), predmetEdit = p.predmetEdit };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        private List<predmet> vrniVsePredmete()
        {
            var predmeti = from s in db.predmet select s;
            List<predmet> predmetiR = predmeti.ToList();
            return predmetiR;
        }
    }
}