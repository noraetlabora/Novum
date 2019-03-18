using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Server.Models.Os;

namespace Novum.Server.Controllers.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsActionsController : Controller
    {
        /// <summary>
        /// Execute a login transaciton. NOTE: Will set an \&quot;AuthToken\&quot; cookie needed in later authorized requests.
        /// </summary>
        /// <param name="data"></param>
        /// <response code="201"></response>
        /// <response code="401"></response>
        [HttpPost]
        [Route("/api/v2/actions/Auth/Login")]
        public IActionResult AuthLogin([FromBody][Required]LoginUser data)
        {
            var osError = new OsError();
            if (string.IsNullOrEmpty(data.Id))
            {
                osError.ErrorMsg = "no Id";
                //401 - Unauthorized
                return new UnauthorizedObjectResult(osError);
            }
            if (string.IsNullOrEmpty(data.Password))
            {
                osError.ErrorMsg = "no Password";
                //401 - Unauthorized
                return new UnauthorizedObjectResult(osError);
            }
            //200 - Ok
            return new OkObjectResult(null);
        }

        /// <summary>
        /// Logout from the server.
        /// </summary>
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/Auth/Logout")]
        public IActionResult AuthLogout()
        {
            //200 - Ok
            return new OkObjectResult(null);
        }

        /// <summary>
        /// Get initialization data for the client. Clients will have to call transactions/login to execute meaningful transactions.
        /// </summary>
        /// <param name="clientData"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="412"></response>
        [HttpPost]
        [Route("/api/v2/actions/Init/RegisterClient")]
        public IActionResult InitRegisterClient([FromBody][Required]ClientInfo clientData)
        {
            var posInfo = new POSInfo();
            var osError = new OsError();

            //
            posInfo.ClientName = "Norli";
            posInfo.RestaurantName = "Müllnerbräu";
            posInfo.UtcTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            //osError.ErrorMsg = "Bad Request";
            ////400 - Bad Request
            //return new BadRequestJsonResult(osError);
            //osError.ErrorMsg = "Precondition Failed";
            ////412 - Precondition Failed
            //return new ContentResult() { StatusCode = 412, Content = osError.ToJson() };

            //200 - Ok
            return new OkObjectResult(posInfo);
        }

        /// <summary>
        /// Informs about a gateway (like OsServer) that is configured to work with this POS. A gateway is a system / server that clients use to communicate with the server.
        /// </summary>
        /// <param name="gatewayInfo">Information about the gateway.</param>
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/Init/RegisterGateway")]
        public IActionResult InitRegisterGateway([FromBody][Required]GatewayInfo gatewayInfo)
        {
            return new NoContentResult();
        }
    }
}