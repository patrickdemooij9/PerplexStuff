using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;

namespace Perplex._2FA.Services
{
    public class CustomBackOfficeUserStore : BackOfficeUserStore
    {
        public CustomBackOfficeUserStore(IScopeProvider scopeProvider, IUserService userService, IEntityService entityService, IExternalLoginService externalLoginService, IOptions<GlobalSettings> globalSettings, IUmbracoMapper mapper, BackOfficeErrorDescriber describer, AppCaches appCaches) : base(scopeProvider, userService, entityService, externalLoginService, globalSettings, mapper, describer, appCaches)
        {
        }

        public override Task<bool> GetTwoFactorEnabledAsync(BackOfficeIdentityUser user,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(true);
        }
    }
}
