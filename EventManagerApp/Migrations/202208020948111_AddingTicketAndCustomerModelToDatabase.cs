namespace EventManagerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTicketAndCustomerModelToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_Id = c.Int(),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Tickets", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Tickets", new[] { "Event_Id" });
            DropIndex("dbo.Tickets", new[] { "Customer_Id" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Customers");
        }
    }
}
