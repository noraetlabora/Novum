using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Dictionary<string, Nt.Data.Printer>> GetInvoicePrinters()
        {
            var printers = new Dictionary<string, Nt.Data.Printer>();
            var sql = new StringBuilder();
            sql.Append(" SELECT DEV, beschreibung, devtype, device ");
            sql.Append(" FROM NT.Device ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND DEV LIKE 'RD%' ");
            sql.Append(" AND pas = 0");
            var dataTable = await Intersystems.Instance.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var printer = new Nt.Data.Printer();
                printer.Id = DataObject.GetString(dataRow, "DEV");
                printer.Name = DataObject.GetString(dataRow, "beschreibung");
                printer.Type = DataObject.GetString(dataRow, "devtype");
                printer.Device = DataObject.GetString(dataRow, "device");

                if (!printers.ContainsKey(printer.Id))
                    printers.Add(printer.Id, printer);
            }

            dataTable.Dispose();
            return printers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<string> GetPrintJobId(Nt.Data.Session session)
        {
            return await Intersystems.Instance.CallClassMethod("cmNT.OmPrint", "GetNextAuftrag", session.ClientId, session.SerialNumber);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="printJobId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPrintData(Nt.Data.Session session, string printJobId)
        {
            var printData = await Intersystems.Instance.CallClassMethod("cmNT.OmPrint", "GetAuftrag", session.ClientId, session.SerialNumber, printJobId);
            var printDataString = new DataString(printData);
            return new List<string>(printDataString.SplitByCRLF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="printJobId"></param>
        public async Task DeletePrintJobId(Nt.Data.Session session, string printJobId)
        {
            await Intersystems.Instance.CallVoidClassMethod("cmNT.OmPrint", "SetAuftragFertig", session.ClientId, session.SerialNumber, printJobId);
        }
    }
}