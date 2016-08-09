namespace Volunteers_ReadyToHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedFiledOfAbbreviation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abbreviations", "alphabet", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Abbreviations", "alphabet");
        }
    }
}
