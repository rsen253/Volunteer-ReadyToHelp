namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColrIdColumnFormUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ColorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ColorId", c => c.Int(nullable: false));
        }
    }
}
