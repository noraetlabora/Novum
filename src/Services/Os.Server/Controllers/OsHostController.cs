using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public static Models.PosStatus PosStatus = new Models.PosStatus { SessionId = DateTime.Now.ToString("dd.MM.yyyy_HH:mm:ss.fffff") };

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/hostStatus")]
        public IActionResult GetHostStatus()
        {
            try
            {
                return new OkObjectResult(PosStatus);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        private IActionResult GetExceptionResponse(Exception ex, int httpStatusCode)
        {
            Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Path + "|");
            var osError = new Models.OsError();
            osError.ErrorMsg = ex.Message;
            return StatusCode(httpStatusCode, osError);
        }
    }
}
