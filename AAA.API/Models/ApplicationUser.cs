using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AAA.API.Infra;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AAA.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public int ClientId { get; set; }

        public DateTime JoinDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager, string jwt)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, jwt).ConfigureAwait(false);
            // Add custom user claims here

            return userIdentity;
        }
    }
}