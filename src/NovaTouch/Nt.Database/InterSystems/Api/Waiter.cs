using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Waiter : IDbWaiter
    {

        /// <summary>
        /// 
        /// </summary>
        public Waiter() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Waiter> GetWaiters()
        {
            var waiters = new Dictionary<string, Nt.Data.Waiter>();
            var sql = new StringBuilder();
            sql.Append(" SELECT PNR, name ");
            sql.Append(" FROM NT.Pers ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var waiter = new Nt.Data.Waiter();
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
            var waiters = new Dictionary<string, Nt.Data.Waiter>();
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
        public void Login(Nt.Data.Session session)
        {
            var posId = DB.Api.Pos.GetPosId(session.SerialNumber);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "Kellnerlogin", session.ClientId, posId, session.WaiterId);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "KellnerloginJournal", Data.ClientId, posId, session.WaiterId, session.SerialNumber, "1");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void Logout(Nt.Data.Session session)
        {
            var posId = DB.Api.Pos.GetPosId(session.SerialNumber);
            Interaction.CallVoidClassMethod("cmNT.Kellner", "Kellnerlogout", session.ClientId, posId, session.WaiterId);
        }
    }
}