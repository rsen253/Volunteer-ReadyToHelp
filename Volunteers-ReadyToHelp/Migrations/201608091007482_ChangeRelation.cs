namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ColorId", "dbo.Abbreviations");
            DropIndex("dbo.AspNetUsers", new[] { "ColorId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.AspNetUsers", "ColorId");
            AddForeignKey("dbo.AspNetUsers", "ColorId", "dbo.Abbreviations", "AbbreviationId", cascadeDelete: true);
        }
    }
}
