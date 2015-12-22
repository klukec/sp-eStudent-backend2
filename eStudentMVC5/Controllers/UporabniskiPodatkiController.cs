using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    public class UporabniskiPodatkiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: UporabniskiPodatki
        public ActionResult Index(int idUporabnik = 0)
        {
            if (idUporabnik == 0)
            {
                return View(new uporabnik());
            }
            else
            {
                uporabnik u = db.uporabnik.Find(idUporabnik);
                return View(u);
            }  
        }

        [HttpPost]
        public ActionResult Index(uporabnik u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (u.idUporabnik != 0)
                    {
                        db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                        int uspeh = db.SaveChanges();
                    }
                    else
                    {
                        Log.Error("Zahtevan je bil vnos novega uporabnika.");
                    }

                }
                catch
                {
                    Log.Error("Uporabnik ni bil posodobljen.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Odpravite napake v obrazcu.");
                return View(u);
            }
            return RedirectToAction("Index");
        }
    }
}