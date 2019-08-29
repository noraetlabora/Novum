using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Misc : IDbMisc
    {
        public Misc() { }

        #region CancellationResason

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.CancellationResason> GetCancellationReason()
        {
            var cancellationReasons = new Dictionary<string, Nt.Data.CancellationResason>();
            var sql = new StringBuilder();
            sql.Append(" SELECT GRUND, bez ");
            sql.Append(" FROM NT.StornoGrund ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var cancellationReason = new Nt.Data.CancellationResason();
                cancellationReason.Id = DataObject.GetString(dataRow, "GRUND");
                cancellationReason.Name = DataObject.GetString(dataRow, "bez");

                if (!cancellationReasons.ContainsKey(cancellationReason.Id))
                    cancellationReasons.Add(cancellationReason.Id, cancellationReason);
            }

            return cancellationReasons;
        }

        #endregion

        #region Service Area

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.ServiceArea> GetServiceAreas()
        {
            var serviceAreas = new Dictionary<string, Nt.Data.ServiceArea>();
            var sql = new StringBuilder();
            sql.Append(" SELECT VKO, bez, vkebene ");
            sql.Append(" FROM WW.VKO ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND  passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var serviceArea = new Nt.Data.ServiceArea();
                serviceArea.Id = DataObject.GetString(dataRow, "VKO");
                serviceArea.Name = DataObject.GetString(dataRow, "bez");
                serviceArea.PriceLevel = DataObject.GetString(dataRow, "vkebene");
                serviceAreas.Add(serviceArea.Id, serviceArea);
            }

            return serviceAreas;
        }
        #endregion

        #region Snapshot    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool HasSnapshotTime(string guid) 
        {
            var lastSnapshotTime = Interaction.CallClassMethod("cmNT.Kasse", "GetOrdermanSnapshot", Api.ClientId, guid);
            if (string.IsNullOrEmpty(lastSnapshotTime))
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public void SetSnapshotTime(string guid) 
        {
            Interaction.CallClassMethod("cmNT.Kasse", "SetOrdermanSnapshot", Api.ClientId, guid);
        }

        #endregion
    }
}
