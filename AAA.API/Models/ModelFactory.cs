using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using AAA.API.Infra;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AAA.API.Models
{
    public class ModelFactory
    {

        private readonly UrlHelper _urlHelper;
        private readonly ApplicationUserManager _appUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _appUserManager = appUserManager;
        }

        public UserViewModel Create(ApplicationUser appUser)
        {
            return new UserViewModel
            {
                Url = _urlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
           
                JoinDate = appUser.JoinDate,
                Roles = _appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _appUserManager.GetClaimsAsync(appUser.Id).Result
            };

        }

        public RoleViewModel Create(IdentityRole appRole)
        {

            return new RoleViewModel
            {
                Url = _urlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };

        }
    }
}