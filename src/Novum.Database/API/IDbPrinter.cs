using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbPrinter
    {
        Dictionary<string, Novum.Data.Printer> GetInvoicePrinters(string department);
    }
}