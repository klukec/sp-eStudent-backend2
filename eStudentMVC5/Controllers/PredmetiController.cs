using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    public class PredmetiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // GET: Predmeti
        public ActionResult Index()
        {
            var predmet = new predmet();
            predmet.seznamIzvajalcev = VrniVseProfesorje();
            return View(predmet);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(predmet p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.predmet.Add(p);
                    int id = db.SaveChanges();
                    ModelState.Clear();
                }
                catch
                {
                    Console.WriteLine("Predmet ni bil vstavljen v bazo.");
                }
            }

            var predmet = new predmet();
            predmet.seznamIzvajalcev = VrniVseProfesorje();
            return View(predmet);
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