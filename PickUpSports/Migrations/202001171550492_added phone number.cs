namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedphonenumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "PhoneNumber");
        }
    }
}
