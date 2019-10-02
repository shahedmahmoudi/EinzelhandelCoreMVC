using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EinzelhandelCoreMVC.Data;
using EinzelhandelCoreMVC.Models;

namespace EinzelhandelCoreMVC.Controllers
{
    public class ProduktartsController : Controller
    {
        private readonly MVCEinzelhandelContext _context;

        public ProduktartsController(MVCEinzelhandelContext context)
        {
            _context = context;
        }

        // GET: Produktarts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produktart.ToListAsync());
        }

        // GET: Produktarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktart = await _context.Produktart
                .FirstOrDefaultAsync(m => m.ID == id);


            if (produktart == null)
            {
                return NotFound();
            }

            return View(produktart);
        }

        // GET: Produktarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produktarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titel")] Produktart produktart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produktart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produktart);
        }

        // GET: Produktarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktart = await _context.Produktart.FindAsync(id);
            if (produktart == null)
            {
                return NotFound();
            }
            return View(produktart);
        }

        // POST: Produktarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titel")] Produktart produktart)
        {
            if (id != produktart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produktart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktartExists(produktart.ID))
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
            return View(produktart);
        }

        // GET: Produktarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktart = await _context.Produktart
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktart == null)
            {
                return NotFound();
            }

            return View(produktart);
        }

        // POST: Produktarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produktart = await _context.Produktart.FindAsync(id);
            _context.Produktart.Remove(produktart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktartExists(int id)
        {
            return _context.Produktart.Any(e => e.ID == id);
        }
    }
}
