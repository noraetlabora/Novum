using Novum.Database.API;

namespace Novum.Database.Cache.API
{
    /// <summary>
    /// 
    /// </summary>
    internal class CacheApi : IDbApi
    {
        private static Cache.API.Misc misc;
        private static Cache.API.Table table;
        private static Cache.API.User user;
        private static Cache.API.Menu menu;
        private static Cache.API.Printer printer;
        private static Cache.API.Payment payment;
        private static Cache.API.Modifier modifier;

        public CacheApi()
        {
            if (misc == null)
                misc = new Cache.API.Misc();
            if (table == null)
                table = new Cache.API.Table();
            if (user == null)
                user = new Cache.API.User();
            if (menu == null)
                menu = new Cache.API.Menu();
            if (printer == null)
                printer = new Cache.API.Printer();
            if (payment == null)
                payment = new Cache.API.Payment();
            if (modifier == null)
                modifier = new Cache.API.Modifier();
        }

        public IDbMisc Misc
        {
            get { return misc; }
        }
        public IDbTable Table
        {
            get { return table; }
        }
        public IDbUser User
        {
            get { return user; }
        }
        public IDbMenu Menu
        {
            get { return menu; }
        }
        public IDbPrinter Printer
        {
            get { return printer; }
        }
        public IDbPayment Payment
        {
            get { return payment; }
        }
        public IDbModifier Modifier
        {
            get { return modifier; }
        }
    }
}