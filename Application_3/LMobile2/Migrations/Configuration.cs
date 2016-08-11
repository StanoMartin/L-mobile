namespace LMobile2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LMobile2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<LMobile2.Models.LMobileContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMobile2.Models.LMobileContext context)
        {
            context.Kundes.AddOrUpdate(x => x.KundenNummer,
                new Kunde() { KundenNummer = "K000001", Name = "Peter Fischer GmbH" },
                new Kunde() { KundenNummer = "K000002", Name = "Pavol Fischer KS" },
                new Kunde() { KundenNummer = "K000003", Name = "George Fischer sro" }
            );

            context.Machines.AddOrUpdate(x => x.MachinenNummer,
                new Machine() { MachinenNummer = "M000815", Bezeichnung = "Laserschweissanlage" },
                new Machine() { MachinenNummer = "M000816", Bezeichnung = "Auto" },
                new Machine() { MachinenNummer = "M000817", Bezeichnung = "LKW" }
            );

            context.ArbeitsZeitMeldungs.AddOrUpdate(x=>x.ArtikelNummer,
                new ArbeitsZeitMeldung { ArtikelNummer = "4711", AnfangDatum = new DateTime(2016,8,1), AnfangUhrZeit = new DateTime(1970, 1,1,8,15,0), EndeDatum = new DateTime(2016, 8, 4), EndeUhrZeit = new DateTime(1970, 1, 1, 11, 25, 0), ServiceAuftragNummer = "S0004711" },
                new ArbeitsZeitMeldung { ArtikelNummer = "0815", AnfangDatum = new DateTime(2016, 8, 2), AnfangUhrZeit = new DateTime(1970, 1, 1, 9, 15, 0), EndeDatum = new DateTime(2016, 8, 5), EndeUhrZeit = new DateTime(1970, 1, 1, 12, 35, 0), ServiceAuftragNummer = "S0004720" },
                new ArbeitsZeitMeldung { ArtikelNummer = "0817", AnfangDatum = new DateTime(2016, 8, 3), AnfangUhrZeit = new DateTime(1970, 1, 1, 10, 15, 0), EndeDatum = new DateTime(2016, 8, 6), EndeUhrZeit = new DateTime(1970, 1, 1, 13, 45, 0), ServiceAuftragNummer = "S0004730" }
            );

            context.ServiceAuftrage.AddOrUpdate(x=>x.AuftragsNummer,
                new ServiceAuftrag { AuftragsNummer = "S0004711", AuftragsArt = "Reparaturauftrag", KundenNummer = "K000001", MachinenNummer = "M000815" },
                new ServiceAuftrag { AuftragsNummer = "S0004720", AuftragsArt = "Reparaturauftrag 2", KundenNummer = "K000002", MachinenNummer = "M000816" },
                new ServiceAuftrag { AuftragsNummer = "S0004730", AuftragsArt = "Reparaturauftrag 3",  KundenNummer = "K000003", MachinenNummer = "M000817" }
            );
        }
    }
}
