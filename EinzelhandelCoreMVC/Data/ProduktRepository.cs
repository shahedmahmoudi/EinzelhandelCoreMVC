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
    public class ProduktartRepository
    {
        private readonly MVCEinzelhandelContext _context;
        public ProduktartRepository(MVCEinzelhandelContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetproduktartSelectList()
        {
            List < Produktart > PP= _context.Produktart.ToList();
                List<SelectListItem> produktart = _context.Produktart.AsNoTracking()
                    .OrderBy(n => n.Titel)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.ID.ToString(),
                            Text = n.Titel
                        }).ToList();
                var Produktarttip = new SelectListItem()
                {
                    Value = null,
                    Text = "Wählen Sie den Produktart"
                };
                produktart.Insert(0, Produktarttip);
                return new SelectList(produktart, "Value", "Text");           
        }

        public IEnumerable<SelectListItem> GetProdukts()
        {
            List<SelectListItem> produkts = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return produkts;
        }

        public IEnumerable<SelectListItem> GetProdukts(int id)
        {
            IEnumerable<SelectListItem> regions = _context.Produkt.AsNoTracking()
                .OrderBy(n => n.Titel)
                .Where(n => n.Produktart.ID == id)
                .Select(n =>
                   new SelectListItem
                   {
                       Value = n.ID.ToString(),
                       Text = n.Titel
                   }).ToList();
            return new SelectList(regions, "Value", "Text");
        }

        internal List<ProduktDetail> GetProduktList()
        {
            List<Produkt> produkts = new List<Produkt>();
            produkts = _context.Produkt.AsNoTracking()
                .Include(x => x.Produktart)
                .ToList();
            if (produkts != null)
            {
                List<ProduktDetail> produktsDetail = new List<ProduktDetail>();
                foreach (var item in produkts)
                {
                    var pDetail = new ProduktDetail()
                    {
                        ID = item.ID,
                        ProduktartTitel = item.Produktart.Titel,
                        Titel = item.Titel,
                        Zahl = item.Zahl
                    };
                    produktsDetail.Add(pDetail);
                }
                return produktsDetail;
            }
            return null;
        }

        internal int GetproduktartByPID(int? pid)
        {
            var produkt = _context.Produkt.Find(pid);
            return produkt.Produktart.ID;
        }

        internal void AddCount(Produkt produkt, int zahl)
        {
            produkt.Zahl += zahl;
            _context.Update(produkt);
            _context.SaveChanges();
        }

        internal void MinusCount(Produkt produkt, int zahl)
        {
            produkt.Zahl -= zahl;
            _context.Update(produkt);
            _context.SaveChanges();
        }
    }
}
