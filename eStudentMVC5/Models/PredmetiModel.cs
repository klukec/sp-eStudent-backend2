using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace eStudentMVC5.Models
{
    public class PredmetiModel
    {
        public List<predmet> seznamPredmetov { get; set; }
        public predmet predmetEdit { get; set; }
    }
}