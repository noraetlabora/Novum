using Novum.Database.API;

namespace Novum.Database.Cache.API
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheApi : IDbApi
    {
        private static Cache.API.Misc misc;
        private static Cache.API.Tables tables;
        private static Cache.API.User user;
        public CacheApi()
        {
            if (misc == null)
                misc = new Cache.API.Misc();
            if (tables == null)
                tables = new Cache.API.Tables();
            if (user == null)
                user = new Cache.API.User();
        }

        public IDbMisc Misc
        {
            get { return misc; }
        }
        public IDbTables Tables
        {
            get { return tables; }
        }

        public IDbUser User
        {
            get { return user; }
        }
    }
}