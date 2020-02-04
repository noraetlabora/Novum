using System.Threading.Tasks;

namespace Nt.Database.Api
{
    /// <summary>
    /// Interface for Menu Api
    /// </summary>
    public interface IDbImage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Nt.Data.Image> GetImage(Nt.Data.Session session, string imageId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> SetImage(Nt.Data.Session session, Nt.Data.Image image);

    }
}