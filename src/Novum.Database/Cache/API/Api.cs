using Novum.Database.Api;

namespace Novum.Database.Cache.API
{
    /// <summary>
    /// 
    /// </summary>
    internal class CacheApi : IDbApi
    {
        private static Cache.API.Misc misc;
        private static Cache.API.Table table;
        private static Cache.API.Waiter waiter;
        private static Cache.API.Menu menu;
        private static Cache.API.Article article;
        private static Cache.API.Modifier modifier;
        private static Cache.API.Printer printer;
        private static Cache.API.Payment payment;

        public CacheApi()
        {
            if (misc == null)
                misc = new Cache.API.Misc();
            if (table == null)
                table = new Cache.API.Table();
            if (waiter == null)
                waiter = new Cache.API.Waiter();
            if (menu == null)
                menu = new Cache.API.Menu();
            if (article == null)
                article = new Cache.API.Article();
            if (modifier == null)
                modifier = new Cache.API.Modifier();
            if (printer == null)
                printer = new Cache.API.Printer();
            if (payment == null)
                payment = new Cache.API.Payment();
        }

        public IDbMisc Misc
        {
            get { return misc; }
        }
        public IDbTable Table
        {
            get { return table; }
        }
        public IDbWaiter Waiter
        {
            get { return waiter; }
        }
        public IDbMenu Menu
        {
            get { return menu; }
        }
        public IDbArticle Article
        {
            get { return article; }
        }
        public IDbModifier Modifier
        {
            get { return modifier; }
        }
        public IDbPrinter Printer
        {
            get { return printer; }
        }
        public IDbPayment Payment
        {
            get { return payment; }
        }
    }
}