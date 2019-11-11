using Nt.Database;

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
        public static void Print(Nt.Data.Session session)
        {
            var printJobId = DB.Api.Printer.GetPrintJobId(session);
            while (!string.IsNullOrEmpty(printJobId))
            {
                //get printing data and post the data to the client api
                var printData = DB.Api.Printer.GetPrintData(session, printJobId);
                Client.ClientApi.Print.PostPrintJob(session, printData);
                //delete current print job id and get next one
                DB.Api.Printer.DeletePrintJobId(session, printJobId);
                printJobId = DB.Api.Printer.GetPrintJobId(session);
            }
        }

    }
}