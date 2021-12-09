using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;

namespace Perplex.Components
{
    public class DatabaseComponent : IComponent
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IKeyValueService _keyValueService;
        private readonly IMigrationPlanExecutor _migrationPlanExecutor;

        public DatabaseComponent(IMigrationPlanExecutor migrationPlanExecutor, IScopeProvider scopeProvider, IKeyValueService keyValueService)
        {
            _migrationPlanExecutor = migrationPlanExecutor;
            _scopeProvider = scopeProvider;
            _keyValueService = keyValueService;
        }

        public void Initialize()
        {
            var plan = new MigrationPlan("SimpleRedirectsMigration");
            plan.From(string.Empty);
            var upgrader = new Upgrader(plan);
            upgrader.Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
