using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Business
{
    public class BusinessLogic
    {
        private static estudentEntities db = new estudentEntities();

        public static uporabnik vrniUporabnika(int id)
        {
            try
            {
                uporabnik u = db.uporabnik.Find(id);
                return u;
            }
            catch
            {
                return null;
            }
        }

        public static List<uporabnik> vrniVseStudente()
        {
            var studenti = from s in db.uporabnik where s.idVloge == 1 select s;
            List<uporabnik> studentiR = studenti.ToList();
            return studentiR;
        }

        public static List<int> vrniVseIdStudentov()
        {
            var studenti = from s in db.uporabnik
                           where s.idVloge == 1
                           select new
                               {
                                   idUporabnika = s.idUporabnik
                               };
            List<int> studentiR = studenti.AsEnumerable().Cast<int>().ToList();
            return studentiR;
        }

        public static IEnumerable<SelectListItem> VrniVsePredmete()
        {
            try
            {
                var predmeti = from s in db.predmet select s;
                List<predmet> predmetiR = predmeti.ToList();

                IEnumerable<SelectListItem> selectPredmet = from c in predmetiR
                                                            select new SelectListItem
                                                            {
                                                                Selected = (c.idPredmet == 0),
                                                                Text = c.imePredmeta,
                                                                Value = c.idPredmet.ToString()
                                                            };
                return selectPredmet;
            }
            catch
            {
                return null;
            }
        }

        public static List<ocena> vrniOceneRok(izpitnirok i)
        {
            var ocene = from s in db.ocena where s.idIzpitnegaRoka == i.idIzpitniRok select s;
            List<ocena> oceneR = ocene.ToList();
            return oceneR;
        }

        public static List<uporabnik> vrniNeocenjeneStudente(izpitnirok i)
        {
            var query = from s in db.ocena
                        where s.idIzpitnegaRoka == i.idIzpitniRok
                        select new
                        {
                            idUporabnika = s.idStudenta
                        };
            List<int> ocenjeni = query.AsEnumerable().Cast<int>().ToList();

            IEnumerable<int> differenceQuery = vrniVseIdStudentov().Except(ocenjeni);
            List<int> neocenjeni = differenceQuery.ToList();

            List<uporabnik> neocenjeniU = new List<uporabnik>();
            foreach (int u in neocenjeni)
            {
                var usr = from s in db.uporabnik where s.idUporabnik == u select s;
                neocenjeniU.Add((uporabnik)usr);
            }
            return neocenjeniU;
        }

        public static int stIzpitnihRokov()
        {
            List<izpitnirok> roki = (from s in db.izpitnirok select s).ToList();
            return roki.Count();
        }

        public static int stNezakljucenihRokov()
        {
            List<izpitnirok> roki = (from s in db.izpitnirok where s.zakljucen == false select s).ToList();
            return roki.Count();
        }

        public static int stVsehStudentov()
        {
            List<uporabnik> studenti = vrniVseStudente();
            return studenti.Count();
        }

        public static int stPredmetov()
        {
            List<predmet> predmeti = (from s in db.predmet select s).ToList();
            return predmeti.Count();
        }

        public static int stStudentovZakljucilo()
        {
            int vsiPredmeti = stPredmetov();

            int stStudFinish = 0;
            List<uporabnik> vsiStudentje = vrniVseStudente();
            foreach (uporabnik u in vsiStudentje)
            {
                List<ocena> opravljeniPredmeti = (from s in db.ocena where (s.ocena1 > 5 && s.idStudenta == u.idUporabnik) select s).ToList();
                int stOpravljenih = opravljeniPredmeti.Count();
                if (stOpravljenih == vsiPredmeti)
                    stStudFinish++;
            }
            return stStudFinish;
        }

        public static int stStudentovNezakljucilo()
        {
            return stVsehStudentov() - stStudentovZakljucilo();
        }
  
    }
}