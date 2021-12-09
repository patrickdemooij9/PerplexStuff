using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Net;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.BackOffice.Security;
using Umbraco.Cms.Web.Common.Security;

namespace Perplex._2FA.Services
{
    public class CustomBackOfficeUserManager : BackOfficeUserManager, IBackOfficeTwoFactorOptions
    {
        public CustomBackOfficeUserManager(IIpResolver ipResolver, IUserStore<BackOfficeIdentityUser> store, IOptions<BackOfficeIdentityOptions> optionsAccessor, IPasswordHasher<BackOfficeIdentityUser> passwordHasher, IEnumerable<IUserValidator<BackOfficeIdentityUser>> userValidators, IEnumerable<IPasswordValidator<BackOfficeIdentityUser>> passwordValidators, BackOfficeErrorDescriber errors, IServiceProvider services, IHttpContextAccessor httpContextAccessor, ILogger<UserManager<BackOfficeIdentityUser>> logger, IOptions<UserPasswordConfigurationSettings> passwordConfiguration, IEventAggregator eventAggregator, IBackOfficeUserPasswordChecker backOfficeUserPasswordChecker) : base(ipResolver, store, optionsAccessor, passwordHasher, userValidators, passwordValidators, errors, services, httpContextAccessor, logger, passwordConfiguration, eventAggregator, backOfficeUserPasswordChecker)
        {
        }

        public override bool SupportsUserTwoFactor => true;

        public override Task<bool> GetTwoFactorEnabledAsync(BackOfficeIdentityUser user)
        {
            return Task.FromResult(true);
        }

        public override Task<IdentityResult> SetTwoFactorEnabledAsync(BackOfficeIdentityUser user, bool enabled)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public string GetTwoFactorView(string username)
        {
            return "/App_Plugins/2FA/2fa.html";
        }

        public override Task<IList<string>> GetValidTwoFactorProvidersAsync(BackOfficeIdentityUser user)
        {
            IList<string> list = new List<string>();
            list.Add("Stuff");
            return Task.FromResult(list);
        }
    }
}
