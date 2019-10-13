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
    public class KundesController : Controller
    {
        private readonly MVCEinzelhandelContext _context;

        public KundesController(MVCEinzelhandelContext context)
        {
            _context = context;
        }

        // GET: Kundes
        public async Task<IActionResult> Index()
        {
            var repo = new KundeRepository(_context);

            var myTask = Task.Run(() => repo.GetKundeList());
            // your thread is free to do other useful stuff right nw

            // after a while you need the result, await for myTask:
            List<KundeDetail> result = await myTask;

            return View(result);
        }

        // GET: Kundes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kunde
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // GET: Kundes/Create
        public IActionResult Create()
        {
            
            KundeDetail KDetail = new KundeDetail();
            return View(KDetail);
        }

        // POST: Kundes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Vorname,Nachname,Rufnummer,Adresse,Email,Geschlecht")] KundeDetail kundeDetail)
        {
            if (ModelState.IsValid)
            {
                Kunde kunde = new Kunde()
                {
                    Adresse = kundeDetail.Adresse,
                    Email = kundeDetail.Email,
                    ID = kundeDetail.ID,
                    Geschlecht = kundeDetail.Geschlecht,
                    Nachname = kundeDetail.Nachname,
                    Rufnummer = kundeDetail.Rufnummer,
                    Vorname = kundeDetail.Vorname
                };

                _context.Add(kunde);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kundeDetail);
        }

        // GET: Kundes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kunde.FindAsync(id);
            KundeDetail KDetail = new KundeDetail()
            {
                Email = kunde.Email,
                Vorname = kunde.Vorname,
                Nachname = kunde.Nachname,
                Adresse = kunde.Adresse,
                Rufnummer = kunde.Rufnummer,
                ID = kunde.ID,
                Geschlecht = kunde.Geschlecht
            };
            
            if (kunde == null)
            {
                return NotFound();
            }
            return View(KDetail);
        }

        // POST: Kundes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Vorname,Nachname,Rufnummer,Adresse,Email,Geschlecht")] Kunde kunde)
        {
            if (id != kunde.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kunde);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KundeExists(kunde.ID))
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
            return View(kunde);
        }

        // GET: Kundes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kunde
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // POST: Kundes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kunde = await _context.Kunde.FindAsync(id);
            _context.Kunde.Remove(kunde);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KundeExists(int id)
        {
            return _context.Kunde.Any(e => e.ID == id);
        }
    }
}
