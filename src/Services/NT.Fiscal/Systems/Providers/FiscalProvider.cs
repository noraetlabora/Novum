using NT.Fiscal.Models;
using NT.Fiscal.Systems.Countries;
using NT.Fiscal.Systems.Enums;

namespace NT.Fiscal.Systems.Providers
{
    public abstract class FiscalProvider
    {
        protected FiscalCountry _country;

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

        public virtual void CheckProvider()
        {
            _country.CheckProvider();
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