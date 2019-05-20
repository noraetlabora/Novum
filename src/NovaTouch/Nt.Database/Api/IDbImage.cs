using System.Collections.Generic;

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
        Nt.Data.Image GetImage(Nt.Data.Session session, string imageId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string SetImage(Nt.Data.Session session, Nt.Data.Image image);

    }
}