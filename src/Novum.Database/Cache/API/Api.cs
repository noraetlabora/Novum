using Novum.Database.API;

namespace Novum.Database.Cache.API
{
    /// <summary>
    /// 
    /// </summary>
    internal class CacheApi : IDbApi
    {
        private static Cache.API.Misc misc;
        private static Cache.API.Table tables;
        private static Cache.API.User user;
        private static Cache.API.Menu menu;

        public CacheApi()
        {
            if (misc == null)
                misc = new Cache.API.Misc();
            if (tables == null)
                tables = new Cache.API.Table();
            if (user == null)
                user = new Cache.API.User();
            if (menu == null)
                menu = new Cache.API.Menu();
        }

        public IDbMisc Misc
        {
            get { return misc; }
        }
        public IDbTable Tables
        {
            get { return tables; }
        }

        public IDbUser User
        {
            get { return user; }
        }

        public IDbMenu Menu
        {
            get { return menu; }
        }
    }
}