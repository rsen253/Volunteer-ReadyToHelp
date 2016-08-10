namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExceptionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        ErrorId = c.String(nullable: false, maxLength: 128),
                        ErrorType = c.String(),
                        IsUserAuthenticate = c.Boolean(nullable: false),
                        IP = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.String(),
                        Browser = c.String(),
                    })
                .PrimaryKey(t => t.ErrorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionLogs");
        }
    }
}
