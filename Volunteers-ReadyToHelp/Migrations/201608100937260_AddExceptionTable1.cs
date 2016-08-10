namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExceptionTable1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ExceptionLogs", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExceptionLogs", "Time", c => c.String());
        }
    }
}
