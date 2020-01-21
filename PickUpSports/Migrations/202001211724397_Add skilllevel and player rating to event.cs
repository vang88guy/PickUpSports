namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addskilllevelandplayerratingtoevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "SkillLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "PlayerRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "PlayerRating");
            DropColumn("dbo.Events", "SkillLevel");
        }
    }
}
