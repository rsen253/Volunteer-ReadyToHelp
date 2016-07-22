namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.String(nullable: false, maxLength: 128),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.String(nullable: false, maxLength: 128),
                        _stateId = c.String(),
                        StateName = c.String(),
                        Country_CountryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .Index(t => t.Country_CountryId);
            
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "StateId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CountryId");
            CreateIndex("dbo.AspNetUsers", "StateId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "StateId", "dbo.States", "StateId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StateId", "dbo.States");
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.States", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "StateId" });
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropIndex("dbo.States", new[] { "Country_CountryId" });
            AlterColumn("dbo.AspNetUsers", "StateId", c => c.String());
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.String());
            DropTable("dbo.States");
            DropTable("dbo.Countries");
        }
    }
}
