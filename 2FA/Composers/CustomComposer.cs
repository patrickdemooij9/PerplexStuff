using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Perplex._2FA.Components;
using Perplex._2FA.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.BackOffice.Security;
using Umbraco.Extensions;

namespace Perplex._2FA.Composers
{
    public class CustomComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.SetBackOfficeUserManager<CustomBackOfficeUserManager>();

            builder.Components().Append<DatabaseComponent>();

            builder.Services.AddScoped<IBackOfficeTwoFactorOptions, CustomBackOfficeUserManager>();
            builder.Services.AddAuthentication()
                .AddCookie(Constants.Security.BackOfficeTwoFactorRememberMeAuthenticationType);

            var identityBuilder = new BackOfficeIdentityBuilder(builder.Services);
            identityBuilder.AddTokenProvider("Stuff", typeof(TestTwoFactorProvider));
        }
    }
}
