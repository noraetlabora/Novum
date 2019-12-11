
namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Api : IDbApi
    {
        private static Misc misc;
        private static Table table;
        private static Waiter waiter;
        private static Menu menu;
        private static Article article;
        private static Modifier modifier;
        private static Printer printer;
        private static Image image;
        private static Payment payment;
        private static Fiscal fiscal;
        private static Order order;
        private static Pos pos;
        private static Hotel hotel;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static string ClientId { get; set; }

        public Api()
        {
            misc = new Misc();
            table = new Table();
            waiter = new Waiter();
            menu = new Menu();
            article = new Article();
            modifier = new Modifier();
            printer = new Printer();
            image = new Image();
            payment = new Payment();
            fiscal = new Fiscal();
            order = new Order();
            pos = new Pos();
            hotel = new Hotel();
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

        public IDbImage Image
        {
            get { return image; }
        }

        public IDbPayment Payment
        {
            get { return payment; }
        }

        public IDbFiscal Fiscal
        {
            get { return fiscal; }
        }

        public IDbOrder Order
        {
            get { return order; }
        }

        public IDbPos Pos
        {
            get { return pos; }
        }

        public IDbHotel Hotel
        {
            get { return hotel; }
        }

        public void Initialize()
        {
            Api.ClientId = Interaction.CallClassMethod("cmNT.Kassa", "GetOmanFirma");
            Logging.Log.Database.Info("ClientId = " + Api.ClientId);
        }
    }
}