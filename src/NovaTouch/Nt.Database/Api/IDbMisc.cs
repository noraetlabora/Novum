using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, Nt.Data.CancellationResason>> GetCancellationReason();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.ServiceArea>> GetServiceAreas();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.ArticleGroup>> GetArticleGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.TaxGroup>> GetTaxGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, Nt.Data.Course>> GetCourses();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<bool> StaticDataChanged(string guid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        Task ConfirmChangedStaticData(string guid, string serviceName);
    }
}
