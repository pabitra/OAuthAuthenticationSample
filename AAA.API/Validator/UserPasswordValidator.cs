using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AAA.API.Validator
{
    public class UserPasswordValidator : PasswordValidator
    {

        public override async Task<IdentityResult> ValidateAsync(string password)
        {

            var result = await base.ValidateAsync(password).ConfigureAwait(false);

            if (password.Contains("abcdef") || password.Contains("123456"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Password can not contain sequence of chars");
                result = new IdentityResult(errors);
            }

            return result;
        }

    }
}