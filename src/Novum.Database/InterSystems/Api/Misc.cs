using System.Data;
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
        public Dictionary<string, Novum.Data.CancellationResason> GetCancellationReason(string department)
        {
            var cReasons = new Dictionary<string, Novum.Data.CancellationResason>();
            var sql = string.Format("SELECT GRUND, bez FROM NT.StornoGrund WHERE FA = {0} AND passiv > '{1}'", department, Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql);

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
        public Dictionary<string, Novum.Data.ServiceArea> GetServiceAreas(string department)
        {
            var serviceAreas = new Dictionary<string, Novum.Data.ServiceArea>();
            var sql = string.Format("SELECT VKO, bez FROM WW.VKO WHERE FA = {0} AND passiv > '{1}'", department, Interaction.SqlToday);
            var dataTable = Interaction.GetDataTable(sql);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var serviceArea = new Novum.Data.ServiceArea();
                serviceArea.Id = DataObject.GetString(dataRow, "VKO");
                serviceArea.Name = DataObject.GetString(dataRow, "bez");
                serviceAreas.Add(serviceArea.Id, serviceArea);
            }

            return serviceAreas;
        }
        #endregion
    }
}