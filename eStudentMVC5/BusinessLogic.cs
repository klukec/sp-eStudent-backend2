using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStudentMVC5.Business
{
    public class BusinessLogic
    {
        private static estudentEntities db = new estudentEntities();

        public static List<predmet> vrniVsePredmete()
        {
            var predmeti = from s in db.predmet select s;
            List<predmet> predmetiR = predmeti.ToList();
            return predmetiR;
        }

        public static List<uporabnik> vrniVseStudente()
        {
            var studenti = from s in db.uporabnik where s.idVloge == 1 select s;
            List<uporabnik> studentiR = studenti.ToList();
            return studentiR;
        }

        public static List<ocena> vrniOceneRok(izpitnirok i)
        {
            var ocene = from s in db.ocena where s.idIzpitnegaRoka == i.idIzpitniRok select s;
            List<ocena> oceneR = ocene.ToList();
            return oceneR;
        }
    }
}