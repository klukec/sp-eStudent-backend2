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

        [OutputCache(CacheProfile = "CacheEstudent")]
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
            try { 
            var studenti = from s in db.uporabnik where s.idVloge == 1 select s;
            List<uporabnik> studentiR = studenti.ToList();
            return studentiR;
            }
            catch
            {
                return null;
            }
        }

        public static List<int> vrniVseIdStudentov()
        {
            try
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
            catch
            {
                return null;
            }
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
            try
            {
                var ocene = from s in db.ocena where s.idIzpitnegaRoka == i.idIzpitniRok select s;
                List<ocena> oceneR = ocene.ToList();
                return oceneR;
            }
            catch
            {
                return null;
            }
        }

        public static List<uporabnik> vrniNeocenjeneStudente(izpitnirok i)
        {
            try
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
            catch
            {
                return null;
            }
        }

        public static int stIzpitnihRokov()
        {
            try
            {
                List<izpitnirok> roki = (from s in db.izpitnirok select s).ToList();
                return roki.Count();
            }
            catch
            {
                return -1;
            }
        }

        public static int stNezakljucenihRokov()
        {
            try
            {
                List<izpitnirok> roki = (from s in db.izpitnirok where s.zakljucen == false select s).ToList();
                return roki.Count();
            }
            catch
            {
                return -1;
            }
        }

        public static int stVsehStudentov()
        {
            try
            {
                List<uporabnik> studenti = vrniVseStudente();
                return studenti.Count();
            }
            catch
            {
                return -1;
            }
        }

        public static int stPredmetov()
        {
            try
            {
                List<predmet> predmeti = (from s in db.predmet select s).ToList();
                return predmeti.Count();
            }
            catch
            {
                return -1;
            }
        }

        public static int stStudentovZakljucilo()
        {
            try
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
            catch
            {
                return -1;
            }
        }

        public static int stStudentovNezakljucilo()
        {
            try
            {
                return stVsehStudentov() - stStudentovZakljucilo();
            }
            catch
            {
                return -1;
            }
        }

        public static double povprecnoStKreditov()
        {
            try
            {
                List<predmet> predmeti = (from s in db.predmet select s).ToList();
                double res = 0;
                foreach (predmet p in predmeti)
                {
                    res += p.stKreditnih;
                }
                res = res / predmeti.Count();
                return res;
            }
            catch
            {
                return -1;
            }
        }
  
    }
}