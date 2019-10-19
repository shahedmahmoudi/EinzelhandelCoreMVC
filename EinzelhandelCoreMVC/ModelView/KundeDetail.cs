using EinzelhandelCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static EinzelhandelCoreMVC.Models.Gender;

namespace EinzelhandelCoreMVC.ModelView
{
    public class KundeDetail
    {
       

        public int ID { get; set; }

        [Required(ErrorMessage = "Titel ist erforderlich")]
        [MaxLength(50, ErrorMessage = "Vorname darf nicht länger als 50 Zeichen sein")]

        public string Vorname { get; set; }

        [Required(ErrorMessage = "Titel ist erforderlich")]
        [MaxLength(50, ErrorMessage = "Nachname darf nicht länger als 50 zeichen sein")]

        public string Nachname { get; set; }

        [MaxLength(50, ErrorMessage = "Ruffnumer darf nicht länger als 50 zeichen sein")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Ungültige Telefonnummer!")]
        public string Rufnummer { get; set; }

        [MaxLength(500)]
        public string Adresse { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        public Gender Geschlecht { get; set; }
        public List<SelectListItem> Geschlechts { get; set; }
        
        public KundeDetail( )
        {
            Geschlechts = new List<SelectListItem>();
            Geschlechts.Add(new SelectListItem { Value = ((int)Gender.Weiblich).ToString(), Text = Gender.Weiblich.ToString() });
            Geschlechts.Add(new SelectListItem { Value = ((int)Gender.männlich).ToString(), Text = Gender.männlich.ToString() });           
        }
        [DisplayName("KundeName")]
        public string FullName
        {
            get
            {
                return Vorname + " " + Nachname;
            }
            set { }
        }
    }
}
