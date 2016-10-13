using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AAA.API.Controllers
{
    [RoutePrefix("api/claims")]
    public class ClaimsController : BaseApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult GetClaims()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = from c in identity.Claims
                    select new
                    {
                        subject = c.Subject.Name,
                        type = c.Type,
                        value = c.Value
                    };

                return Ok(claims);
            }

            return Ok();
        }

    }
}
