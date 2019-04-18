using System.Collections.Generic;

namespace Nt.Database.Api
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
        Dictionary<string, Nt.Data.Printer> GetInvoicePrinters();
    }
}