﻿using EinzelhandelCoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EinzelhandelCoreMVC.Data
{
    public class MVCEinzelhandelContext: DbContext
    {
        public MVCEinzelhandelContext(DbContextOptions<MVCEinzelhandelContext> options):base(options)
        {

        }
        public DbSet<Produktart> Produktart { get; set; }
        public DbSet<Produkt> Produkt { get; set; }
        public DbSet<EinzelhandelCoreMVC.Models.Kunde> Kunde { get; set; }
        public DbSet<EinzelhandelCoreMVC.Models.Bon> Bon { get; set; }
        public DbSet<EinzelhandelCoreMVC.Models.Detail> Detail { get; set; }

    }
}
