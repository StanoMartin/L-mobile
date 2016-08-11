namespace LMobile2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArbeitsZeitMeldungs",
                c => new
                    {
                        ArtikelNummer = c.String(nullable: false, maxLength: 128),
                        AnfangDatum = c.DateTime(nullable: false),
                        AnfangUhrZeit = c.DateTime(nullable: false),
                        EndeDatum = c.DateTime(nullable: false),
                        EndeUhrZeit = c.DateTime(nullable: false),
                        ServiceAuftragNummer = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtikelNummer)
                .ForeignKey("dbo.ServiceAuftrags", t => t.ServiceAuftragNummer)
                .Index(t => t.ServiceAuftragNummer);
            
            CreateTable(
                "dbo.ServiceAuftrags",
                c => new
                    {
                        AuftragsNummer = c.String(nullable: false, maxLength: 128),
                        AuftragsArt = c.String(),
                        KundenNummer = c.String(maxLength: 128),
                        MachinenNummer = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AuftragsNummer)
                .ForeignKey("dbo.Kundes", t => t.KundenNummer)
                .ForeignKey("dbo.Machines", t => t.MachinenNummer)
                .Index(t => t.KundenNummer)
                .Index(t => t.MachinenNummer);
            
            CreateTable(
                "dbo.Kundes",
                c => new
                    {
                        KundenNummer = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.KundenNummer);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        MachinenNummer = c.String(nullable: false, maxLength: 128),
                        Bezeichnung = c.String(),
                    })
                .PrimaryKey(t => t.MachinenNummer);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArbeitsZeitMeldungs", "ServiceAuftragNummer", "dbo.ServiceAuftrags");
            DropForeignKey("dbo.ServiceAuftrags", "MachinenNummer", "dbo.Machines");
            DropForeignKey("dbo.ServiceAuftrags", "KundenNummer", "dbo.Kundes");
            DropIndex("dbo.ServiceAuftrags", new[] { "MachinenNummer" });
            DropIndex("dbo.ServiceAuftrags", new[] { "KundenNummer" });
            DropIndex("dbo.ArbeitsZeitMeldungs", new[] { "ServiceAuftragNummer" });
            DropTable("dbo.Machines");
            DropTable("dbo.Kundes");
            DropTable("dbo.ServiceAuftrags");
            DropTable("dbo.ArbeitsZeitMeldungs");
        }
    }
}
