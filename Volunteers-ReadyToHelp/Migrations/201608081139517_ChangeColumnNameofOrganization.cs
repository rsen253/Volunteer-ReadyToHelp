namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnNameofOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "EstablishedOn", c => c.String());
            DropColumn("dbo.Organizations", "logoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organizations", "logoId", c => c.String());
            DropColumn("dbo.Organizations", "EstablishedOn");
        }
    }
}
