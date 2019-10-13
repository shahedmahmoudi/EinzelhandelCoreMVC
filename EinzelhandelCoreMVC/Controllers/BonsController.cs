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
    public class BonsController : Controller
    {
        private readonly MVCEinzelhandelContext _context;

        public BonsController(MVCEinzelhandelContext context)
        {
            _context = context;
        }

        // GET: Bons
        public async Task<IActionResult> Index()
        {
            var repo = new BonRepository(_context);
            var myTask = Task.Run(() => repo.GetBonList());
            List<BonDetail> result = await myTask;
            return View(result);
        }

        // GET: Bons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            //var bon = await _context.Bon.FindAsync(id);
            //var kun = new KundeRepository(_context);
            //var bonDetail = new BonDetail()
            //{
            //    Kundes = kun.GetKundeSelectList(),
            //    Art = bon.Art,
            //    Datum = bon.Datum,
            //    ID = bon.ID,
            //    KundeID = bon.Kunde.ID
            //};

            var bon = await _context.Bon.FindAsync(id);
            var kun = new KundeRepository(_context);
            var bonDetail = new BonDetail()
            {
                Kundes = kun.GetKundeSelectList(),
                Art = bon.Art,
                Datum = bon.Datum,
                ID = bon.ID,
                KundeID = bon.Kunde.ID
            };

            // int idd = bon.Kunde.ID;
            BonRepository pr = new BonRepository(_context);

           bonDetail.KundeTitel = pr.GetKundeTitel(bon.Kunde.ID);
            if (bon == null)
            {
                return NotFound();
            }

            return View(bonDetail);
        }

        // GET: Bons/Create
        public IActionResult Create()
        {
            var bonDet = new BonDetail();
            var kn = new KundeRepository(_context);
            bonDet.Kundes  = kn.GetKundeSelectList();
            return View(bonDet);
        }

        // POST: Bons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Datum,Art,KundeID")] BonDetail bonDet)
        {
            if (ModelState.IsValid)
            {
                var bonnew = new Bon()
                {
                    Art = bonDet.Art,
                    Datum = bonDet.Datum
                };
                bonnew.Kunde = _context.Kunde.Find(bonDet.KundeID);


                _context.Add(bonnew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            throw new ApplicationException("Invalid model");
        }

        // GET: Bons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bon = await _context.Bon.FindAsync(id);
             var kun = new KundeRepository(_context);
            var bonDetail = new BonDetail()
            {
                  Kundes = kun.GetKundeSelectList(),
                Art = bon.Art,
                Datum = bon.Datum,
                ID = bon.ID,
                 KundeID = bon.Kunde.ID
            };
            int idd = bon.Kunde.ID;
            if (bon == null)
            {
                return NotFound();
            }
            return View(bonDetail);
        }

        // POST: Bons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Datum,Art,KundeID")] BonDetail bonDet)
        {
            if (id != bonDet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bon = new Bon()
                    {
                        ID = bonDet.ID,
                        Art = bonDet.Art,
                        Datum = bonDet.Datum                         
                    };
                    bon.Kunde = _context.Kunde.Find(bonDet.KundeID);
                    _context.Update(bon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BonExists(bonDet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bonDet);
        }

        // GET: Bons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bon = await _context.Bon
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bon == null)
            {
                return NotFound();
            }

            return View(bon);
        }

        // POST: Bons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bon = await _context.Bon.FindAsync(id);
            _context.Bon.Remove(bon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BonExists(int id)
        {
            return _context.Bon.Any(e => e.ID == id);
        }
    }
}
