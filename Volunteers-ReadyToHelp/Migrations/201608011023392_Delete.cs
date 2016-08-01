namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AvatarId", "dbo.Avatars");
            DropIndex("dbo.AspNetUsers", new[] { "AvatarId" });
            DropColumn("dbo.AspNetUsers", "AvatarId");
            DropTable("dbo.Avatars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        AvatarId = c.String(nullable: false, maxLength: 128),
                        AvatarData = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.AvatarId);
            
            AddColumn("dbo.AspNetUsers", "AvatarId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "AvatarId");
            AddForeignKey("dbo.AspNetUsers", "AvatarId", "dbo.Avatars", "AvatarId");
        }
    }
}
