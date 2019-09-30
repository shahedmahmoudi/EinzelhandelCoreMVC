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
        public IEnumerable<SelectListItem> Getproduktart()
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
                    Text = "--- select country ---"
                };
                produktart.Insert(0, Produktarttip);
                return new SelectList(produktart, "Value", "Text");
           
        }
    }
}
