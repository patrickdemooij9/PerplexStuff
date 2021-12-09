using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.Common.Controllers;

namespace Perplex._2FA
{
    public class TestController : UmbracoApiController
    {
        private readonly IBackOfficeUserManager _userManager;

        public TestController(IBackOfficeUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
