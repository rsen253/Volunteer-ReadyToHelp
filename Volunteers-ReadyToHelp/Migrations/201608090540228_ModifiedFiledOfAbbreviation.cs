namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedFiledOfAbbreviation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AbbreviationId", "dbo.Abbreviations");
            DropIndex("dbo.AspNetUsers", new[] { "AbbreviationId" });
            DropPrimaryKey("dbo.Abbreviations");
            AddColumn("dbo.AspNetUsers", "Abbreviation_AbbreviationId", c => c.Int());
            AlterColumn("dbo.Abbreviations", "AbbreviationId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AspNetUsers", "AbbreviationId", c => c.String());
            AddPrimaryKey("dbo.Abbreviations", "AbbreviationId");
            CreateIndex("dbo.AspNetUsers", "Abbreviation_AbbreviationId");
            AddForeignKey("dbo.AspNetUsers", "Abbreviation_AbbreviationId", "dbo.Abbreviations", "AbbreviationId");
            DropColumn("dbo.Abbreviations", "alphabet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Abbreviations", "alphabet", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "Abbreviation_AbbreviationId", "dbo.Abbreviations");
            DropIndex("dbo.AspNetUsers", new[] { "Abbreviation_AbbreviationId" });
            DropPrimaryKey("dbo.Abbreviations");
            AlterColumn("dbo.AspNetUsers", "AbbreviationId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Abbreviations", "AbbreviationId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Abbreviation_AbbreviationId");
            AddPrimaryKey("dbo.Abbreviations", "AbbreviationId");
            CreateIndex("dbo.AspNetUsers", "AbbreviationId");
            AddForeignKey("dbo.AspNetUsers", "AbbreviationId", "dbo.Abbreviations", "AbbreviationId");
        }
    }
}
