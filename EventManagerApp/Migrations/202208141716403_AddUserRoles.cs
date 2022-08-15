namespace EventManagerApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles(Id, Name) Values ('1', 'Admin')");
            Sql("INSERT INTO AspNetRoles(Id, Name) Values ('2', 'User')");
        }

        public override void Down()
        {
            Sql("DELETE FROM AspNetRoles WHERE Name= 'Admin'");
            Sql("DELETE FROM AspNetRoles WHERE Name= 'User'");
        }
    }
}
