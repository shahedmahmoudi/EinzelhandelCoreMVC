using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
 
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.Models
{
    public class Produkt
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Titel ist erforderlich")]
        [MaxLength(50,ErrorMessage = "Der Titel darf nicht länger als 50 Zeichen sein")]
        [DisplayName("Produktart Name")]
        public string Titel { get; set; }

        
        public int Zahl { get; set; }

        public virtual Produktart Produktart { get; set; }
        public virtual ICollection<Detail> Detail { get; set; }
    }
}
