using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Server.Models.Os;

namespace Novum.Server.Controllers.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsHostController : Controller
    {
        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/HostStatus")]
        public IActionResult GetHostStatus()
        {
            var posStatus = new PosStatus();
            //
            posStatus.SessionId = "1";
            //
            return new OkObjectResult(posStatus);
        }
    }
}