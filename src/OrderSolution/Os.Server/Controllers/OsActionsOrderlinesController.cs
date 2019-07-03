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
    public class OsActionsOrderlinesController : Controller
    {
        /// <summary>
        /// Add a new orderline to a sub table. (e.g. when the waiter orders an article on a table)
        /// </summary>
        /// <param name="subTableId">The ID of the subtable to add the order line to. Note: Orders are always part of a subtable and subtables are part of a table. This means at least 1 subtable exists for each table to allow ordering / adding orderlines.</param>
        /// <param name="data">The orderline to add.</param>
        /// <response code="201"></response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/Add/{subTableId}")]
        public IActionResult AddOrderLines([FromRoute][Required] string subTableId, [FromBody][Required] Models.OrderLineAdd data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLineResult = Logic.Order.Add(session, subTableId, data);
                //201 - Created
                return new CreatedResult("/api/v2/actions/OrderLines/Add/", orderLineResult);
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
        /// Modifies (&#x3D; replaces) an uncommited orderline. IMPORTANT: Any existing modifiers of the orderline are replaced with the new modifiers.  For example if an orderline \&quot;1x Coke with ice\&quot; is modified to be \&quot;1x Coke with Citron\&quot; the ice is also gone so the operation works as a replacement and not as a patch of the existing data.
        /// </summary>
        /// <param name="orderLineId">The orderline to be modified / replaced</param>
        /// <param name="data">The new data to be used for this order line (same as for the add request)</param>
        /// <response code="201">OrderLineResult informing about the new ids / prices of the modified orderline.</response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/ModifyUncommitted/{orderLineId}")]
        public IActionResult ModifyOrderLinesUncommitted([FromRoute][Required] string orderLineId, [FromBody][Required] Models.OrderLineModify data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLineResult = Logic.Order.Modify(session, orderLineId, data);
                //201 - Created
                return new CreatedResult("/api/v2/actions/OrderLines/ModifyUncommitted/" + orderLineResult.Id, orderLineResult);
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
        /// Split an existing orderline into 2 orderlines so that they can later be modified / moved / ... independently. IMPORTANT: The split action is done to allow this operation in a singel transaction instead of calling void and add separately. E.g. if the original orderline is \&quot;3xCoke\&quot; and the provided split quantity is 2 the expected result is  first/original orderline with 1xCoke second orderline (new one) with 2xCoke.
        /// </summary>
        /// <param name="orderLineId">The orderline to split.</param>
        /// <param name="data">Details about how to split this orderlin.</param>
        /// <response code="201"></response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/Split/{orderLineId}")]
        public virtual IActionResult OrderLinesSplit([FromRoute][Required]string orderLineId, [FromBody]Models.OrderLineSplit data)
        { 
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLineResult = Logic.Order.Split(session, orderLineId, data);
                //201 - Created
                return new CreatedResult("/api/v2/actions/OrderLines/Split/" + orderLineResult.Id, orderLineResult);
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
        /// Modifies (&#x3D; replaces) an uncommited orderline. IMPORTANT: Any existing modifiers of the orderline are replaced with the new modifiers.  For example if an orderline \&quot;1x Coke with ice\&quot; is modified to be \&quot;1x Coke with Citron\&quot; the ice is also gone so the operation works as a replacement and not as a patch of the existing data.
        /// </summary>
        /// <param name="orderLineId">The orderline to be modified / replaced</param>
        /// <param name="data">The new data to be used for this order line (same as for the add request)</param>
        /// <response code="201">OrderLineResult informing about the new ids / prices of the modified orderline.</response>
        [HttpPost]
        [Route("/api/v2/actions/OrderLines/Void/{orderLineId}")]
        public virtual IActionResult VoidOrderLines([FromRoute][Required] string orderLineId, [FromBody][Required] Models.OrderLineVoid data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var voidResult = Logic.Order.Void(session, orderLineId, data);
                // 204 - No Content - delete Orderline
                if (voidResult.Quantity <= 0)
                    return new NoContentResult();
                // 200 - OK - update Orderline
                return new OkObjectResult(voidResult);
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
