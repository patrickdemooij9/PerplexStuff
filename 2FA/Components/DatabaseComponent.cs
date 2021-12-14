using Perplex._2FA.Migrations;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;

namespace Perplex._2FA.Components
{
    public class DatabaseComponent : IComponent
    {
        private readonly IMigrationPlanExecutor _planExecutor;
        private readonly IScopeProvider _scopeProvider;
        private readonly IKeyValueService _keyValueService;

        public DatabaseComponent(IMigrationPlanExecutor planExecutor, IScopeProvider scopeProvider, IKeyValueService keyValueService)
        {
            _planExecutor = planExecutor;
            _scopeProvider = scopeProvider;
            _keyValueService = keyValueService;
        }

        public void Initialize()
        {
            var plan = new MigrationPlan("TwoFactoryMigration");
            plan.From(string.Empty)
                .To<InitialMigration>("state-1");

            var upgrader = new Upgrader(plan);
            upgrader.Execute(_planExecutor, _scopeProvider, _keyValueService);
        }

        public void Terminate()
        {
        }
    }
}
