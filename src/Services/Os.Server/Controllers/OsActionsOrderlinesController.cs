using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

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
        [Route("/api/v2/actions/orderLines/add/{subTableId}")]
        public IActionResult AddOrderLines([FromRoute][Required] string subTableId, [FromBody][Required] Models.OrderLineAdd data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLineResult = Logic.Order.Add(session, subTableId, data);
                //201 - Created
                return new CreatedResult("/api/v2/actions/orderLines/add/", orderLineResult);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Modifies (&#x3D; replaces) an uncommited orderline.
        /// </summary>
        /// <remarks>IMPORTANT:   - Any existing modifiers of the order line are replaced with the new modifiers.    For example if an order line \&quot;1x Coke with ice\&quot; is modified to be \&quot;1x Coke with Citron\&quot; the ice is also gone   so the operation works as a replacement and not as a patch of the existing data. - If the order line to be modified is a combo member the combo parameters are untouched.   This is the reason why these parameters are not part of the split operation.</remarks>
        /// <param name="orderLineId">The orderline to be modified / replaced</param>
        /// <param name="data">The new data to be used for this order line (same as for the add request)</param>
        /// <response code="201">OrderLineResult informing about the new ids / prices of the modified orderline.</response>
        [HttpPost]
        [Route("/api/v2/actions/orderLines/modifyUncommitted/{orderLineId}")]
        public IActionResult ModifyOrderLinesUncommitted([FromRoute][Required] string orderLineId, [FromBody][Required] Models.OrderLineModify data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLineResult = Logic.Order.Modify(session, orderLineId, data);
                //201 - Created
                return new CreatedResult("/api/v2/actions/orderLines/modifyUncommitted/" + orderLineResult.Id, orderLineResult);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Split an existing orderline into 2 orderlines so that they can later be modified / moved / ... independently.
        /// </summary>
        /// <remarks>IMPORTANT:   - The split action is done to allow this operation in a single transaction instead of calling void and add separately. - If the order line to be split is a combo member the combo parameters are untouched.   This is the reason why these parameters are not part of the split operation.  E.g. if the original order line is \&quot;3xCoke\&quot; and the provided split quantity is 2 the expected result is  first/original order line with 1xCoke second order line (new one) with 2xCoke. ATTENTION: When splitting an order line that has a FAX modifier the fax image must be copied so that as a result     The first/original order line has the original fax image ID and the second (new) order line has a copy of the first one with a new fax image ID.</remarks>
        /// <param name="orderLineId">The orderline to split.</param>
        /// <param name="data">Details about how to split this orderlin.</param>
        /// <response code="201"></response>
        [HttpPost]
        [Route("/api/v2/actions/orderLines/split/{orderLineId}")]
        public virtual IActionResult OrderLinesSplit([FromRoute][Required]string orderLineId, [FromBody]Models.OrderLineSplit data)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                var orderLineSplitResult = Logic.Order.Split(session, orderLineId, data);
                //201 - Created
                return new CreatedResult("/api/v2/actions/orderLines/split/" + orderLineId, orderLineSplitResult);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Modifies (&#x3D; replaces) an uncommited orderline. IMPORTANT: Any existing modifiers of the orderline are replaced with the new modifiers.  For example if an orderline \&quot;1x Coke with ice\&quot; is modified to be \&quot;1x Coke with Citron\&quot; the ice is also gone so the operation works as a replacement and not as a patch of the existing data.
        /// </summary>
        /// <remarks>IMPORTANT:  - If the order line to be voided is a combo member it's combo parameters are untouched.   This is the reason why they are not part of this transaction.</remarks>
        /// <param name="orderLineId">The orderline to be modified / replaced</param>
        /// <param name="data">The new data to be used for this order line (same as for the add request)</param>
        /// <response code="201">OrderLineResult informing about the new ids / prices of the modified orderline.</response>
        [HttpPost]
        [Route("/api/v2/actions/orderLines/void/{orderLineId}")]
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
