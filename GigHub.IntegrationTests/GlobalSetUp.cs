using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            // If db does not exist, it will be created and updated to latest version
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser
            {
                UserName = "username1",
                Name = "name1",
                Email = "-",
                PasswordHash = "-"
            });
            context.Users.Add(new ApplicationUser
            {
                UserName = "username2",
                Name = "name2",
                Email = "-",
                PasswordHash = "-"
            });
            context.SaveChanges();

        }
    }
}
