using System.Collections.Generic;
using System.Data;
using System.Text;
using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
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
            var cReasons = new Dictionary<string, Nt.Data.CancellationResason>();
            var sql = new StringBuilder();
            sql.Append(" SELECT GRUND, bez ");
            sql.Append(" FROM NT.StornoGrund ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var cReason = new Nt.Data.CancellationResason();
                cReason.Id = DataObject.GetString(dataRow, "GRUND");
                cReason.Name = DataObject.GetString(dataRow, "bez");
                cReasons.Add(cReason.Id, cReason);
            }

            return cReasons;
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
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
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
    }
}