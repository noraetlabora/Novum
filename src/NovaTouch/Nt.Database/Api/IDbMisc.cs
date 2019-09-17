using System.Collections.Generic;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for miscellaneous Api
    /// </summary>
    public interface IDbMisc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.CancellationResason> GetCancellationReason();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.ServiceArea> GetServiceAreas();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.ArticleGroup> GetArticleGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Nt.Data.TaxGroup> GetTaxGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        bool HasSnapshotTime(string guid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        void SetSnapshotTime(string guid);
    }
}
