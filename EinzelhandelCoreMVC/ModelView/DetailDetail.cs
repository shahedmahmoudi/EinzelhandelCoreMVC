using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.ModelView
{
    public class DetailDetail
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Zahl ist erforderlich")]
        [RegularExpression("^[0-9]+$")]
        public int Zahl { get; set; }

        [DisplayFormat(DataFormatString = "{0:n} €")]
        [RegularExpression("^(?:10000|(?:(?:(?:2[5-9]\\d)|[3-9]\\d{2}|\\d{4})(?:[,.]\\d{2})?))€?$")]
        public float Preis { get; set; }


        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public decimal Ermäßigung { get; set; }

       // public int BonNumber { get; set; }
        public string KundeVorname{ get; set; }
        public string KundeNachname { get; set; }
        public DateTime Datum { get; set; }
        public int BonID { get; set; }
        public string FullName
        {
            get
            {
                return KundeVorname + " " + KundeNachname;
            }
            set { }
        }
        [Required]
        [Display(Name = "Produkt")]
        public int ProduktID { get; set; }
        public string ProduktTitel { get; set; }
        public IEnumerable<SelectListItem> Produkts { get; set; }


        public int ProduktartID { get; set; }
        public string ProduktartTitel { get; set; }
        public IEnumerable<SelectListItem> Produktarts { get; set; }

    }
}
