using System.Collections.Generic;
using System.Data;
using Novum.Data;
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

        public Dictionary<string, Novum.Data.Waiter> GetWaiters()
        {
            var waiters = new Dictionary<string, Novum.Data.Waiter>();
            var sql = string.Format("SELECT PNR, name FROM NT.Pers WHERE FA = {0} AND passiv > '{1}'", Data.Department, Interaction.SqlToday);
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

        public bool ValidWaiter(Session session, string code)
        {
            var waiters = new Dictionary<string, Novum.Data.Waiter>();
            var sql = string.Format("SELECT PNR, code, name FROM NT.Pers WHERE FA = {0} AND PNR = '{1}' AND code = '{2}' AND passiv > '{3}'", session.Department, session.WaiterId, code, Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql);

            if (dataTable.Rows.Count == 1)
                return true;
            else
                return false;
        }

        public void Login(Session session)
        {
            var posId = DB.Api.Pos.GetPosId(session.SerialNumber);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "Kellnerlogin", session.Department, posId, session.WaiterId);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "KellnerloginJournal", Data.Department, posId, session.WaiterId, session.SerialNumber, "1");
        }

        public void Logout(Session session)
        {
            var posId = DB.Api.Pos.GetPosId(session.SerialNumber);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "Kellnerlogout", session.Department, posId, session.WaiterId);
        }
    }
}