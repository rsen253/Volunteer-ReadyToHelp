namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewFileldInAvater : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avatars", "ExternalLoginUserPictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avatars", "ExternalLoginUserPictureUrl");
        }
    }
}
