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
    public class ProduktsController : Controller
    {
        private readonly MVCEinzelhandelContext _context;

        public ProduktsController(MVCEinzelhandelContext context)
        {
            _context = context;
        }

        // GET: Produkts
        public async Task<IActionResult> Index()
        {
            var repo = new ProduktartRepository(_context);        
            var myTask = Task.Run(() => repo.GetProduktList());   
            List<ProduktDetail> result = await myTask; 
            return View(  result);
        }

        // GET: Produkts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt.FindAsync(id);
            var pr = new ProduktartRepository(_context);
            var produktDetail = new ProduktDetail()
            {
                Produktarts = pr.GetproduktartSelectList(),
                ID =produkt.ID,
                Titel=produkt.Titel,
                Zahl=produkt.Zahl,
                ProduktartID = produkt.Produktart.ID ,
                ProduktartTitel = produkt.Produktart.Titel
        };
           

            if (produkt == null)
            {
                return NotFound();
            }

            return View(produktDetail);
        }

        // GET: Produkts/Create
        public IActionResult Create()
        {
            var produktDet = new ProduktDetail();
            var pr = new ProduktartRepository(_context);
            produktDet.Produktarts = pr.GetproduktartSelectList();
            return View(produktDet);
        }

        // POST: Produkts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titel,Zahl,ProduktartID")] ProduktDetail produktDetail)
        {
          
            if (ModelState.IsValid)
            {

                var produkt = new Produkt()
                {
                     
                    Titel=produktDetail.Titel,
                    Zahl=produktDetail.Zahl
                };
                produkt.Produktart = _context.Produktart.Find(produktDetail.ProduktartID);
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            throw new ApplicationException("Invalid model");
        }

        // GET: Produkts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
            }       

            var produkt = await _context.Produkt.FindAsync(id);
            var pr = new ProduktartRepository(_context);
            var produktDet = new ProduktDetail()
            {
                Produktarts = pr.GetproduktartSelectList(),
                ID = produkt.ID,
                Titel = produkt.Titel,
                Zahl = produkt.Zahl,
                ProduktartID = produkt.Produktart.ID
            };
            
            
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produktDet);
        }

        // POST: Produkts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titel,Zahl,ProduktartID")] ProduktDetail produktDetail)
        {
            if (id != produktDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var produkt = new Produkt()
                    {
                        ID=produktDetail.ID,
                        Titel=produktDetail.Titel,
                        Zahl=produktDetail.Zahl
                    };
                    produkt.Produktart = _context.Produktart.Find(produktDetail.ProduktartID);

                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produktDetail.ID))
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
            return View(produktDetail);
        }

        // GET: Produkts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.ID == id);
            var pr = new ProduktartRepository(_context);
            var produktDetail = new ProduktDetail()
            {
                Produktarts = pr.GetproduktartSelectList(),
                ID = produkt.ID,
                Titel = produkt.Titel,
                Zahl = produkt.Zahl,
                ProduktartID = produkt.Produktart.ID,
                ProduktartTitel = produkt.Produktart.Titel
            };

            if (produktDetail == null)
            {
                return NotFound();
            }

            return View(produktDetail);
        }

        // POST: Produkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkt = await _context.Produkt.FindAsync(id);
            _context.Produkt.Remove(produkt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return _context.Produkt.Any(e => e.ID == id);
        }
        public ActionResult GetAllEmployeeData()
        {
            return View(_context.Produkt.ToList()) ;
        }
    }
}
