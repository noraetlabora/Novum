using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        [Route("/api/v2/data/Articles")]
        public IActionResult GetArticles()
        {
            try 
            {
                var articles = Logic.Data.GetCachedArticles();
                return new ObjectResult(articles);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/CancellationReasons")]
        public IActionResult GetCancellationReasons()
        {
            try 
            {
                var osCReasons = Logic.Data.GetCancellationReasons();
                return new ObjectResult(osCReasons);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Categories")]
        public IActionResult GetCategories()
        {
            try 
            {
                var categories = Logic.Data.GetCachedCategories("1");
                return new ObjectResult(categories);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/ModifierGroups")]
        public IActionResult GetModifierGroups()
        {
            try 
            {
                var modifierGroups = Logic.Data.GetModifierGroups();
                return new ObjectResult(modifierGroups);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/OsConfiguration")]
        public IActionResult GetOsConfiguration()
        {
            try 
            {
                var osConfiguration = new Models.OsConfiguration();
                osConfiguration.Global = new Dictionary<string, string>();
                osConfiguration.Global.Add("language", "de");
                osConfiguration.Global.Add("locale", "de_DE");
                return new ObjectResult(osConfiguration);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/PaymentMedia")]
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
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Printers")]
        public IActionResult GetPrinters()
        {
            try 
            {
                var printers = Logic.Data.GetPrinters();
                return new ObjectResult(printers);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/ServiceAreas")]
        public IActionResult GetServiceAreas()
        {
            try 
            {
                var serviceAreas = Logic.Data.GetServiceAreas();
                return new OkObjectResult(serviceAreas);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Users")]
        public IActionResult GetUsers()
        {
            try 
            {
                var users = Logic.Data.GetUsers();
                return new ObjectResult(users);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get the order lines possibly filtered by the sub table id. NOTE: Only commited order lines (where actions/tables/FinalizeOrder was called) will be returned by this call.
        /// </summary>
        /// <param name="subTableId">The sub table ID getting orderlines for.</param>
        /// <param name="status">[ "unknown", "ordered", "committed", "paid" ] If specified the status of the orderlines that are required. If not provided all orderlines with status Ordered and Committed are returned</param>
        /// <response code="200">Orderlines with their unpaid quantity/price</response>
        [HttpGet]
        [Route("/api/v2/data/OrderLines")]
        public IActionResult GetOrderLines([FromQuery] string subTableId, [FromQuery] string status)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                if (session == null)
                    return new UnauthorizedResult();

                var orderLines = Logic.Order.GetOrderLines(session, subTableId);
                return new ObjectResult(orderLines);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get open tables for the service area.
        /// </summary>
        /// <param name="serviceAreaId">The service area id of the service area.</param>
        /// <response code="200">open tables array</response>
        [HttpGet]
        [Route("/api/v2/data/Tables")]
        public IActionResult GetTables([FromQuery] string serviceAreaId)
        {
            try 
            {
                var session = Sessions.GetSession(Request);
                if (session == null)
                    return new UnauthorizedResult();

                var tables = Logic.Table.GetTables(session);
                return new ObjectResult(tables);
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
