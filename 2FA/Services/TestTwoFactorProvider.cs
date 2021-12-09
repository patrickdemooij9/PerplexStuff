using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Identity;
using Umbraco.Cms.Core.Security;

namespace Perplex._2FA.Services
{
    public class TestTwoFactorProvider : IUserTwoFactorTokenProvider<BackOfficeIdentityUser>
    {
        public Task<string> GenerateAsync(string purpose, UserManager<BackOfficeIdentityUser> manager, BackOfficeIdentityUser user)
        {
            return Task.FromResult("test");
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<BackOfficeIdentityUser> manager, BackOfficeIdentityUser user)
        {
            return Task.FromResult(true);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var result = tfa.ValidateTwoFactorPIN("", token);
            return Task.FromResult(result);
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<BackOfficeIdentityUser> manager, BackOfficeIdentityUser user)
        {
            return Task.FromResult(true);
        }
    }
}
