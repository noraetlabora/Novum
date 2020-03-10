using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
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
        public string GetClientId()
        {
            return Api.ClientId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPosId()
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT Kassa ");
            sql.Append(" FROM NT.OMDevConfig ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            if (dataTable.Rows.Count == 0)
                return "";

            DataRow dataRow = dataTable.Rows[0];
            return DataObject.GetString(dataRow, "Kassa");
        }

        public Task<string> GetPosId(string deviceId)
        {
            var args = new object[2] { Api.ClientId, deviceId };
            return Intersystems.CallClassMethod("cmNT.Kassa", "GetOmanKassa", args);
        }

        public Task<string> GetServiceAreaId(string posId)
        {
            var args = new object[2] { Api.ClientId, posId };
            return Intersystems.CallClassMethod("cmNT.Kassa", "GetVerkaufsort", args);
        }

        public Task<string> GetServiceAreaName(string sercieAreaId)
        {
            var args = new object[2] { Api.ClientId, sercieAreaId };
            return Intersystems.CallClassMethod("cmWW.VKO", "GetVKOBez", args);
        }

        public Task<string> GetPriceLevel(string sercieAreaId)
        {
            var args = new object[2] { Api.ClientId, sercieAreaId };
            return Intersystems.CallClassMethod("cmWW.VKO", "GetVKPEbene", args);
        }

        public async Task<Data.Pos> GetPos(string posId)
        {
            var pos = new Data.Pos();

            var sql = new StringBuilder();
            sql.Append(" SELECT KASSA, Vko, bez ");
            sql.Append(" FROM NT.KassenStamm ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND   KASSA = ").Append(Intersystems.SqlQuote(posId));
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            if (dataTable.Rows.Count == 0)
                return null;

            DataRow dataRow = dataTable.Rows[0];
            pos.Id = DataObject.GetString(dataRow, "KASSA");
            pos.Name = DataObject.GetString(dataRow, "bez");
            pos.ServiceAreaId = DataObject.GetString(dataRow, "VKO");

            return pos;
        }

        public async Task<List<string>> GetAlternativePosIds(string posId)
        {
            var posIds = new List<string>();
            var args = new object[2] { Api.ClientId, posId };
            var dbString = await Intersystems.CallClassMethod("cmNT.Kassa", "GetUmleitungsKassen", args).ConfigureAwait(false);
            var posDataString = new DataString(dbString);
            var posArray = posDataString.SplitByChar96();

            foreach (var singlePosString in posArray)
            {
                var singlePosDataString = new DataString(singlePosString);
                var singlePosArray = singlePosDataString.SplitByDoublePipes();
                posIds.Add(singlePosArray[0]);
            }

            return posIds;
        }

    }
}