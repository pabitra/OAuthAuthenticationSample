using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AAA.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AAA.API.Infra
{
    public class UserManagementContext : IdentityDbContext<ApplicationUser>
    {
        public UserManagementContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public static UserManagementContext Create()
        {
            return new UserManagementContext();
        }
    }
}