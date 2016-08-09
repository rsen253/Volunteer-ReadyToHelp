namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserTableColumnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "AbbreviationId", newName: "ColorId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AbbreviationId", newName: "IX_ColorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_ColorId", newName: "IX_AbbreviationId");
            RenameColumn(table: "dbo.AspNetUsers", name: "ColorId", newName: "AbbreviationId");
        }
    }
}
