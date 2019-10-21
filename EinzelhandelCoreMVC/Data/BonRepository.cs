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

        internal List<DetailDetail> GetBonDetailList(int? ID)
        {
            List<Detail> Detaila = new List<Detail>();
            Detaila = _context.Detail.AsNoTracking()
                .Include(x=>x.Produkt)
                .Include(x=>x.Bon).Include(x=>x.Bon.Kunde)
                .Where(x => x.Bon.ID == ID).ToList();
            if (Detaila.Count > 0)
            {
                List<DetailDetail> DetailDetail = new List<DetailDetail>();
                foreach (var item in Detaila)
                {
                    var DDetail = new DetailDetail()
                    {
                        ID = item.ID,
                        Ermäßigung = item.Ermäßigung,
                        Preis = item.Preis,
                        Zahl = item.Zahl,
                       // BonNumber = item.Bon.ID,
                        Datum = item.Bon.Datum,
                        KundeNachname = item.Bon.Kunde.Vorname,
                        KundeVorname = item.Bon.Kunde.Nachname,
                        BonID = item.Bon.ID
                    };
                    DetailDetail.Add(DDetail);
                }
                return DetailDetail;
            }
            else
            {
                List<DetailDetail> DetailDetail = new List<DetailDetail>();
                var DDetail = new DetailDetail()
                {
                    BonID = int.Parse(ID.ToString())

                };
                DetailDetail.Add(DDetail);
                return DetailDetail;
            }
            return null;
        }
        //public IEnumerable<SelectListItem> GetproduktartSelectList()
        //{
        //    List<Produktart> PP = _context.Produktart.ToList();
        //    List<SelectListItem> produktart = _context.Produktart.AsNoTracking()
        //        .OrderBy(n => n.Titel)
        //            .Select(n =>
        //            new SelectListItem
        //            {
        //                Value = n.ID.ToString(),
        //                Text = n.Titel
        //            }).ToList();
        //    var Produktarttip = new SelectListItem()
        //    {
        //        Value = null,
        //        Text = "Wählen Sie den Produktart"
        //    };
        //    produktart.Insert(0, Produktarttip);
        //    return new SelectList(produktart, "Value", "Text");
        //}

        //public IEnumerable<SelectListItem> GetproduktSelectList()
        //{
        //    List<Produkt> PP = _context.Produkt.ToList();
        //    List<SelectListItem> produkt = _context.Produkt.AsNoTracking()
        //        .OrderBy(n => n.Titel)
        //            .Select(n =>
        //            new SelectListItem
        //            {
        //                Value = n.ID.ToString(),
        //                Text = n.Titel
        //            }).ToList();
        //    var Produkttip = new SelectListItem()
        //    {
        //        Value = null,
        //        Text = "Wählen Sie den Produktart"
        //    };
        //    produkt.Insert(0, Produkttip);
        //    return new SelectList(produkt, "Value", "Text");
        //}
        internal string GetKundeTitel(int iD)
        {
            Kunde k = _context.Kunde.Find(iD);
            return k.Vorname + " " + k.Nachname;
        }
    }
}
