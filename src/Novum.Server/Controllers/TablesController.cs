using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Novum.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v3/[controller]")]
    [ApiController]
    public class TablesController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<Models.Table> Tables;

        /// <summary>
        /// GET api/v1/tables
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "1001", "1001.1", "1002" };
        }

        /// <summary>
        /// GET api/v1/tables/1001.1/
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Tisch = " + id.ToString();
        }

        /// <summary>
        /// POST api/v1/tables
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// // PUT api/v1/tables/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/v1/tables/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
