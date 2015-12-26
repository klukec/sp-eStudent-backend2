using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;
using eStudentMVC5.Business;

namespace eStudentMVC5.Controllers
{
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
                }
                catch
                {
                    Log.Error("Izpitnega roka ni bilo mogoce pridobiti iz baze.");
                    return View(new RazpisiRokModel());
                }
            }
        }

        [HttpPost]
        public ActionResult Index(RazpisiRokModel p)
        {
            try
            {
                // dodajam nov izpitni rok
                if (p.izpitniRok.idIzpitniRok == 0)
                {
                    p.izpitniRok.zakljucen = false;
                    db.izpitnirok.Add(p.izpitniRok);
                    int uspeh = db.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    // slo je za vnos ocen obstojecemu izpitnemu roku, postimaj samo to
                    if (p.izpitniRok.komentar == null)
                    {
                        try
                        {
                            izpitnirok iTmp = db.izpitnirok.Find(p.izpitniRok.idIzpitniRok);
                            foreach (ocena o in p.seznamOcenVsiUporabniki)
                            {
                                // gre za novo oceno, vpisi jo v bazo
                                if (o.idOcena == 0)
                                {
                                    ocena nesto = new ocena(o, iTmp);
                                    db.ocena.Add(nesto);
                                    int uspehOcenaTmp = db.SaveChanges();
                                }
                                // popravljas staro oceno
                                else
                                {
                                    db.Entry(o).State = System.Data.Entity.EntityState.Modified;
                                    int uspehOcenaTmp = db.SaveChanges();
                                }
                            }
                            iTmp.zakljucen = true;
                            db.Entry(iTmp).State = System.Data.Entity.EntityState.Modified;
                            int uspehZakljucen = db.SaveChanges();
                        }
                        catch
                        {
                            Log.Error("Posodabljanje ocen v bazi ni uspelo.");
                        }
                    }
                    // spreminjali so se podatki o izpitnem roku
                    else
                    {
                        db.Entry(p.izpitniRok).State = System.Data.Entity.EntityState.Modified;
                        int uspeh = db.SaveChanges();
                    }
                }
            }
            catch
            {
                Log.Error("Izpitni rok ni bil vstavljen v bazo.");
            }
            return Redirect("RazpisaniRoki");
        }
    }
}