//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eStudentMVC5
{
    using System;
    using System.Collections.Generic;
    
    public partial class ocena
    {
        public int idOcena { get; set; }
        public int idStudenta { get; set; }
        public int idPredmeta { get; set; }
        public int idIzpitnegaRoka { get; set; }
        public int sTock { get; set; }
        public int ocena1 { get; set; }
    
        public virtual izpitnirok izpitnirok { get; set; }
        public virtual predmet predmet { get; set; }
        public virtual uporabnik uporabnik { get; set; }
    }
}
