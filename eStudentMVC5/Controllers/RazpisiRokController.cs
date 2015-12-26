using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;

namespace eStudentMVC5.Controllers
{
    // sam izracunaj stevilko izpitnega rok
    // glede ocen boms moral malo prilagodit konstruktor

    // Ce je rok prazen, studentov sploh ne potrebujemo. St. roka je 1. Pridobi seznam vseh predmetov.
    // Ce gre za izbrani rok in se ni zakljucen, pridobi tocno dolocen rok, seznam vseh predmetov, seznam vseh studentov, nic ocen.
    // Ce gre za rok, ki je zakljucen, pridobi tocno dolocen rok, vse ocene, ki so že vnesese in študente, ki še nimajo vnesene ocene.


    [Authorize]
    public class RazpisiRokController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: RazpisiRok
        public ActionResult Index(int idRoka = 0)
        {
            if (idRoka == 0)
            {
                return View(new RazpisiRokModel());
            }
            else
            {
                try
                {
                    izpitnirok p = db.izpitnirok.Find(idRoka);
                    return View(new RazpisiRokModel(p));
                } catch {
                    Log.Error("Izpitnega roka ni bilo mogoce pridobiti iz baze.");
                    return View(new RazpisiRokModel());
                }
            }  
        }

        [HttpPost]
        public ActionResult Index(RazpisiRokModel p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (p.izpitniRok.idIzpitniRok == 0)
                    {
                        db.izpitnirok.Add(p.izpitniRok);
                        int uspeh = db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        // spremenili so se podatki o izpitnem roku ali / in vnesle so se ocene
                        db.Entry(p.izpitniRok).State = System.Data.Entity.EntityState.Modified;
                        int uspeh = db.SaveChanges();
                    }
                }
                catch
                {
                    Log.Error("Izpitni rok ni bil vstavljen v bazo.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Odpravite napake v obrazcu.");
                return View(new RazpisiRokModel(p.izpitniRok));
            }
            return RedirectToAction("Index");
        }
    }
}