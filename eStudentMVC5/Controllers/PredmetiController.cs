using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;
using eStudentMVC5.Business;
using System.Diagnostics;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class PredmetiController : Controller
    {
        estudentEntities dbl = new estudentEntities();

        // GET: Predmeti
        // http://dotnetcodr.com/2013/02/07/caching-infrastructure-in-mvc4-with-c-caching-controller-actions/
        // [OutputCache(Duration = 60, VaryByParam = "idPredmet")]
        [OutputCache(CacheProfile = "CacheEstudent")]
        public ActionResult Index(string idPredmet = "")
        {
            if (idPredmet.Length == 0)
            {
                var model = new PredmetiModel { seznamPredmetov = vrniVsePredmete(), predmetEdit = new predmet() };
                return View(model);
            }
            else
            {
                predmet p = dbl.predmet.Find(Int32.Parse(idPredmet));
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
                        dbl.predmet.Add(p.predmetEdit);
                        int uspeh = dbl.SaveChanges();
                        int newId = p.predmetEdit.idPredmet;
                        Log.Info("Nov predmet ima ID: " + newId);
                        ModelState.Clear();
                    }
                    else
                    {
                        dbl.Entry(p.predmetEdit).State = System.Data.Entity.EntityState.Modified;
                        int uspeh = dbl.SaveChanges();
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

        [OutputCache(Duration = 86400)]
        public ActionResult Urejaj()
        {
            ViewData["avgKrediti"] = BusinessLogic.povprecnoStKreditov();
            return View();
        }

        public List<predmet> vrniVsePredmete()
        {
            var predmeti = from s in dbl.predmet select s;
            List<predmet> predmetiR = predmeti.ToList();
            return predmetiR;
        }
    }
}