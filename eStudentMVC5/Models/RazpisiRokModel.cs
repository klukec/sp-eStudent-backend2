using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStudentMVC5.Business;

namespace eStudentMVC5.Models
{
    public class RazpisiRokModel
    {
        public izpitnirok izpitniRok { get; set; }
        public List<predmet> seznamPredmetov { get; set; }
        public List<ocena> seznamOcenVsiUporabniki { get; set; }

        // Ustvaris nov objekt, ko gre za prazen (nov) rok.
        public RazpisiRokModel()
        {
            this.izpitniRok = new izpitnirok();
            this.izpitniRok.stRoka = 1;
            this.seznamPredmetov = BusinessLogic.vrniVsePredmete();
            this.seznamOcenVsiUporabniki = BusinessLogic.vrniSeznamOcenVsiUporabniki(this.izpitniRok);
        }

        // Ce gre za izbrani izpitni rok.
        public RazpisiRokModel(izpitnirok i)
        {
            this.izpitniRok = i;
            this.seznamPredmetov = BusinessLogic.vrniVsePredmete();
            this.seznamOcenVsiUporabniki = BusinessLogic.vrniSeznamOcenVsiUporabniki(i);
        }

    }
}