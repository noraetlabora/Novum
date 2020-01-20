/*
 * NT.Access
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nt.Booking.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId">xxxyyy</param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("/api/v1/mediums/{mediumId}")]
        public virtual IActionResult GetMedium([FromRoute][Required]string mediumId)
        {
            try
            {
                var mediumInformation = NtBooking.BookingSystem.GetMediumInformation(mediumId);
                return new ObjectResult(mediumInformation);
            }
            catch (BookingException ex)
            {
                return GetExceptionResponse(ex);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>

        /// <param name="mediumId">returns a list of mediums / chips / rooms / vouchers</param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("/api/v1/mediums/")]
        public virtual IActionResult GetMediums()
        {
            try
            {
                var mediumInformation = NtBooking.BookingSystem.GetMediumInformation();
                return new ObjectResult(mediumInformation);
            }
            catch (BookingException ex)
            {
                return GetExceptionResponse(ex);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <param name="mediumId">id of the medium</param>
        /// <param name="data"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/api/v1/mediums/{mediumId}/payments")]
        public virtual IActionResult MediumPayment([FromRoute][Required]string mediumId, [FromBody]Models.Payment payment)
        {
            try
            {
                var booking = NtBooking.BookingSystem.Pay();
                return new ObjectResult(booking);
            }
            catch (BookingException ex)
            {
                return GetExceptionResponse(ex);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <param name="mediumId">xxxyyy</param>
        /// <param name="data"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/api/v1/mediums/{mediumId}/cancellations")]
        public virtual IActionResult MediumCancellation([FromRoute][Required]string mediumId, [FromBody]Models.Cancellation cancellation)
        {
            try
            {
                var booking = NtBooking.BookingSystem.Pay();
                return new ObjectResult(booking);
            }
            catch (BookingException ex)
            {
                return GetExceptionResponse(ex);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex);
            }
        }

        #region private methods

        private ObjectResult GetExceptionResponse(BookingException ex)
        {
            Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Path + "|");
            var error = new Models.Error();
            error.Message = ex.Message;
            error.DisplayMessage = ex.DisplayMessage;
            error.BookingSystem = ex.BookingSystem.ToString();
            error.BookingSystemCode = ex.BookingSystemCode;
            error.BookingSystemMessage = ex.BookingSystemMessage;
            
            return StatusCode(ex.HttpStatusCode, error);
        }

        private ObjectResult GetExceptionResponse(Exception ex)
        {
            Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Path + "|");
            //
            var error = new Models.Error();
            error.Message = ex.Message;
            //500
            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }

        #endregion

    }
}
