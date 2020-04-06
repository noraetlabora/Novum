using NT.Fiscal.Systems.Countries;
using NT.Fiscal.Systems.Enums;
using NT.Fiscal.Models;

namespace NT.Fiscal.Systems.Providers.Efsta
{
    internal class FiscalProviderEfsta : FiscalProvider
    {
        #region Constructor

        public FiscalProviderEfsta(FiscalCountry country) : base(country)
        {
            base.Provider = Provider.Efsta;
        }

        #endregion Constructor

        #region Send receipt

        public override SendReceiptResponse SendReceiptProvider(SendReceiptRequest request)
        {
            SendReceiptResponse response = new SendReceiptResponse();
            response.Id = "1";
            response.DisplayMessage = "This was processed by " + this.Provider.ToString("g") + " in " + this._country.Country.ToString("g");
            return response;
        }

        #endregion Send receipt

        #region Transaction handling

        public override void RollbackProvider()
        {
            throw new System.NotImplementedException();
        }

        #endregion Transaction handling

        #region Export

        public override void ExportData()
        {
            throw new System.NotImplementedException();
        }

        #endregion Export
    }
}