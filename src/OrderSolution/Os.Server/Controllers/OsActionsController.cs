using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Os.Server.Controllers
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
        public IActionResult AuthLogin([FromBody][Required] Models.LoginUser loginUser)
        {
            var session = Sessions.GetSession(Request);
            try
            {
                Logic.Registration.Login(session, loginUser);
                //204 - No Content
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                session.WaiterId = "";
                var osError = new Models.OsError();
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
            try 
            {
                var session = Sessions.GetSession(Request);
                session.WaiterId = "";
                //200 - Ok
                return new OkObjectResult(null);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
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
        public IActionResult InitRegisterClient([FromBody][Required] Models.ClientInfo clientInfo)
        {
            try 
            {
                var session = Sessions.GetSession(Request);
                if (session == null)
                {
                    session = new Nt.Data.Session();
                    session.SerialNumber = clientInfo.Id;
                    session.ClientId = Logic.Data.GetClientId();
                    session.PosId = Logic.Data.GetPosId(clientInfo.Id);
                    session.ServiceAreaId = Logic.Data.GetServiceAreaId(session.PosId);
                    session.PriceLevel = Logic.Data.GetPriceLevel(session.ServiceAreaId);
                    session.Printer = clientInfo.PrinterPath;
                    session.FiscalProvider = Logic.Payment.GetFiscalProvider(session.ClientId, session.PosId, session.SerialNumber);
                }
                session.WaiterId = "";

                //register client
                var registerClientResponse = Logic.Registration.RegisterClient(session, clientInfo);
                //save session internal and return the sessionId in the response
                Sessions.SetSession(session);
                Response.Cookies.Append("sessionId", session.Id);
                
                //200 - Ok
                return new OkObjectResult(registerClientResponse);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                var osError = new Models.OsError();
                osError.ErrorMsg = ex.Message;
                //400 - BadRequest
                return new BadRequestObjectResult(osError);
            }
        }

        /// <summary>
        /// Informs about a gateway (like OsServer) that is configured to work with this POS. A gateway is a system / server that clients use to communicate with the server.
        /// </summary>
        /// <param name="gatewayInfo">Information about the gateway.</param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/api/v2/actions/Init/RegisterGateway")]
        public IActionResult InitRegisterGateway([FromBody][Required] Models.GatewayInfo gatewayInfo)
        {
            try 
            {
                //register gateway
                var registerGatewayResponse = new Models.RegisterGatewayResponse();
                //var serviceAreaId = Nt.Database.DB.Api.Pos.GetServiceAreaId(posId);
                //registerGatewayResponse.RestaurantName = Nt.Database.DB.Api.Pos.GetServiceAreaName(serviceAreaId);
                registerGatewayResponse.PartnerId = "1OSZATNOVACOMSOF1HdRnQ3bZ0t2BED3003";
                //200 - Ok
                return new OkObjectResult(registerGatewayResponse);
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
