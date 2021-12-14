using Perplex._2FA.Models.Database;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Perplex._2FA.Migrations
{
    public class InitialMigration : MigrationBase
    {
        public InitialMigration(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            if (!TableExists("TwoFactor"))
            {
                Create.Table<TwoFactorEntity>().Do();
            }
        }
    }
}
