using System.Collections.Generic;
using System.Data;
using System.Text;
using Novum.Data;
using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Waiter : IDbWaiter
    {

        /// <summary>
        /// 
        /// </summary>
        public Waiter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Waiter> GetWaiters()
        {
            var waiters = new Dictionary<string, Novum.Data.Waiter>();
            var sql = new StringBuilder();
            sql.Append(" SELECT PNR, name ");
            sql.Append(" FROM NT.Pers ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var waiter = new Novum.Data.Waiter();
                waiter.Id = DataObject.GetString(dataRow, "PNR");
                waiter.Name = DataObject.GetString(dataRow, "name");
                waiters.Add(waiter.Id, waiter);
            }

            return waiters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waiterId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ValidWaiter(string waiterId, string code)
        {
            var waiters = new Dictionary<string, Novum.Data.Waiter>();
            var sql = new StringBuilder();
            sql.Append(" SELECT PNR, code, name");
            sql.Append(" FROM NT.Pers ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND PNR = ").Append(Interaction.SqlQuote(waiterId));
            sql.Append(" AND code = ").Append(Interaction.SqlQuote(code));
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            if (dataTable.Rows.Count == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void Login(Session session)
        {
            var posId = DB.Api.Pos.GetPosId(session.SerialNumber);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "Kellnerlogin", session.ClientId, posId, session.WaiterId);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "KellnerloginJournal", Data.ClientId, posId, session.WaiterId, session.SerialNumber, "1");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void Logout(Session session)
        {
            var posId = DB.Api.Pos.GetPosId(session.SerialNumber);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "Kellnerlogout", session.ClientId, posId, session.WaiterId);
        }
    }
}
