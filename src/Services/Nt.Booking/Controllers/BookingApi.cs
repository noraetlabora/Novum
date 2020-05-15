/*
 * Nt.Booking
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
using System.Net.Http;
using System.Threading.Tasks;

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
        public static IHttpClientFactory HttpClientFactory { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public BookingApiController(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mediumId">xxxyyy</param>
        /// <param name="metaData"></param>
        /// <response code="200">super</response>
        /// <response code="400">schlecht</response>
        [HttpGet]
        [Route("/api/v1/mediums/info/{mediumId}")]
        public async Task<IActionResult> GetMediumAsync([FromRoute][Required]string mediumId, [FromBody]Models.MetaData metaData)
        {
            try
            {
                var response = await NtBooking.BookingSystem.GetMediumInformation(mediumId, metaData);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, string.Format("Exception at /api/v1/mediums/info/{0}", mediumId));
                Nt.Logging.Log.Server.Error("metaData: " + metaData.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("/api/v1/mediums/info")]
        public async Task<IActionResult> GetMediums([FromBody]Models.MetaData metaData)
        {
            try
            {
                var response = await NtBooking.BookingSystem.GetMediumInformation(metaData);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, "Exception at /api/v1/mediums/info");
                Nt.Logging.Log.Server.Error("metaData: " + metaData.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <param name="mediumId">id of the medium</param>
        /// <param name="debitRequest"></param>
        /// <response code="200">gonz guad</response>
        [HttpPost]
        [Route("/api/v1/mediums/debit/{mediumId}")]
        public async Task<IActionResult> Debit([FromRoute][Required]string mediumId, [FromBody]Models.DebitRequest debitRequest)
        {
            try
            {
                var response = await NtBooking.BookingSystem.Debit(mediumId, debitRequest);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, string.Format("Exception at /api/v1/mediums/debit/{0}", mediumId));
                Nt.Logging.Log.Server.Error("debitRequest: " + debitRequest.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <param name="mediumId">id of the medium</param>
        /// <param name="creditRequest"></param>
        /// <response code="200">gonz guad</response>
        /// 
        [HttpPost]
        [Route("/api/v1/mediums/credit/{mediumId}")]
        public async Task<IActionResult> Credit([FromRoute][Required]string mediumId, [FromBody]Models.CreditRequest creditRequest)
        {
            try
            {
                var response = await NtBooking.BookingSystem.Credit(mediumId, creditRequest);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, string.Format("Exception at /api/v1/mediums/credit/{0}", mediumId));
                Nt.Logging.Log.Server.Error("creditRequest: " + creditRequest.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// credit a new medium where the mediumId is not yet known
        /// </summary>
        /// <param name="creditRequest"></param>
        /// <response code="200">gonz guad</response>
        /// 
        [HttpPost]
        [Route("/api/v1/mediums/credit")]
        public async Task<IActionResult> Credit([FromBody]Models.CreditRequest creditRequest)
        {
            try
            {
                var response = await NtBooking.BookingSystem.Credit(string.Empty, creditRequest);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, string.Format("Exception at /api/v1/mediums/credit"));
                Nt.Logging.Log.Server.Error("creditRequest: " + creditRequest.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/api/v1/mediums/cancelDebit/{mediumId}")]
        public async Task<IActionResult> CancelDebit([FromRoute][Required]string mediumId, [FromBody]Models.CancellationRequest cancellationRequest)
        {
            try
            {
                var response = await NtBooking.BookingSystem.CancelDebit(mediumId, cancellationRequest);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, string.Format("Exception at /api/v1/mediums/cancelDebit/{0}", mediumId));
                Nt.Logging.Log.Server.Error("cancellationRequest: " + cancellationRequest.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get the status of the host / POS. This will be regularly called by clients to detect status changes (like host / POS restarts)
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/api/v1/mediums/cancelCredit/{mediumId}")]
        public async Task<IActionResult> CancelCredit([FromRoute][Required]string mediumId, [FromBody]Models.CancellationRequest cancellationRequest)
        {
            try
            {
                var response = await NtBooking.BookingSystem.CancelCredit(mediumId, cancellationRequest);
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Error(ex, string.Format("Exception at /api/v1/mediums/cancelCredit/{0}", mediumId));
                Nt.Logging.Log.Server.Error("cancellationRequest: " + cancellationRequest.ToJson());
                return GetExceptionResponse(ex);
            }
        }

        #region private methods

        private ObjectResult GetExceptionResponse(Exception ex)
        {
            Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Path + "|");
            //
            var errorResponse = new Models.ErrorResponse();
            errorResponse.Error.Message = ex.Message;
            //500
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }

        #endregion private methods
    }
}