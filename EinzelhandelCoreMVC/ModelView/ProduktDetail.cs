using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.ModelView
{
    public class ProduktDetail
    {

        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Titel ist erforderlich")]
        [MaxLength(50, ErrorMessage = "Der Titel darf nicht länger als 50 Zeichen sein")]
        [DisplayName("Produktart Name")]
        public string Titel { get; set; }


        public int Zahl { get; set; }

        [Required]
        [Display(Name = "Produktart")]
        public int ProduktartID { get; set; }
        public string ProduktartTitel { get; set; }
        public IEnumerable<SelectListItem> Produktarts { get; set; }


    }
}