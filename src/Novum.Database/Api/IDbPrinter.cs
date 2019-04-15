using System.Collections.Generic;

namespace Novum.Database.Api
{
    /// <summary>
    /// Interface for Printer Api
    /// </summary>
    public interface IDbPrinter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Novum.Data.Printer> GetInvoicePrinters();
    }
}