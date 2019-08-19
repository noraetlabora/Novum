using Nt.Database.Api;
using System.Data;
using System.Text;

namespace Nt.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Pos : IDbPos
    {
        public Pos() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetClientId() {
            return InterSystemsApi.ClientId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPosId()
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT Kassa ");
            sql.Append(" FROM NT.OMDevConfig ");
            sql.Append(" WHERE FA = ").Append(InterSystemsApi.ClientId);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            if (dataTable.Rows.Count == 0)
                return "";

            DataRow dataRow = dataTable.Rows[0];
            return DataObject.GetString(dataRow, "Kassa");
        }

        public string GetPosId(string deviceId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetOmanKassa", InterSystemsApi.ClientId, deviceId);
        }

        public string GetServiceAreaId(string posId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetVerkaufsort", InterSystemsApi.ClientId, posId);
        }

        public string GetServiceAreaName(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKOBez", InterSystemsApi.ClientId, sercieAreaId);
        }

        public string GetPriceLevel(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKPEbene", InterSystemsApi.ClientId, sercieAreaId);
        }

    }
}