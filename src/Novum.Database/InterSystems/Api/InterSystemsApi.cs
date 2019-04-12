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
        private static InterSystems.Api.Pos pos;

        public InterSystemsApi()
        {
            misc = new InterSystems.Api.Misc();
            table = new InterSystems.Api.Table();
            waiter = new InterSystems.Api.Waiter();
            menu = new InterSystems.Api.Menu();
            article = new InterSystems.Api.Article();
            modifier = new InterSystems.Api.Modifier();
            printer = new InterSystems.Api.Printer();
            payment = new InterSystems.Api.Payment();
            order = new InterSystems.Api.Order();
            pos = new InterSystems.Api.Pos();
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

        public IDbPos Pos
        {
            get { return pos; }
        }

        public void Initialize()
        {
            Data.ClientId = Interaction.CallClassMethod("cmNT.Kassa", "GetOmanFirma");
            Log.Database.Info("Department = " + Data.ClientId);
        }
    }
}