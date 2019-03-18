using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Server.Models.Os;

namespace Novum.Server.Controllers.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsActionsPayController : Controller
    {
        /// <summary>
        /// Pay sub full subtables. If the pay action is executed successfully the subtables will be fully paid and clsoed.
        /// </summary>
        /// <param name="data">Pay data describing which payments have to be executed for which subtables</param>
        /// <response code="204">OK in case the payment was successfull and the receipt is moved to the printer queue.</response>
        [HttpPost]
        [Route("/api/v2/actions/Pay/SubTables")]
        public virtual IActionResult PaySubTables([FromBody][Required]PaySubTables data)
        {

            // 204 - No Content 
            return new NoContentResult();
        }

        /// <summary>
        /// Pay individual orderlines. Typically used when the bill is splitted.
        /// </summary>
        /// <param name="data">The orderlines to be paid + payment info.</param>
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/Pay/OrderLines")]
        public IActionResult PayOrderLines([FromBody]PayOrderLines data)
        {

            // 204 - No Content 
            return new NoContentResult();
        }
    }
}