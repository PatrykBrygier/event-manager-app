namespace EventManagerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnnotationsToEventModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TicketPool", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Events", "TicketsLeft");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "TicketsLeft", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Name", c => c.String());
            DropColumn("dbo.Events", "TicketPool");
        }
    }
}
