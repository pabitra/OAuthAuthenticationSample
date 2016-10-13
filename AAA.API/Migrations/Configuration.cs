using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AAA.API.Infra;
using AAA.API.Models;
using AAA.API.Provider;
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
            var userManagementContext = new UserManagementContext();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userManagementContext));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userManagementContext));

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


            if (userManagementContext.Clients.Any())
            {
                return;
            }

            userManagementContext.Clients.AddRange(BuildClientsList());
            userManagementContext.SaveChanges();
        }

        private static List<Client> BuildClientsList()
        {

            List<Client> ClientsList = new List<Client> 
            {
                new Client
                { Id = "ngAuthApp", 
                    Secret= Helper.GetHash("abc@123"), 
                    Name="AngularJS front-end Application", 
                    ApplicationType =  Models.ApplicationTypes.MobileApp, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "http://localhost"
                },
                new Client
                { Id = "consoleApp", 
                    Secret=Helper.GetHash("123@abc"), 
                    Name="Console Application", 
                    ApplicationType =Models.ApplicationTypes.Platform, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}
