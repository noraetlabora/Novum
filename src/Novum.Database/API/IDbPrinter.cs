using System.Collections.Generic;

namespace Novum.Database.API
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbPrinter
    {
        List<Novum.Data.Printer> GetInvoicePrinters(string department);
    }
}