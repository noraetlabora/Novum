using System;
using System.Data;
using System.Reflection;
using Novum.Database.API;
using Novum.Data;
using InterSystems.Data.CacheClient;
using System.Collections.Generic;

namespace Novum.Database.Cache.API
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
        public Dictionary<string, Novum.Data.Printer> GetInvoicePrinters(string department)
        {
            var printers = new Dictionary<string, Novum.Data.Printer>();
            try
            {
                System.Threading.Monitor.Enter(DB.CacheConnection);

                var sql = string.Format("SELECT DEV, beschreibung, devtype, device FROM NT.Device WHERE FA = {0} AND DEV LIKE 'RD%' AND pas = 0", department);
                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": SQL = " + sql);

                var dataAdapter = new CacheDataAdapter(sql, DB.CacheConnection);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var printer = new Novum.Data.Printer();
                    printer.Id = DataObject.GetString(dataRow, "DEV");
                    printer.Name = DataObject.GetString(dataRow, "beschreibung");
                    printer.Type = DataObject.GetString(dataRow, "devtype");
                    printer.Device = DataObject.GetString(dataRow, "device");
                    printers.Add(printer.Id, printer);
                }

                Log.Database.Debug(MethodBase.GetCurrentMethod().Name + ": TableRowCount = " + dataTable.Rows.Count);
            }
            catch (Exception ex)
            {
                Log.Database.Error(ex.Message);
                Log.Database.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                System.Threading.Monitor.Exit(DB.CacheConnection);
            }

            return printers;
        }
    }
}