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
    public class BonRepository
    {
        private readonly MVCEinzelhandelContext _context;
        public BonRepository(MVCEinzelhandelContext context)
        {
            _context = context;
        }

        internal List<BonDetail> GetBonList()
        {
            List<Bon> bons = new List<Bon>();
            bons = _context.Bon.AsNoTracking()
                .Include(x => x.Kunde)
                .ToList();
            if (bons != null)
            {
                List<BonDetail> bonDetail = new List<BonDetail>();
                foreach (var item in bons)
                {
                    var bDetail = new BonDetail()
                    {
                        ID = item.ID,
                        KundeTitel = item.Kunde.Vorname+" "+ item.Kunde.Nachname,
                        Datum = item.Datum,
                        Art=item.Art
                        
                        
                        
                    };
                    bonDetail.Add(bDetail);
                }
                return bonDetail;
            }
            return null;
        }

        internal string GetKundeTitel(int iD)
        {
            Kunde k = _context.Kunde.Find(iD);
            return k.Vorname + " " + k.Nachname;
        }
    }
}
