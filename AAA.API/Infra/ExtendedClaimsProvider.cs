using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AAA.API.Models;

namespace AAA.API.Infra
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ApplicationUser user)
        {

            var claims = new List<Claim>();

            var daysInWork = (DateTime.Now.Date - user.JoinDate).TotalDays;

            claims.Add(daysInWork > 90 ? CreateClaim("FTE", "1") : CreateClaim("FTE", "0"));

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

    }
}