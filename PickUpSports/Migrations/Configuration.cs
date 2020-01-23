namespace PickUpSports.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PickUpSports.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PickUpSports.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Sport.AddOrUpdate(
                new Models.Sport { SportName = "Basketball" },
                new Models.Sport { SportName = "Football" },
                new Models.Sport { SportName = "Tennis", },
                new Models.Sport { SportName = "Volleyball" },
                new Models.Sport { SportName = "Soccer" });
            context.SkillLevel.AddOrUpdate(
                new Models.SkillLevel { Level = 1 },
                new Models.SkillLevel { Level = 2 },
                new Models.SkillLevel { Level = 3, },
                new Models.SkillLevel { Level = 4 },
                new Models.SkillLevel { Level = 5 });
        }
    }
}
