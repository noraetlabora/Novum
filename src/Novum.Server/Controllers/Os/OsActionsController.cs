using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Data;
using Novum.Data.Os;

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
        /// <param name="loginUser"></param>
        /// <response code="204"></response>
        /// <response code="401"></response>
        [HttpPost]
        [Route("/api/v2/actions/Auth/Login")]
        public IActionResult AuthLogin([FromBody][Required]LoginUser loginUser)
        {
            var session = Data.Sessions.GetSession(Request);
            try
            {
                session.WaiterId = loginUser.Id;
                Logic.Os.Registration.Login(session, loginUser);
                Data.Sessions.SetSession(session);
                //204 - No Content
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                session.WaiterId = "";
                Data.Sessions.SetSession(session);
                var osError = new OsError();
                osError.ErrorMsg = ex.Message;
                //401 - Unauthorized
                return new UnauthorizedObjectResult(osError);
            }
        }

        /// <summary>
        /// Logout from the server.
        /// </summary>
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/Auth/Logout")]
        public IActionResult AuthLogout()
        {
            var session = Data.Sessions.GetSession(Request);
            session.WaiterId = "";
            Data.Sessions.SetSession(session);
            //200 - Ok
            return new OkObjectResult(null);
        }

        /// <summary>
        /// Get initialization data for the client. Clients will have to call transactions/login to execute meaningful transactions.
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="412"></response>
        [HttpPost]
        [Route("/api/v2/actions/Init/RegisterClient")]
        public IActionResult InitRegisterClient([FromBody][Required]ClientInfo clientInfo)
        {
            var session = Data.Sessions.GetSession(Request);
            session = Logic.SessionHelper.Initialize(session, clientInfo.Id);
            try
            {
                //register client
                var posInfo = Logic.Os.Registration.RegisterClient(session, clientInfo);
                //save session internal and return the sessionId in the response
                Data.Sessions.SetSession(session);
                Response.Cookies.Append("sessionId", session.Id);
                //200 - Ok
                return new OkObjectResult(posInfo);
            }
            catch (Exception ex)
            {
                var osError = new OsError();
                osError.ErrorMsg = ex.Message;
                //400 - BadRequest
                return new BadRequestObjectResult(osError);
            }
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