using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Business;

namespace eStudentMVC5.Models
{
    public class RazpisiRokModel
    {
        estudentEntities db = new estudentEntities();

        public izpitnirok izpitniRok { get; set; }
        public List<ocena> seznamOcenVsiUporabniki { get; set; }
        public IEnumerable<SelectListItem> seznamPredmetov {
            get
            {
                return BusinessLogic.VrniVsePredmete();
            }
        }


        // Ustvaris nov objekt, ko gre za prazen (nov) rok.
        public RazpisiRokModel()
        {
            this.izpitniRok = new izpitnirok();
            this.izpitniRok.stRoka = 1;
            this.seznamOcenVsiUporabniki = vrniSeznamOcenVsiUporabniki(this.izpitniRok);
        }

        // Ce gre za izbrani izpitni rok.
        public RazpisiRokModel(izpitnirok i)
        {
            this.izpitniRok = i;
            this.seznamOcenVsiUporabniki = vrniSeznamOcenVsiUporabniki(i);
        }

        public List<ocena> vrniSeznamOcenVsiUporabniki(izpitnirok i)
        {
            List<uporabnik> vsiStudentje = BusinessLogic.vrniVseStudente();
            List<ocena> ocene = new List<ocena>();
            foreach (uporabnik u in vsiStudentje)
            {
                try
                {
                    ocena ocenaTmp = (from s in db.ocena where (s.idStudenta == u.idUporabnik && s.idIzpitnegaRoka == i.idIzpitniRok) select s).ToList().First();
                    ocene.Add(ocenaTmp);
                }
                catch
                {
                    // Uporabnik se nima ocene - kreiraj prazno oceno za uporabnika
                    ocena ocenaTmp = new ocena();
                    ocenaTmp.idStudenta = u.idUporabnik;
                    ocenaTmp.uporabnik = u;
                    ocenaTmp.idPredmeta = i.idPredmeta;
                    ocenaTmp.predmet = i.predmet;
                    ocenaTmp.idIzpitnegaRoka = i.idIzpitniRok;
                    ocenaTmp.izpitnirok = i;
                    ocene.Add(ocenaTmp);
                }
            }
            return ocene;
        }

    }
}