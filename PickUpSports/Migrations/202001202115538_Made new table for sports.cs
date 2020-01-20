namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Madenewtableforsports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sports",
                c => new
                    {
                        SportId = c.Int(nullable: false, identity: true),
                        SportName = c.String(),
                    })
                .PrimaryKey(t => t.SportId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sports");
        }
    }
}
