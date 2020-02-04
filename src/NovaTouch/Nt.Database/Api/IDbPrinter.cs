using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.Printer>> GetInvoicePrinters();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> GetPrintJobId(Nt.Data.Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetPrintData(Nt.Data.Session session, string printJobId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="printJobId"></param>
        /// <returns></returns>
        Task DeletePrintJobId(Nt.Data.Session session, string printJobId);
    }
}