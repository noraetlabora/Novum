using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nt.Database.Api.InterSystems
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
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var waiter = new Nt.Data.Waiter();
                waiter.Id = DataObject.GetString(dataRow, "PNR");
                waiter.Name = DataObject.GetString(dataRow, "name");

                if (!waiters.ContainsKey(waiter.Id))
                    waiters.Add(waiter.Id, waiter);
            }

            return waiters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetWaiterId(string code)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT PNR, code, name");
            sql.Append(" FROM NT.Pers ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND code = ").Append(Interaction.SqlQuote(code));
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            if (dataTable.Rows.Count == 1)
                return DataObject.GetString(dataTable.Rows[0], "PNR");
            else
                return null;
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
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
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
            Interaction.CallVoidClassMethod("cmNT.Kellner", "KellnerloginJournal", Api.ClientId, posId, session.WaiterId, session.SerialNumber, "1");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waiterId"></param>
        /// <returns></returns>
        public Dictionary<Nt.Data.Permission.PermissionType, Nt.Data.Permission> GetPermissions(string waiterId)
        {
            var permissions = new Dictionary<Nt.Data.Permission.PermissionType, Nt.Data.Permission>();
            var sql = new StringBuilder();
            sql.Append(" SELECT B.PRG, P.bez, B.bere ");
            sql.Append(" FROM NT.PersMitglied M ");
            sql.Append(" JOIN NT.PersStufenBere B ON B.FA=M.FA AND B.STUFE = M.STUFE ");
            sql.Append(" JOIN NT.PersSecProg P ON P.PRG = B.PRG AND P.obsolet = 0 ");
            sql.Append(" WHERE M.FA = ").Append(Api.ClientId);
            sql.Append(" AND M.PNR = ").Append(waiterId);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var name = DataObject.GetString(dataRow, "bez");
                var program = DataObject.GetString(dataRow, "PRG");
                var permitted = DataObject.GetBool(dataRow, "bere");
                Nt.Data.Permission.PermissionType permissionType;

                switch (program)
                {
                    case "SEC-1":
                        permissionType = Nt.Data.Permission.PermissionType.ConfirmOrder;
                        break;
                    case "SEC-2":
                        permissionType = Nt.Data.Permission.PermissionType.OpenTable;
                        break;
                    case "SEC-4":
                        permissionType = Nt.Data.Permission.PermissionType.CancelConfirmedOrder;
                        break;
                    case "SEC-15":
                        permissionType = Nt.Data.Permission.PermissionType.CancelUnconfirmedOrder;
                        break;
                    case "SPLITT":
                        permissionType = Nt.Data.Permission.PermissionType.SplitTable;
                        break;
                    case "SOL-FAX":
                        permissionType = Nt.Data.Permission.PermissionType.ModifyWithFax;
                        break;
                    default:
                        continue;
                }

                var permission = new Nt.Data.Permission(name, program, permissionType, permitted);
                permissions.Add(permission.Type, permission);
            }

            return permissions;
        }
    }
}