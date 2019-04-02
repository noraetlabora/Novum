using System.Collections.Generic;
using System.Data;
using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Waiter : IDbWaiter
    {
        public Waiter()
        {
        }

        public Dictionary<string, Novum.Data.Waiter> GetWaiters(string department)
        {
            var waiters = new Dictionary<string, Novum.Data.Waiter>();
            var sql = string.Format("SELECT PNR, name FROM NT.Pers WHERE FA = {0} AND passiv > '{1}'", department, Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var waiter = new Novum.Data.Waiter();
                waiter.Id = DataObject.GetString(dataRow, "PNR");
                waiter.Name = DataObject.GetString(dataRow, "name");
                waiters.Add(waiter.Id, waiter);
            }

            return waiters;
        }
    }
}