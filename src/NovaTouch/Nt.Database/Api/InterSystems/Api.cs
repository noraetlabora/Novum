namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Api : IDbApi
    {
        private static Misc _misc;
        private static Table _table;
        private static Waiter _waiter;
        private static Menu _menu;
        private static Article _article;
        private static Modifier _modifier;
        private static Printer _printer;
        private static Image _image;
        private static Payment _payment;
        private static Fiscal _fiscal;
        private static Order _order;
        private static Pos _pos;
        private static Hotel _hotel;
        private static string _clientId;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static string ClientId
        {
            get
            {
                return _clientId;
            }
        }

        public Api()
        {
            _misc = new Misc();
            _table = new Table();
            _waiter = new Waiter();
            _menu = new Menu();
            _article = new Article();
            _modifier = new Modifier();
            _printer = new Printer();
            _image = new Image();
            _payment = new Payment();
            _fiscal = new Fiscal();
            _order = new Order();
            _pos = new Pos();
            _hotel = new Hotel();
            _clientId = "";
        }

        public IDbMisc Misc
        {
            get { return _misc; }
        }
        public IDbTable Table
        {
            get { return _table; }
        }
        public IDbWaiter Waiter
        {
            get { return _waiter; }
        }

        public IDbMenu Menu
        {
            get { return _menu; }
        }

        public IDbArticle Article
        {
            get { return _article; }
        }

        public IDbModifier Modifier
        {
            get { return _modifier; }
        }

        public IDbPrinter Printer
        {
            get { return _printer; }
        }

        public IDbImage Image
        {
            get { return _image; }
        }

        public IDbPayment Payment
        {
            get { return _payment; }
        }

        public IDbFiscal Fiscal
        {
            get { return _fiscal; }
        }

        public IDbOrder Order
        {
            get { return _order; }
        }

        public IDbPos Pos
        {
            get { return _pos; }
        }

        public IDbHotel Hotel
        {
            get { return _hotel; }
        }

        public void Initialize()
        {
            _clientId = Intersystems.Instance.CallClassMethod("cmNT.Kassa", "GetOmanFirma").GetAwaiter().GetResult();
            Logging.Log.Database.Info("ClientId = " + _clientId);
        }
    }
}