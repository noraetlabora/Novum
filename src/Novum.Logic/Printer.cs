using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Printer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static Dictionary<string, Novum.Data.Printer> GetInvoicePrinters(string department)
        {
            return Novum.Database.DB.Api.Printer.GetInvoicePrinters(department);
        }
    }
}