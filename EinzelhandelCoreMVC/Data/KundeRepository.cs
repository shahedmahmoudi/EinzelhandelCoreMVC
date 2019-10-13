using EinzelhandelCoreMVC.Models;
using EinzelhandelCoreMVC.ModelView;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.Data
{
    public class KundeRepository
    {
        private readonly MVCEinzelhandelContext _context;
        public KundeRepository(MVCEinzelhandelContext context)
        {
            _context = context;
        }

        internal List<KundeDetail> GetKundeList()
        {
            List<Kunde> Kundes = new List<Kunde>();
            Kundes = _context.Kunde.AsNoTracking().ToList();
            if (Kundes != null)
            {
                List<KundeDetail> kundesDetail = new List<KundeDetail>();
                foreach (var item in Kundes)
                {
                    var kDetail = new KundeDetail()
                    {
                        ID = item.ID,
                         Adresse= item.Adresse,
                        Email = item.Email,
                        FullName = item.Vorname+" "+item.Nachname,
                        Rufnummer=item.Rufnummer,
                        Geschlecht=item.Geschlecht
                    };
                    kundesDetail.Add(kDetail);
                }
                return kundesDetail;
            }
            return null;
        }

        internal IEnumerable<SelectListItem> GetKundeSelectList()
        {
            List<Kunde> lK = _context.Kunde.ToList();
            List<SelectListItem> lSelect= _context.Kunde.AsNoTracking()
                    .OrderBy(n => n.Vorname)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.ID.ToString(),
                            Text = n.Vorname+" "+n.Nachname
                        }).ToList();
            var kundelist = new SelectListItem()
            {
                Value = null,
                Text = "Bitte Kunden auswählene"
            };
            lSelect.Insert(0, kundelist);
            return new SelectList(lSelect, "Value", "Text");
        }
    }
}
