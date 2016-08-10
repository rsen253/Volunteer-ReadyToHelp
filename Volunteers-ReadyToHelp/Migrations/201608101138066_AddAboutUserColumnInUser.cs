namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAboutUserColumnInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AboutUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AboutUser");
        }
    }
}
