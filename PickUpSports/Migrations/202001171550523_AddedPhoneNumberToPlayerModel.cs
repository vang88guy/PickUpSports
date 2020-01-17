namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhoneNumberToPlayerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "PhoneNujmber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "PhoneNujmber");
        }
    }
}
