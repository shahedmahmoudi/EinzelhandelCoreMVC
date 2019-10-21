using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EinzelhandelCoreMVC.Data;
using EinzelhandelCoreMVC.Models;
using EinzelhandelCoreMVC.ModelView;

namespace EinzelhandelCoreMVC.Controllers
{
    public class DetailsController : Controller
    {
        private readonly MVCEinzelhandelContext _context;

        public DetailsController(MVCEinzelhandelContext context)
        {
            _context = context;
        }




       
       
         [HttpGet]
        public ActionResult GetProdukts(int id)
        {
            if (!string.IsNullOrWhiteSpace(id.ToString())  )
            {
                var repo = new ProduktartRepository(_context);

                IEnumerable<SelectListItem> produkts = repo.GetProdukts(id);
                return Json(produkts);
            }
            return null;
        }
        public async Task<IActionResult> Index(int id)
        {
           
            var repo = new BonRepository(_context);
            var myTask = Task.Run(() => repo.GetBonDetailList(id));
            List<DetailDetail> result = await myTask;

            return View(result);
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Detail.Include(x => x.Bon)
                .FirstOrDefaultAsync(m => m.ID == id);
            DetailDetail dd = new DetailDetail()
            {
                BonID = detail.Bon.ID,
                ID = detail.ID,
                Zahl = detail.Zahl,
                Datum = detail.Bon.Datum,
                Ermäßigung = detail.Ermäßigung,
                Preis = detail.Preis
            };

            if (detail == null)
            {
                return NotFound();
            }
            return View(dd);
        }

        // GET: Details/Create
        public IActionResult Create(int id)
        {
              var DetDet = new DetailDetail();
            var bonRep = new BonRepository(_context);
            var proRep = new ProduktartRepository(_context);
            DetDet.Produktarts = proRep.GetproduktartSelectList();
            DetDet.Produkts = proRep.GetProdukts();
            DetDet.BonID = id;                      
            return View(DetDet);
        }

        // POST: Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Zahl,Preis,Ermäßigung,BonID,ProduktID")] DetailDetail detail,int BonID)
        {
            if (ModelState.IsValid)
            {
                ProduktartRepository produkt = new ProduktartRepository(_context);
                Produkt p = _context.Produkt.Find(detail.ProduktID);
                Bon b = _context.Bon.Find(detail.BonID);
                Detail Det = new Detail()
                {
                    Bon = b,
                    Preis = detail.Preis,
                    Zahl = detail.Zahl,
                    Produkt=p,
                    Ermäßigung=detail.Ermäßigung
                };
                 
                _context.Add(Det);
                await _context.SaveChangesAsync();
                if (b.Art)
                    produkt.AddCount(p, detail.Zahl);
                else
                    produkt.MinusCount(p, detail.Zahl);
                return RedirectToAction("Index",new { id= detail.BonID });
            }
            return View(detail);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var bonRep = new BonRepository(_context);
            var proRep = new ProduktartRepository(_context);
           

            var detail = await _context.Detail.Include(x => x.Bon)
                .Include(x => x.Bon.Kunde)
                .Include(x => x.Produkt)
                .FirstOrDefaultAsync(i => i.ID == id);
            var detailDetail = new DetailDetail()
            {
                BonID = detail.Bon.ID,
                Datum = detail.Bon.Datum,
                Ermäßigung = detail.Ermäßigung,
                KundeVorname = detail.Bon.Kunde.Vorname,
                KundeNachname = detail.Bon.Kunde.Nachname,
                Preis = detail.Preis,
                ProduktID = detail.Produkt.ID,
                ProduktTitel = detail.Produkt.Titel,
                Zahl = detail.Zahl,
                ID = detail.ID,
                Produktarts = proRep.GetproduktartSelectList(),
                Produkts = proRep.GetProdukts()
            };
            if (detailDetail == null)
            {
                return NotFound();
            }
            return View(detailDetail);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Zahl,Preis,Ermäßigung,BonID,ProduktID")] DetailDetail detail)
        {
            if (id != detail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Produkt p = _context.Produkt.Find(detail.ProduktID);
                    Bon b = _context.Bon.Find(detail.BonID);
                    Detail Det = new Detail()
                    {
                        Bon = b,
                        Preis = detail.Preis,
                        Zahl = detail.Zahl,
                        Produkt = p,
                        Ermäßigung = detail.Ermäßigung,
                        ID=detail.ID

                    };
                    _context.Update(Det);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailExists(detail.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new { id= detail.BonID });
            }
            return View(detail);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Detail.Include(x => x.Bon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detail == null)
            {
                return NotFound();
            }
            DetailDetail dd = new DetailDetail()
            {
                BonID = detail.Bon.ID,
                ID = detail.ID,
                Zahl = detail.Zahl,
                Datum = detail.Bon.Datum,
                Ermäßigung = detail.Ermäßigung,
                Preis = detail.Preis
            };
            return View(dd);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detail = await _context.Detail.Include(x => x.Bon).FirstOrDefaultAsync(x => x.ID == id);
             
           
            _context.Detail.Remove(detail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = detail.Bon.ID });
        }

        private bool DetailExists(int id)
        {
            return _context.Detail.Any(e => e.ID == id);
        }
    }
}
