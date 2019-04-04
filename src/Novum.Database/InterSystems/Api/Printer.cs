using System.Data;
using Novum.Database.Api;
using System.Collections.Generic;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Printer : IDbPrinter
    {
        public Printer()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Printer> GetInvoicePrinters()
        {
            var printers = new Dictionary<string, Novum.Data.Printer>();
            var sql = string.Format("SELECT DEV, beschreibung, devtype, device FROM NT.Device WHERE FA = {0} AND DEV LIKE 'RD%' AND pas = 0", Data.Department);
            var dataTable = Interaction.GetDataTable(sql);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var printer = new Novum.Data.Printer();
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