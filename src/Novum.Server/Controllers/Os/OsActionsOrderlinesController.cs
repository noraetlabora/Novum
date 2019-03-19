using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Data.Models.Os;

namespace Novum.Server.Controllers.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsActionsOrderlinesController : Controller
    {
        /// <summary>
        /// Modifies (&#x3D; replaces) an uncommited orderline. IMPORTANT: Any existing modifiers of the orderline are replaced with the new modifiers.  For example if an orderline \&quot;1x Coke with ice\&quot; is modified to be \&quot;1x Coke with Citron\&quot; the ice is also gone so the operation works as a replacement and not as a patch of the existing data.
        /// </summary>
        /// <param name="orderLineId">The orderline to be modified / replaced</param>
        /// <param name="data">The new data to be used for this order line (same as for the add request)</param>
        /// <response code="201">OrderLineResult informing about the new ids / prices of the modified orderline.</response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/Void/{orderLineId}")]
        public virtual IActionResult VoidOrderLines([FromRoute][Required]string orderLineId, [FromBody][Required]OrderLineVoid data)
        {
            Log.Json.Info(Request.Path.Value);
            var osError = new OsError();
            var olVoidResult = new OrderLineVoidResult();

            //

            //

            // 204 - No Content - Delete Orderline (Orderline (1x Water, 1x Void, return NoContentResult))
            //return new NoContentResult();
            // 200 - OK - Update Orderline (Orderline 3x Water, 1x Void, return OrderLineVoidResult with 2x Water)
            return new OkObjectResult(olVoidResult);
        }

        /// <summary>
        /// Add a new orderline to a sub table. (e.g. when the waiter orders an article on a table)
        /// </summary>
        /// <param name="subTableId">The ID of the subtable to add the order line to. Note: Orders are always part of a subtable and subtables are part of a table. This means at least 1 subtable exists for each table to allow ordering / adding orderlines.</param>
        /// <param name="data">The orderline to add.</param>
        /// <response code="201"></response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/Add/{subTableId}")]
        public IActionResult AddOrderLines([FromRoute][Required]string subTableId, [FromBody][Required]OrderLineAdd data)
        {
            var osError = new OsError();
            var olResult = new OrderLineResult();

            //
            olResult.Id = "article0";
            olResult.SinglePrice = 1299;
            olResult.Modifiers = new List<OrderLineResultModifier>();
            var modifier = new OrderLineResultModifier() { Id = "modifier0" };
            olResult.Modifiers.Add(modifier);
            //

            //201 - Created
            return new CreatedResult("OrderLines/Add", olResult);
        }

        /// <summary>
        /// Modifies (&#x3D; replaces) an uncommited orderline. IMPORTANT: Any existing modifiers of the orderline are replaced with the new modifiers.  For example if an orderline \&quot;1x Coke with ice\&quot; is modified to be \&quot;1x Coke with Citron\&quot; the ice is also gone so the operation works as a replacement and not as a patch of the existing data.
        /// </summary>
        /// <param name="orderLineId">The orderline to be modified / replaced</param>
        /// <param name="data">The new data to be used for this order line (same as for the add request)</param>
        /// <response code="201">OrderLineResult informing about the new ids / prices of the modified orderline.</response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/ModifyUncommitted/{orderLineId}")]
        public IActionResult ModifyOrderLinesUncommitted([FromRoute][Required]string orderLineId, [FromBody][Required]OrderLineModify data)
        {
            var osError = new OsError();
            var olResult = new OrderLineResult();

            //
            olResult.Id = "article0";
            olResult.SinglePrice = 1299;
            olResult.Modifiers = new List<OrderLineResultModifier>();
            var modifier = new OrderLineResultModifier() { Id = "modifier0" };
            olResult.Modifiers.Add(modifier);
            //

            //201 - Created
            return new CreatedResult("OrderLines/ModifyUncommitted", olResult);
        }
    }
}