using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
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
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
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
                printers.Add(printer.Id, printer);
            }

            return printers;
        }
    }
}