using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Os.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OsDataController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/articles")]
        public IActionResult GetArticles()
        {
            try
            {
                var articles = Logic.Data.GetCachedArticles();
                return new ObjectResult(articles);
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/cancellationReasons")]
        public IActionResult GetCancellationReasons()
        {
            try
            {
                var osCReasons = Logic.Data.GetCancellationReasons();
                return new ObjectResult(osCReasons);
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
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/categories")]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = Logic.Data.GetCachedCategories("1");
                return new ObjectResult(categories);
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
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/modifierGroups")]
        public IActionResult GetModifierGroups()
        {
            try
            {
                var modifierGroups = Logic.Data.GetModifierGroups();
                return new ObjectResult(modifierGroups);
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
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/osConfiguration")]
        public IActionResult GetOsConfiguration()
        {
            try
            {
                var osConfiguration = new Models.OsConfiguration();
                osConfiguration.Global = new Dictionary<string, string>();
                osConfiguration.Global.Add("language", OsServer.ClientConfiguration.GetLanguageCode());
                osConfiguration.Global.Add("locale", OsServer.ClientConfiguration.Localization);
                osConfiguration.Global.Add("priceEntryMode", OsServer.ClientConfiguration.PriceEntryMode);
                osConfiguration.Global.Add("disableSubtables", OsServer.ClientConfiguration.DisableSubtables ? "1" : "0");
                osConfiguration.Global.Add("authenticationMode", OsServer.ClientConfiguration.AuthenthicationMode);
                osConfiguration.Features = new List<string>();
                if (OsServer.ClientConfiguration.FeatureMoveAllSubTables)
                    osConfiguration.Features.Add("moveAllSubTables");
                if (OsServer.ClientConfiguration.FeatureMoveSingleSubTable)
                    osConfiguration.Features.Add("moveSingleSubTable");
                if (OsServer.ClientConfiguration.FeatureTip)
                    osConfiguration.Features.Add("tip");
                return new ObjectResult(osConfiguration);
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
        /// Get the predefined available payment media types.
        /// </summary>
        /// <remarks>IMPORTANT:  The first medium has a special behavior as it is available 2 times:  - It is shown in the payment media list in the payment media screen as configured and can be used normally. - It is shown as a quick payment button left to the \&quot;Pay...\&quot; button in the payment screen (&#x3D; quick pay button). It will be used as quick payment method      and will have \&quot;askForAmount\&quot; flag be forced to false (and so also \&quot;allowOverPayment\&quot; is also false). It will be used to fully pay the      whole selected amount with the specified tip.      Example: If a check with 22 EUR + 2 tip is paid with the quick pay button which is named \&quot;Pay Cash\&quot; in the demo the payment will be executed             with the \&quot;Cash\&quot; media type (as this is the first one in the list) and it will pay with the full amount (22 EUR) on the current check and the tip (2 EUR)</remarks>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/paymentMedia")]
        public IActionResult GetPaymentMedia()
        {
            try
            {
                var paymentMedia = Logic.Data.GetPaymentMedia();
                Logic.Data.InitialStaticDataSent = true;
                Nt.Database.DB.Api.Misc.SetSnapshotTime(Controllers.OsHostController.PosStatus.SessionId);
                return new ObjectResult(paymentMedia);
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
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/printers")]
        public IActionResult GetPrinters()
        {
            try
            {
                var printers = Logic.Data.GetPrinters();
                return new ObjectResult(printers);
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
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/serviceAreas")]
        public IActionResult GetServiceAreas()
        {
            try
            {
                var serviceAreas = Logic.Data.GetServiceAreas();
                return new OkObjectResult(serviceAreas);
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
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/users")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = Logic.Data.GetUsers();
                return new ObjectResult(users);
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
        /// Get the order lines possibly filtered by the sub table id. NOTE: Only commited order lines (where actions/tables/FinalizeOrder was called) will be returned by this call.
        /// </summary>
        /// <param name="subTableId">The sub table ID getting orderlines for.</param>
        /// <param name="status">[ "unknown", "ordered", "committed", "paid" ] If specified the status of the orderlines that are required. If not provided all orderlines with status Ordered and Committed are returned</param>
        /// <response code="200">Orderlines with their unpaid quantity/price</response>
        [HttpGet]
        [Route("/api/v2/data/orderLines")]
        public IActionResult GetOrderLines([FromQuery] string subTableId, [FromQuery] string status)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLines = Logic.Order.GetOrderLines(session, subTableId);
                return new ObjectResult(orderLines);
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
        /// Get open tables for the service area.
        /// </summary>
        /// <param name="serviceAreaId">The service area id of the service area.</param>
        /// <response code="200">open tables array</response>
        [HttpGet]
        [Route("/api/v2/data/tables")]
        public IActionResult GetTables([FromQuery] string serviceAreaId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var tables = Logic.Table.GetTables(session);
                return new ObjectResult(tables);
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
        /// Only supported if coursing features is enabled (see osConfiguration for details). Get list of standard courses.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/courses")]
        public virtual IActionResult CoursesGet()
        {
            try
            {
                var session = Sessions.GetSession(Request);

                throw new NotImplementedException("/api/v2/data/courses is not yet implemented");

                //return new ObjectResult(null);
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
        /// Only supported if coursing features is enabled (see osConfiguration for details). Get list of standard courses.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/guestTypes")]
        public virtual IActionResult GuestTypesGet()
        {
            try
            {
                var session = Sessions.GetSession(Request);

                throw new NotImplementedException("/api/v2/data/guestTypes is not yet implemented");

                //return new ObjectResult(null);
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
