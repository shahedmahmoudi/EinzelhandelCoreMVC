using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.Models
{
    public class Produktart
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Titel ist erforderlich.")]
        [MaxLength(50,ErrorMessage = "Der Titel darf nicht länger als 50 Zeichen sein")]
        [DisplayName("Produktart Titel")]
        public string Titel { get; set; }

        public virtual ICollection<Produkt> Produkts { get; set; }
    }
}
