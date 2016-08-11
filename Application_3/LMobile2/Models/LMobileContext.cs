using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMobile2.Models
{
    public class LMobileContext : DbContext
    {
        public LMobileContext() 
                : base("name=LMobileContext")
        {
        }

        public DbSet<ServiceAuftrag> ServiceAuftrage { get; set; }
        public DbSet<Kunde> Kundes { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<ArbeitsZeitMeldung> ArbeitsZeitMeldungs { get; set; }
    }
}