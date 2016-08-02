namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTableForPoint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        PointId = c.String(nullable: false, maxLength: 128),
                        PointImage = c.String(),
                    })
                .PrimaryKey(t => t.PointId);
            
            CreateTable(
                "dbo.UserPoints",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PointId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Points", t => t.PointId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPoints", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPoints", "PointId", "dbo.Points");
            DropIndex("dbo.UserPoints", new[] { "PointId" });
            DropIndex("dbo.UserPoints", new[] { "UserId" });
            DropTable("dbo.UserPoints");
            DropTable("dbo.Points");
        }
    }
}
