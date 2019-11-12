using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Os.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OsActionsPayController : Controller
    {
        /// <summary>
        /// Pay sub full subtables. If the pay action is executed successfully the subtables will be fully paid and clsoed.
        /// </summary>
        /// <param name="data">Pay data describing which payments have to be executed for which subtables</param>
        /// <response code="204">OK in case the payment was successfull and the receipt is moved to the printer queue.</response>
        [HttpPost]
        [Route("/api/v2/actions/pay/subTables")]
        public virtual IActionResult PaySubTables([FromBody][Required] Models.PaySubTables data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                Logic.Payment.PaySubTables(session, data);
                Logic.Printer.Print(session);
                // 204 - No Content 
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Path + "|");
                var osError = new Models.OsError();
                osError.ErrorMsg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, osError);
            }
        }

        /// <summary>
        /// Will be used when a payment medium was used that is configured to require pre-authorization. If accepted (OK, 200) the medium will be used for the pay action. If not accepted (Conflict, 409) it will be ignored.
        /// </summary>

        /// <param name="data"></param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        /// <response code="409"></response>
        [HttpPost]
        [Route("/api/v2/actions/pay/preAuthorize")]
        public virtual IActionResult PayPreAuthorize([FromBody]Models.PreAuthData data)
        {
            throw new NotImplementedException("/api/v2/actions/pay/preAuthorize is not yet implemented");
        }

        /// <summary>
        /// Pay individual orderlines. Typically used when the bill is splitted.
        /// </summary>
        /// <param name="data">The orderlines to be paid + payment info.</param>
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/pay/orderLines")]
        public IActionResult PayOrderLines([FromBody] Models.PayOrderLines data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                Logic.Payment.PayOrderLines(session, data);
                Logic.Printer.Print(session);
                // 204 - No Content 
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Path + "|");
                var osError = new Models.OsError();
                osError.ErrorMsg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, osError);
            }
        }
    }
}
