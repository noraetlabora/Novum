using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                var articles = Logic.Data.GetCachedArticles();
                return new ObjectResult(articles);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/cancellationReasons")]
        public async Task<IActionResult> GetCancellationReasons()
        {
            try
            {
                var osCReasons = await Logic.Data.GetCancellationReasons();
                return new ObjectResult(osCReasons);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                //HACK: always use Menu Category "1"
                var categories = Logic.Data.GetCachedCategories("1");
                return new ObjectResult(categories);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/modifierGroups")]
        public async Task<IActionResult> GetModifierGroups()
        {
            try
            {
                var modifierGroups = await Logic.Data.GetModifierGroups();
                return new ObjectResult(modifierGroups);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/osConfiguration")]
        public async Task<IActionResult> GetOsConfiguration()
        {
            try
            {
                var osConfiguration = new Models.OsConfiguration();
                //globals
                osConfiguration.Global = new Dictionary<string, string>();
                osConfiguration.Global.Add("language", OsServer.ClientConfiguration.GetLanguageCode());
                osConfiguration.Global.Add("locale", OsServer.ClientConfiguration.Localization);
                osConfiguration.Global.Add("priceEntryMode", OsServer.ClientConfiguration.PriceEntryMode);
                osConfiguration.Global.Add("disableSubtables", OsServer.ClientConfiguration.DisableSubtables ? "1" : "0");
                osConfiguration.Global.Add("authenticationMode", OsServer.ClientConfiguration.AuthenthicationMode);
                osConfiguration.Global.Add("coursingMode", OsServer.ClientConfiguration.Coursing ? "manual" : "disabled");
                //features
                osConfiguration.Features = new List<string>();
                if (OsServer.ClientConfiguration.FeatureMoveAllSubTables)
                    osConfiguration.Features.Add("moveAllSubTables");
                if (OsServer.ClientConfiguration.FeatureMoveSingleSubTable)
                    osConfiguration.Features.Add("moveSingleSubTable");
                if (OsServer.ClientConfiguration.FeatureTip)
                    osConfiguration.Features.Add("tip");
                if (OsServer.ClientConfiguration.Coursing)
                    osConfiguration.Features.Add("addCourseOnLongPress");

                //TODO: delete folowing 2 lines - manual course and addCourseOnLongPress
                osConfiguration.Global.Add("coursingMode", "manual");
                osConfiguration.Features.Add("addCourseOnLongPress");

                return new ObjectResult(osConfiguration);

            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get the predefined available payment media types.
        /// </summary>
        /// <remarks>IMPORTANT:  The first medium has a special behavior as it is available 2 times:  - It is shown in the payment media list in the payment media screen as configured and can be used normally. - It is shown as a quick payment button left to the \&quot;Pay...\&quot; button in the payment screen (&#x3D; quick pay button). It will be used as quick payment method      and will have \&quot;askForAmount\&quot; flag be forced to false (and so also \&quot;allowOverPayment\&quot; is also false). It will be used to fully pay the      whole selected amount with the specified tip.      Example: If a check with 22 EUR + 2 tip is paid with the quick pay button which is named \&quot;Pay Cash\&quot; in the demo the payment will be executed             with the \&quot;Cash\&quot; media type (as this is the first one in the list) and it will pay with the full amount (22 EUR) on the current check and the tip (2 EUR)</remarks>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/paymentMedia")]
        public async Task<IActionResult> GetPaymentMedia()
        {
            try
            {
                var paymentMedia = await Logic.Data.GetPaymentMedia();
                await Nt.Database.DB.Api.Misc.SetSnapshotTime(Controllers.OsHostController.PosStatus.SessionId);
                return new ObjectResult(paymentMedia);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/printers")]
        public async Task<IActionResult> GetPrinters()
        {
            try
            {
                var printers = await Logic.Printer.GetPrinters();
                return new ObjectResult(printers);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/serviceAreas")]
        public async Task<IActionResult> GetServiceAreas()
        {
            try
            {
                var serviceAreas = await Logic.Data.GetServiceAreas();
                return new OkObjectResult(serviceAreas);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await Logic.Data.GetUsers();
                return new ObjectResult(users);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
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
        public async Task<IActionResult> GetOrderLines([FromQuery] string subTableId, [FromQuery] string status)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLines = await Logic.Order.GetOrderLines(session, subTableId);
                return new ObjectResult(orderLines);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get open tables for the service area.
        /// </summary>
        /// <param name="serviceAreaId">The service area id of the service area.</param>
        /// <response code="200">open tables array</response>
        [HttpGet]
        [Route("/api/v2/data/tables")]
        public async Task<IActionResult> GetTables([FromQuery] string serviceAreaId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var tables = await Logic.Table.GetTables(session);
                return new ObjectResult(tables);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Only supported if coursing features is enabled (see osConfiguration for details). Get list of standard courses.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/courses")]
        public async Task<IActionResult>CoursesGet()
        {
            try
            {
                var courses = await Logic.Data.GetCourses();
                return new ObjectResult(courses);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Only supported if coursing features is enabled (see osConfiguration for details). Get list of standard courses.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/guestTypes")]
        public async Task<IActionResult>GuestTypesGet()
        {
            try
            {
                var session = Sessions.GetSession(Request);

                throw new NotImplementedException("/api/v2/data/guestTypes is not yet implemented");

                //return new ObjectResult(null);
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
