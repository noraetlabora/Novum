using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class InterSystemsApi : IDbApi
    {
        private static InterSystems.Api.Misc misc;
        private static InterSystems.Api.Table table;
        private static InterSystems.Api.Waiter waiter;
        private static InterSystems.Api.Menu menu;
        private static InterSystems.Api.Article article;
        private static InterSystems.Api.Modifier modifier;
        private static InterSystems.Api.Printer printer;
        private static InterSystems.Api.Payment payment;
        private static InterSystems.Api.Order order;

        public InterSystemsApi()
        {
            if (misc == null)
                misc = new InterSystems.Api.Misc();
            if (table == null)
                table = new InterSystems.Api.Table();
            if (waiter == null)
                waiter = new InterSystems.Api.Waiter();
            if (menu == null)
                menu = new InterSystems.Api.Menu();
            if (article == null)
                article = new InterSystems.Api.Article();
            if (modifier == null)
                modifier = new InterSystems.Api.Modifier();
            if (printer == null)
                printer = new InterSystems.Api.Printer();
            if (payment == null)
                payment = new InterSystems.Api.Payment();
            if (order == null)
                order = new InterSystems.Api.Order();
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

        public IDbOrder Order
        {
            get { return order; }
        }

        public void Initialize()
        {
            Data.Department = Interaction.CallClassMethod("cmNT.Kassa", "GetOmanFirma");
            Log.Database.Info("Department = " + Data.Department);
        }
    }
}