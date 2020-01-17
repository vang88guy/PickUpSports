namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedPhoneNumberName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "PhoneNumber", c => c.String());
            DropColumn("dbo.Players", "PhoneNujmber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "PhoneNujmber", c => c.String());
            DropColumn("dbo.Players", "PhoneNumber");
        }
    }
}
