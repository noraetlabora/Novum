using Nt.Data;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Database.Api.Intersystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Misc : IDbMisc
    {
        internal static Dictionary<string, TaxGroup> cachedTaxGroups;
        internal static Dictionary<string, ArticleGroup> cachedArticleGroups;

        public Misc() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, ArticleGroup>> GetArticleGroups()
        {
            var articleGroups = new Dictionary<string, Nt.Data.ArticleGroup>();
            var sql = new StringBuilder();
            sql.Append(" SELECT AGR, bez, STGR  ");
            sql.Append(" FROM WW.AGR ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var articleGroup = new Nt.Data.ArticleGroup();
                articleGroup.Id = DataObject.GetString(dataRow, "AGR");
                articleGroup.Name = DataObject.GetString(dataRow, "bez");
                articleGroup.TaxGroupId = DataObject.GetString(dataRow, "STGR");

                if (!articleGroups.ContainsKey(articleGroup.Id))
                    articleGroups.Add(articleGroup.Id, articleGroup);
            }

            cachedArticleGroups = articleGroups;
            return articleGroups;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.CancellationResason>> GetCancellationReason()
        {
            var cancellationReasons = new Dictionary<string, Nt.Data.CancellationResason>();
            var sql = new StringBuilder();
            sql.Append(" SELECT GRUND, bez ");
            sql.Append(" FROM NT.StornoGrund ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

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

        public async Task<Dictionary<string, Course>> GetCourses()
        {
            var courses = new Dictionary<string, Nt.Data.Course>();
            var sql = new StringBuilder();
            sql.Append(" SELECT GANGMENU, GANG, bez ");
            sql.Append(" FROM WW.Speisenfolge ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND  passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var course = new Nt.Data.Course();
                course.Menu = DataObject.GetInt(dataRow, "GANGMENU");
                course.Number = DataObject.GetInt(dataRow, "GANG");
                course.Name = DataObject.GetString(dataRow, "bez");
                if (!courses.ContainsKey(course.Id))
                    courses.Add(course.Id, course);
            }

            return courses;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.ServiceArea>> GetServiceAreas()
        {
            var serviceAreas = new Dictionary<string, Nt.Data.ServiceArea>();
            var sql = new StringBuilder();
            sql.Append(" SELECT VKO, bez, vkebene ");
            sql.Append(" FROM WW.VKO ");
            sql.Append(" WHERE FA = ").Append(Api.ClientId);
            sql.Append(" AND  passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, TaxGroup>> GetTaxGroups()
        {
            var taxGroups = new Dictionary<string, Nt.Data.TaxGroup>();
            var sql = new StringBuilder();
            sql.Append(" SELECT A.STGR, A.bez, B.uproz, B.uproz2  ");
            sql.Append(" FROM WW.STGR A ");
            sql.Append(" INNER JOIN WW.STGRSteuer B ON (B.FA = A.FA AND B.STGR = A.STGR AND B.GILT <= ").Append(Intersystems.SqlToday).Append(")");
            sql.Append(" WHERE A.FA = ").Append(Api.ClientId);
            sql.Append(" AND A.passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.GetDataTable(sql.ToString()).ConfigureAwait(false);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var taxGroup = new Nt.Data.TaxGroup();
                taxGroup.Id = DataObject.GetString(dataRow, "STGR");
                taxGroup.Name = DataObject.GetString(dataRow, "bez");
                taxGroup.TaxRate = DataObject.GetUInt(dataRow, "uproz");
                taxGroup.TaxRate2 = DataObject.GetUInt(dataRow, "uproz2");

                if (!taxGroups.ContainsKey(taxGroup.Id))
                    taxGroups.Add(taxGroup.Id, taxGroup);
            }

            cachedTaxGroups = taxGroups;
            return taxGroups;
        }

        #region StaticData    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<bool> StaticDataChanged(string guid)
        {
            var args = new object[2] { Api.ClientId, guid };
            var lastSnapshotTime = await Intersystems.CallClassMethod("cmNT.Kasse", "GetOrdermanSnapshot", args).ConfigureAwait(false);
            System.Diagnostics.Debug.WriteLine("StaticDataChanged: " + lastSnapshotTime);
            if (string.IsNullOrEmpty(lastSnapshotTime))
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public Task ConfirmChangedStaticData(string guid, string serviceName)
        {
            var args = new object[3] { Api.ClientId, guid , serviceName};
            return Intersystems.CallClassMethod("cmNT.Kasse", "SetOrdermanSnapshot", args);
        }

        #endregion
    }
}
