/*
 * NT.Access
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nt.Access.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class MediumApiController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>

        /// <param name="mediumId">xxxyyy</param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("/api/v1/mediums/{mediumId}")]
        public virtual IActionResult CancellationReasonsGet([FromRoute][Required]string mediumId)
        {
            return new ObjectResult(null);
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>

        /// <param name="mediumId">xxxyyy</param>
        /// <param name="data"></param>
        /// <response code="200"></response>
        public virtual IActionResult MediumCancellation([FromRoute][Required]string mediumId, [FromBody]Models.Cancellation data)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>

        /// <param name="mediumId">id of the medium</param>
        /// <param name="data"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/api/v1/mediums/{mediumId}/payment")]
        public virtual IActionResult MediumPayment([FromRoute][Required]string mediumId, [FromBody]Models.Payment data)
        {
            throw new System.NotImplementedException();
        }
    }
}
