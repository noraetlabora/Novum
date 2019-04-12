using System.Data;
using System.Text;
using Novum.Database.Api;
using System.Collections.Generic;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Misc : IDbMisc
    {
        public Misc()
        {
        }

        #region CancellationResason

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.CancellationResason> GetCancellationReason()
        {
            var cReasons = new Dictionary<string, Novum.Data.CancellationResason>();
            var sql = new StringBuilder();
            sql.Append(" SELECT GRUND, bez ");
            sql.Append(" FROM NT.StornoGrund ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var cReason = new Novum.Data.CancellationResason();
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
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.ServiceArea> GetServiceAreas()
        {
            var serviceAreas = new Dictionary<string, Novum.Data.ServiceArea>();
            var sql = new StringBuilder();
            sql.Append(" SELECT VKO, bez, vkebene ");
            sql.Append(" FROM WW.VKO ");
            sql.Append(" WHERE FA = ").Append(Data.ClientId);
            sql.Append(" AND  passiv > ").Append(Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var serviceArea = new Novum.Data.ServiceArea();
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