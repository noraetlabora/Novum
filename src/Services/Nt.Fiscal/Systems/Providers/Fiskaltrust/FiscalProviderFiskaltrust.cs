using Nt.Fiscal.Models;
using Nt.Fiscal.Systems.Countries;
using Nt.Fiscal.Systems.Enums;
using System.Collections.Generic;

namespace Nt.Fiscal.Systems.Providers.Fiskaltrust
{
    internal class FiscalProviderFiskaltrust : FiscalProvider
    {
        public new List<Country> SupportedCountries = new List<Country> { Country.Austria };

        #region Constructor

        public FiscalProviderFiskaltrust(FiscalCountry country) : base(country)
        {
            base.Provider = Provider.Fiskaltrust;
        }

        #endregion Constructor

        #region Check provider

        public override CheckProviderResponse CheckProvider()
        {
            return base.CheckProvider();
        }

        #endregion Check provider

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