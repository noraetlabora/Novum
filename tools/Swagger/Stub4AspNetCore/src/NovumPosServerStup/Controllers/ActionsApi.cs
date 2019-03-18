/*
 * OrderSolution HTTP API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.9.6
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using NovumPosServerStup.Attributes;
using NovumPosServerStup.Models;

namespace NovumPosServerStup.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    public class ActionsApiController : Controller
    { 
        /// <summary>
        /// Execute a login transaciton. NOTE: Will set an \&quot;AuthToken\&quot; cookie needed in later authorized requests.
        /// </summary>
        
        /// <param name="data"></param>
        /// <response code="201"></response>
        /// <response code="401"></response>
        [HttpPost]
        [Route("/api/v2/actions/Auth/Login")]
        [ValidateModelState]
        [SwaggerOperation("AuthLogin")]
        [SwaggerResponse(statusCode: 401, type: typeof(OsError), description: "")]
        public virtual IActionResult AuthLogin([FromBody]LoginUser data)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201);

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401, default(OsError));


            throw new NotImplementedException();
        }

        /// <summary>
        /// Logout from the server.
        /// </summary>
        
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/Auth/Logout")]
        [ValidateModelState]
        [SwaggerOperation("AuthLogout")]
        public virtual IActionResult AuthLogout()
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);


            throw new NotImplementedException();
        }

        /// <summary>
        /// Get initialization data for the client. Clients will have to call transactions/login to execute meaningful transactions.
        /// </summary>
        
        /// <param name="clientData"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="412"></response>
        [HttpPost]
        [Route("/api/v2/actions/Init/RegisterClient")]
        [ValidateModelState]
        [SwaggerOperation("InitRegisterClient")]
        [SwaggerResponse(statusCode: 200, type: typeof(POSInfo), description: "")]
        [SwaggerResponse(statusCode: 400, type: typeof(OsError), description: "")]
        [SwaggerResponse(statusCode: 412, type: typeof(OsError), description: "")]
        public virtual IActionResult InitRegisterClient([FromBody]ClientInfo clientData)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(POSInfo));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(OsError));

            //TODO: Uncomment the next line to return response 412 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(412, default(OsError));

            string exampleJson = null;
            exampleJson = "{\r\n  \"restaurantName\" : \"restaurantName\",\r\n  \"clientName\" : \"clientName\",\r\n  \"utcTime\" : 0\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<POSInfo>(exampleJson)
            : default(POSInfo);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Informs about a gateway (like OsServer) that is configured to work with this POS. A gateway is a system / server that clients use to communicate with the server.
        /// </summary>
        
        /// <param name="gatewayInfo">Information about the gateway.</param>
        /// <response code="204"></response>
        [HttpPost]
        [Route("/api/v2/actions/Init/RegisterGateway")]
        [ValidateModelState]
        [SwaggerOperation("InitRegisterGateway")]
        public virtual IActionResult InitRegisterGateway([FromBody]GatewayInfo gatewayInfo)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);


            throw new NotImplementedException();
        }
    }
}
