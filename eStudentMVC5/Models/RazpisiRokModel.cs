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
            this.seznamOcenVsiUporabniki = BusinessLogic.vrniSeznamOcenVsiUporabniki(this.izpitniRok);
        }

        // Ce gre za izbrani izpitni rok.
        public RazpisiRokModel(izpitnirok i)
        {
            this.izpitniRok = i;
            this.seznamOcenVsiUporabniki = BusinessLogic.vrniSeznamOcenVsiUporabniki(i);
        }

    }
}