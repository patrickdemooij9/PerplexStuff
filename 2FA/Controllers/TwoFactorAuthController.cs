using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Perplex._2FA.Models.Business;
using Perplex._2FA.Models.Database;
using Perplex._2FA.Services;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace Perplex._2FA.Controllers
{
    public class TwoFactorAuthController : UmbracoAuthorizedApiController
    {
        private readonly TwoFactorService _twoFactorService;
        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;

        public TwoFactorAuthController(TwoFactorService twoFactorService, IBackOfficeSecurityAccessor backOfficeSecurityAccessor)
        {
            _twoFactorService = twoFactorService;
            _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        }

        [HttpGet]
        public async Task<bool> TwoFactorEnabled()
        {
            var result = await _twoFactorService.GetTwoFactorEnabledAsync(_backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser.Id);
            return result;
        }

        [HttpGet]
        public async Task<TwoFactorAuthInfo> GoogleAuthenticatorSetupCode()
        {
            var tfa = new TwoFactorAuthenticator();
            var user = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser;
            var accountSecretKey = Guid.NewGuid().ToString();

            var currentTwoFactor = await _twoFactorService.GetTwoFactor(user.Id, "Stuff");
            if (currentTwoFactor is null)
                currentTwoFactor = new TwoFactorEntity
                {
                    Provider = "Stuff",
                    UserId = user.Id,
                    IsConfirmed = false
                };
            else if (currentTwoFactor.IsConfirmed)
                return new TwoFactorAuthInfo();

            var setupInfo = tfa.GenerateSetupCode("TestApplication", user.Email, accountSecretKey, false, 30);

            currentTwoFactor.Value = accountSecretKey;
            await _twoFactorService.SetTwoFactorAsync(currentTwoFactor);

            var twoFactorAuthInfo = new TwoFactorAuthInfo
            {
                Secret = setupInfo.ManualEntryKey,
                Email = user.Email,
                ApplicationName = "TestApplication",
                QrCodeSetupImageUrl = setupInfo.QrCodeSetupImageUrl
            };

            return twoFactorAuthInfo;
        }

        [HttpPost]
        public async Task<bool> ValidateAndSaveGoogleAuth(string code)
        {
            var user = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser;
            try
            {
                var tfa = new TwoFactorAuthenticator();
                var currentTwoFactor = await _twoFactorService.GetTwoFactor(user.Id, "Stuff");
                if (currentTwoFactor is null)
                    return false;

                var isValid = tfa.ValidateTwoFactorPIN(currentTwoFactor.Value, code);
                if (!isValid)
                    return false;

                currentTwoFactor.IsConfirmed = true;
                await _twoFactorService.SetTwoFactorAsync(currentTwoFactor);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
            //return _twoFactorService.ValidateAndSaveGoogleAuth(code, user.Id);
        }

        [HttpPost]
        public bool Disable()
        {
            var result = 0;
            var isAdmin = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser.Groups.Select(x => x.Name == "Administrators").FirstOrDefault();
            if (isAdmin)
            {
                var user = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser;
                //TODO: Disable
                //result = _twoFactorService.Disable(user.Id);
                //if more than 0 rows have been deleted, the query ran successfully
            }
            return result != 0;
        }
    }
}
