namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrganizationStatustableColumnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationStates", "OrganizationStatus", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrganizationStates", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationStates", "MyProperty", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrganizationStates", "OrganizationStatus");
        }
    }
}
