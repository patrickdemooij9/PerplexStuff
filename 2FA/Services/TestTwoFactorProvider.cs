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
        private readonly TwoFactorService _twoFactorService;

        public TestTwoFactorProvider(TwoFactorService twoFactorService)
        {
            _twoFactorService = twoFactorService;
        }

        public Task<string> GenerateAsync(string purpose, UserManager<BackOfficeIdentityUser> manager, BackOfficeIdentityUser user)
        {
            return Task.FromResult("test");
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<BackOfficeIdentityUser> manager, BackOfficeIdentityUser user)
        {
            var auth = await _twoFactorService.GetTwoFactor(int.Parse(user.Id), "Stuff");
            if (auth is null)
                return false;

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var result = tfa.ValidateTwoFactorPIN(auth.Value, token);
            return result;
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<BackOfficeIdentityUser> manager, BackOfficeIdentityUser user)
        {
            return Task.FromResult(true);
        }
    }
}
