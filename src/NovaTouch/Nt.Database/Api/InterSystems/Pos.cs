using Nt.Database.Api;
using System.Data;
using System.Text;

namespace Nt.Database.Api.InterSystems
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
            return Api.ClientId;
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
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            var dataTable = Interaction.GetDataTable(sql.ToString());

            if (dataTable.Rows.Count == 0)
                return "";

            DataRow dataRow = dataTable.Rows[0];
            return DataObject.GetString(dataRow, "Kassa");
        }

        public string GetPosId(string deviceId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetOmanKassa", Api.ClientId, deviceId);
        }

        public string GetServiceAreaId(string posId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetVerkaufsort", Api.ClientId, posId);
        }

        public string GetServiceAreaName(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKOBez", Api.ClientId, sercieAreaId);
        }

        public string GetPriceLevel(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKPEbene", Api.ClientId, sercieAreaId);
        }

    }
}