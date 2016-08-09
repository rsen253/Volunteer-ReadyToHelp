namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserAbbribiationDataType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Abbreviation_AbbreviationId", "dbo.Abbreviations");
            DropIndex("dbo.AspNetUsers", new[] { "Abbreviation_AbbreviationId" });
            DropColumn("dbo.AspNetUsers", "AbbreviationId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Abbreviation_AbbreviationId", newName: "AbbreviationId");
            AlterColumn("dbo.AspNetUsers", "AbbreviationId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "AbbreviationId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AbbreviationId");
            AddForeignKey("dbo.AspNetUsers", "AbbreviationId", "dbo.Abbreviations", "AbbreviationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AbbreviationId", "dbo.Abbreviations");
            DropIndex("dbo.AspNetUsers", new[] { "AbbreviationId" });
            AlterColumn("dbo.AspNetUsers", "AbbreviationId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "AbbreviationId", c => c.String());
            RenameColumn(table: "dbo.AspNetUsers", name: "AbbreviationId", newName: "Abbreviation_AbbreviationId");
            AddColumn("dbo.AspNetUsers", "AbbreviationId", c => c.String());
            CreateIndex("dbo.AspNetUsers", "Abbreviation_AbbreviationId");
            AddForeignKey("dbo.AspNetUsers", "Abbreviation_AbbreviationId", "dbo.Abbreviations", "AbbreviationId");
        }
    }
}
