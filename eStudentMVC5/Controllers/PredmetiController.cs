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
            // Spodnja forma za dodajanje oz. urejanje predmeta.
            var prof = from s in db.uporabnik where s.idVloge.Equals(2) select s;
            List<uporabnik> profesorji = prof.ToList();
            IEnumerable<SelectListItem> selectProfesor = from c in profesorji select new SelectListItem
                {
                    Selected = (c.idUporabnik == 0),
                    Text = c.ime + " " + c.priimek,
                    Value = c.idUporabnik.ToString()
                };
            ViewData["profesorji"] = selectProfesor;

            // Zgornja tabela predmetov.
            var predmeti = from s in db.predmet select s;
            List<predmet> predmetiR = predmeti.ToList();

            // Vrni Tuple na View.
            var tuple = new Tuple<List<predmet>, predmet>(predmetiR, new predmet());
            return View(tuple);

        }

        [HttpPost]
        public ActionResult Posodobi(predmet p)
        {
            return View(); 
        }

    }
}