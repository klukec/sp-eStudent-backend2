//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eStudent2
{
    using System;
    using System.Collections.Generic;
    
    public partial class uporabnik
    {
        public uporabnik()
        {
            this.ocena = new HashSet<ocena>();
            this.predmet = new HashSet<predmet>();
            this.studentpredmet = new HashSet<studentpredmet>();
        }
    
        public int idUporabnik { get; set; }
        public Nullable<int> vpisnaStevilka { get; set; }
        public string ime { get; set; }
        public string priimek { get; set; }
        public string email { get; set; }
        public string geslo { get; set; }
        public string mobi { get; set; }
        public string spol { get; set; }
        public Nullable<int> letnikStudija { get; set; }
        public System.DateTime datumRegistracije { get; set; }
        public System.DateTime zadnjiDostop { get; set; }
        public int idVloge { get; set; }
    
        public virtual ICollection<ocena> ocena { get; set; }
        public virtual ICollection<predmet> predmet { get; set; }
        public virtual ICollection<studentpredmet> studentpredmet { get; set; }
        public virtual vloga vloga { get; set; }
    }
}
