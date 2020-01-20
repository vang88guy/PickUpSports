namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRolesandEmail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "ApplicationId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "ApplicationId" });
            AddColumn("dbo.Events", "PlayerId", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "SkillLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "SportsInterest", c => c.String());
            CreateIndex("dbo.Events", "PlayerId");
            AddForeignKey("dbo.Events", "PlayerId", "dbo.Players", "PlayerId", cascadeDelete: true);
            DropColumn("dbo.Events", "ApplicationId");
            DropColumn("dbo.Players", "PlayLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "PlayLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "ApplicationId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Events", "PlayerId", "dbo.Players");
            DropIndex("dbo.Events", new[] { "PlayerId" });
            DropColumn("dbo.Players", "SportsInterest");
            DropColumn("dbo.Players", "SkillLevel");
            DropColumn("dbo.Events", "PlayerId");
            CreateIndex("dbo.Events", "ApplicationId");
            AddForeignKey("dbo.Events", "ApplicationId", "dbo.AspNetUsers", "Id");
        }
    }
}
