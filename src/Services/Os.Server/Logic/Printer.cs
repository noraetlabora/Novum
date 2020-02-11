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
        public static List<Models.Printer> GetPrinters()
        {
            //Todo delete Thread diagnostics
            int workerThreads = 0;
            int completionPortThreads = 0;
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            System.Diagnostics.Debug.WriteLine(string.Format("Thread: {0} ({1}/{2})  -  Logic.Data.GetPrinters ", threadId, workerThreads, completionPortThreads));

            var osPrinters = new List<Models.Printer>();
            var ntPrinters = Task.Run(async () => await Nt.Database.DB.Api.Printer.GetInvoicePrinters()).Result;

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
        public static void Print(Nt.Data.Session session)
        {
            var printJobId = Task.Run(async () => await DB.Api.Printer.GetPrintJobId(session)).Result;
            while (!string.IsNullOrEmpty(printJobId))
            {
                //get printing data and post the data to the client api
                var printData = Task.Run(async () => await DB.Api.Printer.GetPrintData(session, printJobId)).Result;
                Client.ClientApi.Print.PostPrintJob(session, printData);
                //delete current print job id and get next one
                Task.Run(async () => await DB.Api.Printer.DeletePrintJobId(session, printJobId));
                printJobId = Task.Run(async () => await DB.Api.Printer.GetPrintJobId(session)).Result;
            }
        }

    }
}