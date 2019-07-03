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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetPrintJobId(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetPrintData(Nt.Data.Session session, string printJobId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="printJobId"></param>
        /// <returns></returns>
        void DeletePrintJobId(Nt.Data.Session session, string printJobId);
    }
}