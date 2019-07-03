using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Os.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OsImagesController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/images/fax/{id}")]
        public virtual IActionResult GetFaxImage([FromRoute][Required] string id)
        {
            try 
            {
                var session = Sessions.GetSession(Request);
                //200 - Ok
                return new FileStreamResult(Logic.Image.GetImageStream(id), "image/png");
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Replace an existing fax image on the server. NOTE: The data will be read from the body as raw binary bytes.
        /// </summary>

        /// <param name="id">The ID of the fax image to overwrite.</param>
        /// <response code="204"></response>
        [HttpPut]
        [Route("/api/v2/images/fax/{id}")]
        public virtual IActionResult PutFaxImage([FromRoute][Required] string id)
        {
            try
            {
                var session = Sessions.GetSession(Request);
                Logic.Image.SaveImage(id, Request.Body);
                //204 - No Content
                return new NoContentResult();
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Store a new fax image on the server. NOTE: The data will be read from the body as raw binary bytes.
        /// </summary>

        /// <param name="subTableId">Context information about the sub table to which the fax image is planned to be sent for. When later reffering to the fax image id we will always do so in the context of that sub table. This also means in case the sub table is not valid anymore (e.g. closed via payment) the OrderSolution also does not have a need for that image anymore which means the image can safely be deleted (from the OrderSolution perspective).</param>
        /// <response code="201">Reports the location of the newly created resource in the http header</response>
        /// <response code="400"></response>
        /// <response code="403"></response>
        [HttpPost]
        [Route("/api/v2/images/fax")]
        public IActionResult PostFaxImage([FromQuery] string subTableId)
        {
            try 
            {
                var session = Sessions.GetSession(Request);
                var imageId = Logic.Image.SaveNewImage(session, Request.Body);
                //201 - Created
                return new CreatedResult("/api/v2/images/fax/" + imageId , null);
            }
            catch (Exception ex) 
            {
                Nt.Logging.Log.Server.Error(ex, HttpContext.Request.Method);
                //500 - Internal Server Error
                return new StatusCodeResult(500);
            }
        }
    }
}
