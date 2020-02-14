using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        [Route("/api/v2/actions/subTables/create")]
        public async Task<IActionResult> CreateSubTable([FromQuery] string tableId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                //register client
                var subTable = await Logic.Table.CreateSubTable(session, tableId);
                //201 - Created
                return new CreatedResult("/api/v2/actions/subTables/create", subTable);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Move sub tables to another table. This can be a single or multiple or all subTables of a table.
        /// </summary>
        /// <param name="data">Defines the data for the action (source subTables to move + target table to move to)</param>
        /// <response code="200">On success the new/updated data of the target table. This is the same result as from a call to tables/openByName.</response>
        [HttpPost]
        [Route("/api/v2/actions/subTables/move")]
        public async Task<IActionResult>SubTablesMove([FromBody]Models.MoveSubtables data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var moveResult = await Logic.Table.MoveSubTable(session, data);
                //200 - Ok
                return new OkObjectResult(moveResult);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
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
        [Route("/api/v2/actions/tables/openByName/{name}")]
        public async Task<IActionResult> OpenTableByName([FromRoute][Required] string name, [FromQuery] string serviceAreaId, [FromQuery] bool? prePayment)
        {
            try
            {
                var session = Sessions.GetSession(Request);

                if (prePayment == null)
                    prePayment = false;
                var tableResult = await Logic.Table.OpenByName(session, name, serviceAreaId, (bool)prePayment);
                //201 - Created
                //return new CreatedResult("Tables/OpenByName", tableResult);
                //200 - Ok
                return new OkObjectResult(tableResult);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
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
        [Route("/api/v2/actions/tables/finalizeOrder/{tableId}")]
        public async Task<IActionResult> FinalizeTableOrder([FromRoute][Required] string tableId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                await Logic.Order.FinalizeOrder(session, tableId);
                //204 - No Content
                return new NoContentResult();
                //401 - Unauthorized
                //return new UnauthorizedResult();
                //409 - Conflict
                //return new ConflictResult();
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status409Conflict);
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
        [Route("/api/v2/actions/tables/cancelOrder/{tableId}")]
        public async Task<IActionResult> CancelOrder([FromRoute][Required] string tableId)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                await Logic.Order.CancelOrder(session, tableId);
                //204 - No Content
                return new NoContentResult();
                //401 - Unauthorized
                //return new UnauthorizedResult();
                //409 - Conflict
                //return new ConflictResult();
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Modifiers properties of the given table.
        /// </summary>

        /// <param name="tableId">The table of which the properties are updated.</param>
        /// <param name="newProperties">The properties with their values to be updated. NOTE: Only the properties that are given in this options are updated. If a property is not part of this object it must not be changed.</param>
        /// <response code="204"></response>
        /// <response code="401"></response>
        /// <response code="403"></response>
        /// <response code="409"></response>
        [HttpPatch]
        [Route("/api/v2/actions/tables/modifyProperties/{tableId}")]
        public async Task<IActionResult> TablesModifyProperties([FromRoute][Required]string tableId, [FromBody]Models.TableProperties newProperties)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                // return StatusCode(204);
                // return StatusCode(401);
                // return StatusCode(403);
                // return StatusCode(409);
                throw new NotImplementedException("/api/v2/actions/tables/modifyProperties/ not implemented");
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>

        /// <param name="tableId"></param>
        /// <param name="data"></param>
        /// <response code="200">New course added</response>
        [HttpPost]
        [Route("/api/v2/actions/tables/addCourse/{tableId}")]
        public async Task<IActionResult> TablesAddCourse([FromRoute][Required]string tableId, [FromBody]Models.AddCourseData data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                throw new NotImplementedException("/api/v2/actions/tables/addCourse/ not implemented");
                //200 - Ok
                //return new OkObjectResult(null);
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
