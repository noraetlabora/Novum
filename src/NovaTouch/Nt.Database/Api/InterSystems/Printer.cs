using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Printer : IDbPrinter
    {
        public Printer() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Printer> GetInvoicePrinters()
        {
            var printers = new Dictionary<string, Nt.Data.Printer>();
            var sql = new StringBuilder();
            sql.Append(" SELECT DEV, beschreibung, devtype, device ");
            sql.Append(" FROM NT.Device ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND DEV LIKE 'RD%' ");
            sql.Append(" AND pas = 0");
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var printer = new Nt.Data.Printer();
                printer.Id = DataObject.GetString(dataRow, "DEV");
                printer.Name = DataObject.GetString(dataRow, "beschreibung");
                printer.Type = DataObject.GetString(dataRow, "devtype");
                printer.Device = DataObject.GetString(dataRow, "device");

                if(!printers.ContainsKey(printer.Id))
                    printers.Add(printer.Id, printer);
            }

            return printers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string GetPrintJobId(Nt.Data.Session session) 
        {
            return Interaction.CallClassMethod("cmNT.OmPrint", "GetNextAuftrag", session.ClientId, session.SerialNumber);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="printJobId"></param>
        /// <returns></returns>
        public List<string> GetPrintData(Nt.Data.Session session, string printJobId)
        {
            var printData = Interaction.CallClassMethod("cmNT.OmPrint", "GetAuftrag", session.ClientId, session.SerialNumber, printJobId);
            var printDataString = new DataString(printData);
            return new List<string>(printDataString.SplitByCRLF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="printJobId"></param>
        public void DeletePrintJobId(Nt.Data.Session session, string printJobId)
        {
            Interaction.CallVoidClassMethod("cmNT.OmPrint", "SetAuftragFertig", session.ClientId, session.SerialNumber, printJobId);
        }
    }
}