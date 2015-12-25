using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStudentMVC5.Models
{
    public class ZakljuceniRokiModel
    {
        public predmet predmet { get; set; }
        public izpitnirok izpitnirok { get; set; }
        public List<ocena> ocene { get; set; }
        public int pozitivno { get; set; }
        public int negativno { get; set; }
        public int skupaj { get; set; }
        public int procenti { get; set; }

        public ZakljuceniRokiModel()
        {
        }

        public ZakljuceniRokiModel(predmet p, izpitnirok i, List<ocena> o)
        {
            this.predmet = p;
            this.izpitnirok = i;
            this.ocene = o;
            this.pozitivno = StPozitivnih(o);
            this.negativno = StNegativnih(o);
            this.skupaj = o.Count();
            this.procenti = VrniProcente(this.pozitivno, this.skupaj);
        }

        public int StPozitivnih(List<ocena> all) {
            int res = 0;
            foreach (ocena o in all)
            {
                if(o.ocena1 > 5)
                    res++;
            }
            return res;
        }

        public int StNegativnih(List<ocena> all)
        {
            int res = 0;
            foreach (ocena o in all)
            {
                if (o.ocena1 < 6)
                    res++;
            }
            return res;
        }

        public int VrniProcente(int pozitivni, int vsi)
        {
            try
            {
                double res = (double)pozitivni / vsi * 100;
                int noDecimal = Convert.ToInt32(res);
                return noDecimal;
            }
            catch
            {
                return 0;
            }
        }
    }
}