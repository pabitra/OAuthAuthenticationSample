using System;
using System.Data.Entity.Migrations;
using System.Linq;
using AAA.API.Infra;
using AAA.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AAA.API.Migrations
{
   

    internal sealed class Configuration : DbMigrationsConfiguration<UserManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserManagementContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new UserManagementContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new UserManagementContext()));

            var user = new ApplicationUser()
            {
                UserName = "SuperPowerUser",
                Email = "pabitra,kumar@gmail.com",
                EmailConfirmed = true,
                FirstName = "Pabitra",
                LastName = "Mohapatra",
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "P@ss!123");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("SuperPowerUser");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });
        }
    }
}
