using Nt.Fiscal.Systems.Countries;
using Nt.Fiscal.Systems.Enums;
using Nt.Fiscal.Models;

namespace Nt.Fiscal.Systems.Providers.Efsta
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
            response.Status = "1";
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