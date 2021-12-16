using System;
using Umbraco.Cms.Core.Dashboards;

namespace Perplex._2FA.Dashboard
{
    public class ManagementDashboard : IDashboard
    {
        public string Alias => "2faManagementDashboard";
        public string View => "/App_Plugins/2FA/managementdashboard.html";
        public string[] Sections => new[] {"Content"};
        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();
    }
}
