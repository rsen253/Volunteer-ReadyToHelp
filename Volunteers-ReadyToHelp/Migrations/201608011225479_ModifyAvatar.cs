namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAvatar : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Avatars", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avatars", "UserId", c => c.String());
        }
    }
}
