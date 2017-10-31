using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ged.Models
{
    public class GedContext : DbContext
    {
        public GedContext() : base("DefaultConnection")
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public System.Data.Entity.DbSet<Ged.Models.PlanoContas> PlanoContas { get; set; }

        public System.Data.Entity.DbSet<Ged.Models.Conta> Contas { get; set; }
    }
}