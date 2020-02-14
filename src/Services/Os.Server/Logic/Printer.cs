using Nt.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Os.Server.Logic
{

    /// <summary>
    /// 
    /// </summary>
    public class Printer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.Printer>> GetPrinters()
        {
            var osPrinters = new List<Models.Printer>();

            var ntPrinters = await Nt.Database.DB.Api.Printer.GetInvoicePrinters();

            foreach (var ntPrinter in ntPrinters.Values)
            {
                var osPrinter = new Models.Printer();
                osPrinter.Name = ntPrinter.Name;
                osPrinter.Path = ntPrinter.Id;
                osPrinters.Add(osPrinter);
            }

            return osPrinters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task Print(Nt.Data.Session session)
        {
            var printJobId = await DB.Api.Printer.GetPrintJobId(session);
            while (!string.IsNullOrEmpty(printJobId))
            {
                //get printing data and post the data to the client api
                var printData = await DB.Api.Printer.GetPrintData(session, printJobId);
                Client.ClientApi.Print.PostPrintJob(session, printData);
                //delete current print job id and get next one
                await DB.Api.Printer.DeletePrintJobId(session, printJobId);
                printJobId = await DB.Api.Printer.GetPrintJobId(session);
            }
        }

    }
}