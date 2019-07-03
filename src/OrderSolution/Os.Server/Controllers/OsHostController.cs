using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Os.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OsHostController : Controller
    {
        private static DateTime serverStart = DateTime.Now;

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/HostStatus")]
        public IActionResult GetHostStatus()
        {
            try
            {
                var posStatus = new Models.PosStatus();
                posStatus.SessionId = serverStart.ToString("dd.MM.yyyy_HH:mm:ss.ffff");
                return new OkObjectResult(posStatus);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }
    }
}
