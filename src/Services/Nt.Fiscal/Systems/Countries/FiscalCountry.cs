using Nt.Fiscal.Systems.Enums;

namespace Nt.Fiscal.Systems.Countries
{
    public abstract class FiscalCountry
    {
        public Country Country;

        #region Check provider

        public void CheckProvider()
        {
        }

        #endregion Check provider

        #region Check receipt

        public void CheckReceipt()
        {
        }

        #endregion Check receipt

        #region Get country specific types

        public void GetPaymentType()
        {
        }

        public void GetTaxType()
        {
        }

        #endregion Get country specific types

        #region Receipt sending post methods

        public void AfterSendingReceipt()
        {
        }

        #endregion Receipt sending post methods
    }
}