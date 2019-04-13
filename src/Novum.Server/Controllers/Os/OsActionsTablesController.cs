using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Data.Os;

namespace Novum.Server.Controllers.Os
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
            var subTable = new SubTable();

            //
            subTable.Id = tableId + ".1";
            subTable.Name = tableId + ".1";
            subTable.IsSelected = true;
            //

            //201 - Created
            return new CreatedResult("SubTables/Create", subTable);
        }

        /// <summary>
        /// Open table by name. If it does not exist it will be created.
        /// </summary>
        /// <param name="name">The Name of the table to be opened. Is unique in the service area defined.</param>
        /// <param name="serviceAreaId">The ID of the service area in which the table should be opened.</param>
        /// <param name="prePayment">If provided defines if the request is in preparation of a payment action.</param>
        /// <response code="200"></response>
        /// <response code="201"></response>
        [HttpPost]
        [Route("/api/v2/actions/Tables/OpenByName/{name}")]
        public IActionResult OpenTableByName([FromRoute][Required] string name, [FromQuery] string serviceAreaId, [FromQuery] bool? prePayment)
        {
            var tableResult = new TableResult();

            //
            tableResult.Id = "1001.1";
            tableResult.Name = "1.1";
            tableResult.BookedAmount = 12398;
            tableResult.LastActivityTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 5000;
            tableResult.ServiceAreaId = "RES";
            //

            ////201 - Created
            //return new CreatedResult("Tables/OpenByName", tableResult);
            //200 - Ok
            return new OkObjectResult(tableResult);
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
            //204 - No Content
            return new NoContentResult();
            ////401 - Unauthorized
            //return new UnauthorizedResult();
            ////409 - Conflict
            //return new ConflictResult();
        }
    }
}
