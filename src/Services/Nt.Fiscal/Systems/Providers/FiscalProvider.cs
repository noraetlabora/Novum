using Nt.Fiscal.Models;
using Nt.Fiscal.Systems.Countries;
using Nt.Fiscal.Systems.Enums;
using System.Collections.Generic;

namespace Nt.Fiscal.Systems.Providers
{
    public abstract class FiscalProvider
    {
        protected FiscalCountry _country;

        public List<Country> SupportedCountries;

        public Provider Provider;

        protected bool InTransaction = false;

        public enum CommunicationType
        {
            Request,
            Response
        }

        #region Constructor

        public FiscalProvider(FiscalCountry country)
        {
            _country = country;
        }

        #endregion Constructor

        #region Open / close methods

        public virtual void Open()
        {
        }

        public virtual void Close()
        {
        }

        #endregion Open / close methods

        #region Check provider

        public virtual CheckProviderResponse CheckProvider()
        {
            var response = new CheckProviderResponse();
            response.Country = Nt.Fiscal.NtFiscal.FiscalProvider._country.Country.ToString("g");
            response.Provider = Nt.Fiscal.NtFiscal.FiscalProvider.Provider.ToString("g");
            response.InTransaction = Nt.Fiscal.NtFiscal.FiscalProvider.InTransaction;
            response.Status = "0";
            response.StatusText = "OK";
            _country.CheckProvider();
            return response;
        }

        #endregion Check provider

        #region Check receipt

        public virtual void CheckReceipt()
        {
            _country.CheckReceipt();
        }

        #endregion Check receipt

        #region Send receipt

        public SendReceiptResponse SendReceipt(SendReceiptRequest request)
        {
            return SendReceiptProvider(request);
        }

        public abstract SendReceiptResponse SendReceiptProvider(SendReceiptRequest reqeust);

        #endregion Send receipt

        #region Transaction handling

        public void Commit()
        {
        }

        public void Rollback()
        {
        }

        public abstract void RollbackProvider();

        #endregion Transaction handling

        #region Export

        public abstract void ExportData();

        #endregion Export

        #region Hardware commands

        public virtual void ReprintReceipt()
        {
        }

        public virtual void PrintNonFiscalReceipt()
        {
        }

        public virtual void OpenCashDrawer()
        {
        }

        #endregion Hardware commands

        #region Communication logging

        public void Log(CommunicationType communicationType, string message)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(Provider.ToString("g")).Append(" ").Append(communicationType.ToString("g")).Append("|");
            if (message?.Length <= 500)
            {
                sb.Append(message);
                Nt.Logging.Log.Communication.Info(sb);
            }
            else
            {
                var shortMessage = new System.Text.StringBuilder().Append(sb).Append(message.Substring(0, 500));
                Nt.Logging.Log.Communication.Info(shortMessage);
                sb.Append(message);
                Nt.Logging.Log.Communication.Debug(sb);
            }
        }

        #endregion Communication logging
    }
}