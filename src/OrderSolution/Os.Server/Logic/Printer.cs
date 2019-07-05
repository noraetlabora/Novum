using System;
using System.Collections.Generic;
using System.Net.Http;
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
            while(!string.IsNullOrEmpty(printJobId))
            {
                var printData = DB.Api.Printer.GetPrintData(session, printJobId);
                
                DB.Api.Printer.DeletePrintJobId(session, printJobId);
                printJobId = DB.Api.Printer.GetPrintJobId(session);
                ClientApi.Print.PostPrintJob(session.Printer, printData);
            }
        }

    }
}