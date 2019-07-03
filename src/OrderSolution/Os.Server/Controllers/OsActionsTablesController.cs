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
    public class OsActionsTablesController : Controller
    {
        /// <summary>
        /// Create a new subtable.
        /// </summary>
        /// <param name="tableId">The table this sub table is created for.</param>
        /// <response code="201"></response>
        [HttpPost]
        [Route("/api/v2/actions/SubTables/Create")]
        public virtual IActionResult CreateSubTable([FromQuery] string tableId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                //register client
                var subTable = Logic.Table.CreateSubTable(session, tableId);
                //201 - Created
                return new CreatedResult("SubTables/Create", subTable);
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
        /// Open table by name. If it does not exist it will be created.
        /// </summary>
        /// <param name="name">The Name of the table to be opened. Is unique in the service area defined.</param>
        /// <param name="serviceAreaId">The ID of the service area in which the table should be opened.</param>
        /// <param name="prePayment">If provided defines if the request is in preparation of a payment action.</param>
        /// <response code="200">Existing table opened.</response>
        /// <response code="201">New table opened.</response>
        [HttpPost]
        [Route("/api/v2/actions/Tables/OpenByName/{name}")]
        public IActionResult OpenTableByName([FromRoute][Required] string name, [FromQuery] string serviceAreaId, [FromQuery] bool? prePayment)
        {
            try
            {
                var session = Sessions.GetSession(Request);

                if (prePayment == null)
                    prePayment = false;
                var tableResult = Logic.Table.OpenByName(session, name, serviceAreaId, (bool)prePayment);
                //201 - Created
                //return new CreatedResult("Tables/OpenByName", tableResult);
                //200 - Ok
                return new OkObjectResult(tableResult);
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
        /// Finalize the current order so that they the open orders are commited and the table is getting unlocked.
        /// </summary>
        /// <param name="tableId">Table to commit orders for.</param>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="409">Conflict</response>
        [HttpPost]
        [Route("/api/v2/actions/Tables/FinalizeOrder/{tableId}")]
        public virtual IActionResult FinalizeTableOrder([FromRoute][Required] string tableId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                Logic.Order.FinalizeOrder(session, tableId);
                //204 - No Content
                return new NoContentResult();
                //401 - Unauthorized
                //return new UnauthorizedResult();
                //409 - Conflict
                //return new ConflictResult();
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
        /// Cancles the current order procedure for the table. The table will be unlocked and all uncommited orders will be deleted.
        /// </summary>
        /// <param name="tableId">Table to commit orders for.</param>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="409">Conflict</response>
        [HttpPost]
        [Route("/api/v2/actions/Tables/CancelOrder/{tableId}")]
        public virtual IActionResult CancelOrder([FromRoute][Required] string tableId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                Logic.Order.CancelOrder(session, tableId);
                //204 - No Content
                return new NoContentResult();
                //401 - Unauthorized
                //return new UnauthorizedResult();
                //409 - Conflict
                //return new ConflictResult();
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
    }
}
