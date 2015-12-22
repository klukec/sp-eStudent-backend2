using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class UporabniskiPodatkiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: UporabniskiPodatki
        public ActionResult Index(int idUporabnik = 0)
        {
            try
            {
                uporabnik tren = (from s in db.uporabnik where s.email.Equals(User.Identity.Name) select s).ToList().First();
                if (idUporabnik == 0)
                {
                    return View(tren);
                }
                else
                {
                    uporabnik u = db.uporabnik.Find(idUporabnik);
                    return View(u);
                }  
            }
            catch
            {
                Log.Error("Prislo je do napake pri ugotavljanju uporabnika.");
            }
            return View(new uporabnik());
        }

        [HttpPost]
        public ActionResult Index(uporabnik u, string poslji)
        {
            try
                {
                    if (u.idUporabnik != 0)
                    {
                        uporabnik tmp;
                        if (poslji.Equals("Shrani osebne podatke"))
                        {
                             tmp = PosodobiUporabnikaOsebno(db.uporabnik.Find(u.idUporabnik), u);
                        }
                        else
                        {
                            tmp = PosodobiUporabnikaSolanje(db.uporabnik.Find(u.idUporabnik), u);
                        }
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
            return RedirectToAction("Index");
        }

        public uporabnik PosodobiUporabnikaOsebno(uporabnik izBaze, uporabnik nov)
        {
            izBaze.ime = nov.ime;
            izBaze.priimek = nov.priimek;
            izBaze.mobi = nov.mobi;
            izBaze.spol = nov.spol;
            izBaze.vloga = nov.vloga;
            return izBaze;
        }

        public uporabnik PosodobiUporabnikaSolanje(uporabnik izBaze, uporabnik nov)
        {
            izBaze.vpisnaStevilka = nov.vpisnaStevilka;
            izBaze.letnikStudija = nov.letnikStudija;
            return izBaze;
        }
    }
}