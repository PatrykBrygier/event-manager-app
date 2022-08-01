namespace EventManagerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDateAndPlaceToEventModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Place", c => c.String());
            AddColumn("dbo.Events", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Date");
            DropColumn("dbo.Events", "Place");
        }
    }
}
