using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Perplex._2FA.Models.Database;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Security;
using Umbraco.Extensions;

namespace Perplex._2FA.Services
{
    public class TwoFactorService
    {
        private readonly IScopeProvider _scopeProvider;

        public TwoFactorService(IScopeProvider scopeProvider)
        {
            //TODO: Everything database related should be in a repository
            _scopeProvider = scopeProvider;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(int userId)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var twoFactoryEntry = await scope.Database.FirstOrDefaultAsync<TwoFactorEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<TwoFactorEntity>()
                    .Where<TwoFactorEntity>(it => it.UserId == userId && it.IsConfirmed));

                return twoFactoryEntry is not null;
            }
        }

        public async Task<TwoFactorEntity> GetTwoFactor(int userId, string provider)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var twoFactoryEntry = await scope.Database.FirstOrDefaultAsync<TwoFactorEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<TwoFactorEntity>()
                    .Where<TwoFactorEntity>(it => it.UserId == userId && it.Provider == provider));

                return twoFactoryEntry;
            }
        }

        public async Task SetTwoFactorAsync(TwoFactorEntity twoFactorEntity)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Save(twoFactorEntity);
                scope.Complete();
            }
        }
    }
}
