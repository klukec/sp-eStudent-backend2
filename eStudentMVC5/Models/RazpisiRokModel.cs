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
        public List<uporabnik> seznamStudentov { get; set; }
        public List<ocena> ocene { get; set; }

        public RazpisiRokModel(izpitnirok i)
        {
            this.izpitniRok = i;
            this.seznamPredmetov = BusinessLogic.vrniVsePredmete();
            this.seznamStudentov = BusinessLogic.vrniVseStudente();
            this.ocene = BusinessLogic.vrniOceneRok(i);
        }

        public RazpisiRokModel()
        {
            this.seznamPredmetov = BusinessLogic.vrniVsePredmete();
            this.seznamStudentov = BusinessLogic.vrniVseStudente();
        }
    }
}