namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTableAndFileld : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abbreviations",
                c => new
                    {
                        AbbreviationId = c.String(nullable: false, maxLength: 128),
                        alphabet = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.AbbreviationId);
            
            AddColumn("dbo.AspNetUsers", "AbbreviationId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "AbbreviationId");
            AddForeignKey("dbo.AspNetUsers", "AbbreviationId", "dbo.Abbreviations", "AbbreviationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AbbreviationId", "dbo.Abbreviations");
            DropIndex("dbo.AspNetUsers", new[] { "AbbreviationId" });
            DropColumn("dbo.AspNetUsers", "AbbreviationId");
            DropTable("dbo.Abbreviations");
        }
    }
}
