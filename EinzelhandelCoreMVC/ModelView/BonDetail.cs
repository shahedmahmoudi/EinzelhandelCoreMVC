using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.ModelView
{
    public class BonDetail
    {
        public int ID { get; set; }
   
    

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [DisplayName("Art Bon")]
        public bool Art { get; set; }

        public IEnumerable<SelectListItem> Kundes { get; set; }

        public int? KundeID { get; set; }
        public string KundeTitel { get; set; }

        [DisplayName("Rechnungsart")]
        public string ArtBon
        {
            get
            {
               return Art ? "Ankauf" : "Verkauf";                
            }
            set
            { }
        }
    }
}
