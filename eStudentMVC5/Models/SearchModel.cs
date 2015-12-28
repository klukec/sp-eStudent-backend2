using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStudentMVC5.Models
{
    public class SearchModel
    {
        public string query { get; set; }
        
        public List<uporabnik> studentje { get; set; }
        public List<uporabnik> zaposleni { get; set; }
        public List<predmet> predmeti { get; set; }
        public List<izpitnirok> izpitniRoki { get; set; }

        public int stStudentov { get; set; }
        public int stZaposlenih { get; set; }
        public int stPredmetov { get; set; }
        public int stIzpitnihRokov { get; set; }

        
        public SearchModel() 
        {
            this.query = "";
            this.stStudentov = 0;
            this.stZaposlenih = 0;
            this.stPredmetov = 0;
            this.stIzpitnihRokov = 0;
        }

        public SearchModel(List<uporabnik> s, List<uporabnik> z, List<predmet> p, List<izpitnirok> i)
        {
            this.studentje = s;
            this.zaposleni = z;
            this.predmeti = p;
            this.izpitniRoki = i;

            this.stStudentov = s.Count();
            this.stZaposlenih = z.Count();
            this.stPredmetov = p.Count();
            this.stIzpitnihRokov = i.Count();
        }
    }
}