﻿using System;
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

        public int BonID1;
        public async Task<IActionResult> Index(int id)
        {
            BonID1 = id;
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

            var detail = await _context.Detail
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: Details/Create
        public IActionResult Create(int id)
        {
              var DetDet = new DetailDetail();
            var bonRep = new BonRepository(_context);
            DetDet.Produkts = bonRep.GetproduktSelectList();
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

            var detail = await _context.Detail.Include(x => x.Bon)
                .Include(x => x.Bon.Kunde)
                .Include(x => x.Produkt)
                .FirstOrDefaultAsync(i => i.ID == id);
            var detailDetail = new DetailDetail()
            {
                BonID=detail.Bon.ID,
                 Datum=detail.Bon.Datum,
                 Ermäßigung=detail.Ermäßigung,
                 KundeVorname=detail.Bon.Kunde.Vorname,
                 KundeNachname=detail.Bon.Kunde.Nachname,
                 Preis=detail.Preis,
                 ProduktID=detail.Produkt.ID,
                 ProduktTitel=detail.Produkt.Titel,
                 Zahl=detail.Zahl,
                 ID=detail.ID

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

            var detail = await _context.Detail
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
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
