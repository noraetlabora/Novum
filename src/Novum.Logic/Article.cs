using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static Dictionary<string, Novum.Data.Article> GetArticles(string department, string menuId)
        {
            return Novum.Database.DB.Api.Article.GetArticles(department, menuId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static Dictionary<string, Novum.Data.Article> GetArticles(string department)
        {
            return Novum.Database.DB.Api.Article.GetArticles(department);
        }

    }
}