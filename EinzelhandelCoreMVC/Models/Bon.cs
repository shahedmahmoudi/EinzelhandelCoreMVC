using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.Models
{
    public class Bon
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }


        public bool Art { get; set; }

        public virtual ICollection<Detail> Detail { get; set; }
        public virtual Kunde Kunde { get; set; }


    }
}
