using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Novum.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Table
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value>"1001.1"</value>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value>"1.1"</value>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value>123.45</value>
        public decimal Sum { get; set; }

    }
}