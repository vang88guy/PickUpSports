namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtimedateandmaxplayerstoevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "DateOfEvent", c => c.String());
            AddColumn("dbo.Events", "TimeOfEvent", c => c.String());
            AddColumn("dbo.Events", "MaximumPlayers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "MaximumPlayers");
            DropColumn("dbo.Events", "TimeOfEvent");
            DropColumn("dbo.Events", "DateOfEvent");
        }
    }
}
