using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.Models
{
    public class Detail
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int ID { get; set; }

        [Required(ErrorMessage ="Zahl ist erforderlich")]
        [RegularExpression("^[0-9]+$")]
        public int Zahl { get; set; }

        [DisplayFormat(DataFormatString = "{0:n} €")]
        [RegularExpression("^(?:10000|(?:(?:(?:2[5-9]\\d)|[3-9]\\d{2}|\\d{4})(?:[,.]\\d{2})?))€?$")]
        public float Preis { get; set; }


        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]     
        public decimal Ermäßigung { get; set; }

        public virtual Produkt Produkt { get; set; }
        public virtual Bon Bon { get; set; }
    }
}
