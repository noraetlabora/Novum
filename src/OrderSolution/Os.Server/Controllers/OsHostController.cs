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
        
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Models.PosStatus PosStatus = new Models.PosStatus {SessionId = DateTime.Now.ToString("dd.MM.yyyy_HH:mm:ss.ffff") };
        
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
                return new OkObjectResult(PosStatus);
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
