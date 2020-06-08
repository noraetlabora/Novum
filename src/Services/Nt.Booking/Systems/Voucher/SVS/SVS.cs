using Microsoft.Extensions.Configuration;
using Nt.Booking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Voucher.SVS
{    
    /// <summary>
    /// SVS booking system service handler.
    /// </summary>
    public class SVS : IBookingSystem
    {
        /// <summary>Get the SVS booking system type.</summary>
        public NtBooking.BookingSystemType BookingSystem { get => NtBooking.BookingSystemType.SVS; }
        /// <summary>Get the SVS booking system name.</summary>
        public string BookingSystemName { get => "SVS"; }
        /// <summary>Soap client object.</summary>
        private SvsSoapClient _svsSoapClient = null;
        /// <summary>User defined service configuration.</summary>
        private SvsServiceConfiguration _config = null;
        /// <summary>Random number generator.</summary>
        private Random _random = new Random();
        /// <summary>Supported SVS cards, defined by the service configuration.</summary>
        private List<SvsCardHandler> _svsCardPool = new List<SvsCardHandler>();

        /// <summary>
        /// SVS voucher service. Create an object to handle SVS voucher service queries.
        /// </summary>
        /// <param name="configuration">Main configuration file.</param>
        public SVS(in SvsServiceConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("Configuration has not been initialized.");

            if (configuration.Version != "0.3")
                throw new ArgumentNullException("Invalid configuration. Please update to newer version.");

            Initialize(configuration);

            var timespan = new TimeSpan(0, 0, 0, configuration.Connection.Timeout, 0);
            var binding = SvsSoapClient.GetBindingForEndpoint();
            binding.OpenTimeout = timespan;
            binding.CloseTimeout = timespan;
            binding.SendTimeout = timespan;
            binding.ReceiveTimeout = timespan;
            var endpoint = new System.ServiceModel.EndpointAddress(configuration.Connection.Address);

            _svsSoapClient = new SvsSoapClient(binding, endpoint);
            // credentials
            _svsSoapClient.ClientCredentials.UserName.UserName = configuration.Connection.Username;
            _svsSoapClient.ClientCredentials.UserName.Password = configuration.Connection.Password;
        }

        /// <summary>
        /// Get information of a medium.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <param name="metaData">Additional meta information.</param>
        /// <returns>Information response object.</returns>
        public async Task<Response> GetMediumInformation(string mediumId, MetaData metaData)
        {
            var mediumHandler = GetMediumHandler(mediumId, _svsCardPool);

            if (mediumHandler == null)
                throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherInvalidBarcode"), mediumHandler.GetCardNumber(mediumId)));

            //assemble request for SVS
            var svsRequest = new BalanceInquiryRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.Arguments.MerchantNumber;
            svsRequest.merchant.merchantName = _config.Arguments.MerchantName;
            svsRequest.routingID = _config.Arguments.RoutingId;
            svsRequest.stan = _random.Next(100000, 999999).ToString();
            svsRequest.amount = new Amount();
            svsRequest.amount.currency = NtBooking.ServiceConfig.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.ServiceConfig.Currency;
            svsRequest.card.cardNumber = mediumHandler.GetCardNumber(mediumId);
            svsRequest.card.pinNumber = mediumHandler.GetPinNumber(mediumId);

            //send asynchronous redemption request to SVS
            var svsResponse = await _svsSoapClient.balanceInquiryAsync(svsRequest).ConfigureAwait(false);

            //return error response if response of svs contains error
            if (SvsResponseHasError(svsResponse.balanceInquiryReturn.returnCode))
                return SvsErrorResponse(mediumId, svsResponse.balanceInquiryReturn.returnCode);

            //assemble booking response of the svs response
            var response = new InformationResponse();
            response.Credit = new CreditResponse();
            response.Currency = svsRequest.card.cardCurrency;
            response.Credit.Amount = (decimal)svsResponse.balanceInquiryReturn.balanceAmount.amount;
            return response;
        }


        /// <summary>
        /// Get information of all media, not used in SVS.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task<List<Response>> GetMediumInformation(MetaData metadata)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pay with voucher (debit).
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <param name="debitRequest">Debit request object.</param>
        /// <returns>Booking response object.</returns>
        public async Task<Response> Debit(string mediumId, DebitRequest debitRequest)
        {
            var mediumHandler = GetMediumHandler(mediumId, _svsCardPool);

            if (mediumHandler == null)
                throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherInvalidBarcode"), mediumHandler.GetCardNumber(mediumId)));

            if(mediumHandler.OnlyFullRedemption)
            {
                if(int.TryParse(mediumHandler.GetAmount(mediumId), out int voucherAmount))
                {
                    if (debitRequest.Amount != voucherAmount)
                    {
                        throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherNoFullRedemption"), mediumHandler.GetCardNumber(mediumId)));
                    }
                }
                else
                {
                    throw new Exception(Resources.Dictionary.GetString("VoucherInvalidBarcode"));
                }
            }

            //assemble request for SVS
            var svsRequest = new RedemptionRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.Arguments.MerchantNumber;
            svsRequest.merchant.merchantName = _config.Arguments.MerchantName;
            svsRequest.routingID = _config.Arguments.RoutingId;
            svsRequest.stan = debitRequest.MetaData.transactionId;
            svsRequest.redemptionAmount = new Amount();
            svsRequest.redemptionAmount.amount = (double)debitRequest.Amount;
            svsRequest.redemptionAmount.currency = NtBooking.ServiceConfig.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.ServiceConfig.Currency;
            svsRequest.card.cardNumber = mediumHandler.GetCardNumber(mediumId);
            svsRequest.card.pinNumber = mediumHandler.GetPinNumber(mediumId);

            //send asynchronous redemption request to SVS
            var svsResponse = await _svsSoapClient.redemptionAsync(svsRequest);

            //return error response if response of svs contains error
            if (SvsResponseHasError(svsResponse.redemptionReturn.returnCode))
                return SvsErrorResponse(mediumId, svsResponse.redemptionReturn.returnCode);

            //assemble booking response of the svs response
            var response = new BookingResponse();
            response.ApprovedAmount = (decimal)svsResponse.redemptionReturn.approvedAmount.amount;
            response.BalanceAmount = (decimal)svsResponse.redemptionReturn.balanceAmount.amount;
            response.Currency = svsResponse.redemptionReturn.approvedAmount.currency;

            return response;
        }

        /// <summary>
        /// Increase the value of the voucher.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <param name="creditRequest">Credit request object.</param>
        /// <returns>Booking response object.</returns>
        public async Task<Response> Credit(string mediumId, Models.CreditRequest creditRequest)
        {
            var mediumHandler = GetMediumHandler(mediumId, _svsCardPool);

            if (mediumHandler == null)
                throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherInvalidBarcode"), mediumHandler.GetCardNumber(mediumId)));

            if (mediumHandler.MaxCharge == 0)
                throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherAlreadyIssued"), mediumHandler.GetCardNumber(mediumId)));

            //assemble request for SVS
            var svsRequest = new IssueGiftCardRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.Arguments.MerchantNumber;
            svsRequest.merchant.merchantName = _config.Arguments.MerchantName;
            svsRequest.routingID = _config.Arguments.RoutingId;
            svsRequest.stan = creditRequest.MetaData.transactionId;
            svsRequest.issueAmount = new Amount();
            svsRequest.issueAmount.amount = (double)creditRequest.Amount;
            svsRequest.issueAmount.currency = NtBooking.ServiceConfig.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.ServiceConfig.Currency;
            svsRequest.card.cardNumber = mediumHandler.GetCardNumber(mediumId);
            svsRequest.card.pinNumber = mediumHandler.GetPinNumber(mediumId);

            //send asynchronous redemption request to SVS
            var svsResponse = await _svsSoapClient.issueGiftCardAsync(svsRequest);

            //return error response if response of svs contains error
            if (SvsResponseHasError(svsResponse.issueGiftCardReturn.returnCode))
                return SvsErrorResponse(mediumId, svsResponse.issueGiftCardReturn.returnCode);

            //assemble booking response of the svs response
            var response = new BookingResponse();
            response.ApprovedAmount = (decimal)svsResponse.issueGiftCardReturn.approvedAmount.amount;
            response.BalanceAmount = (decimal)svsResponse.issueGiftCardReturn.balanceAmount.amount;
            response.Currency = svsResponse.issueGiftCardReturn.approvedAmount.currency;

            return response;
        }

        /// <summary>
        /// Cancel existing debit entry.  
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <param name="cancellationRequest">Cancellation request object.</param>
        /// <returns>Booking request object.</returns>
        public async Task<Response> CancelDebit(string mediumId, CancellationRequest cancellationRequest)
        {
            return await Cancel(mediumId, cancellationRequest);
        }

        /// <summary>
        /// Cancel existing credit entry.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <param name="cancellationRequest">Cancellation request object.</param>
        /// <returns>Booking request object.</returns>
        public async Task<Response> CancelCredit(string mediumId, CancellationRequest cancellationRequest)
        {
            return await Cancel(mediumId, cancellationRequest);
        }

        #region private methods

        /// <summary>
        /// Check if medium is in the valid range.
        /// </summary>
        /// <param name="mediumId">Medium identification number of the card to check.</param>
        /// <param name="svsCards">Cards that include the valid ranges.</param>
        /// <returns>True, card is in range, otherwise false.</returns>
        private SvsCardHandler GetMediumHandler(in string mediumId, in List<SvsCardHandler> svsCards)
        {
            foreach (var card in svsCards)
            {
                if(card.IsInRange(mediumId))
                {
                    return card;
                }
            }
            return null;
        }


        private async Task<Response> Cancel(string mediumId, CancellationRequest cancellationRequest)
        {
            var mediumHandler = GetMediumHandler(mediumId, _svsCardPool);

            if (mediumHandler == null)
                throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherInvalidBarcode"), mediumHandler.GetCardNumber(mediumId)));

            //assemble request for SVS
            var svsRequest = new CancelRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.Arguments.MerchantNumber;
            svsRequest.merchant.merchantName = _config.Arguments.MerchantName;
            svsRequest.routingID = _config.Arguments.RoutingId;
            svsRequest.stan = cancellationRequest.MetaData.transactionId;
            svsRequest.transactionAmount = new Amount();
            svsRequest.transactionAmount.amount = (double)cancellationRequest.Amount;
            svsRequest.transactionAmount.currency = NtBooking.ServiceConfig.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.ServiceConfig.Currency;
            svsRequest.card.cardNumber = mediumHandler.GetCardNumber(mediumId);
            svsRequest.card.pinNumber = mediumHandler.GetPinNumber(mediumId);
            svsRequest.invoiceNumber = cancellationRequest.InvoiceId;

            //send asynchronous redemption request to SVS
            var svsResponse = await _svsSoapClient.cancelAsync(svsRequest);

            //return error response if response of svs contains error
            if (SvsResponseHasError(svsResponse.cancelReturn.returnCode))
                return SvsErrorResponse(mediumId, svsResponse.cancelReturn.returnCode);

            //assemble booking response of the svs response
            var response = new BookingResponse();

            return response;

        }

        /// <summary>
        /// SVS error response handler.
        /// </summary>
        /// <param name="mediumId">Medium identification number.</param>
        /// <param name="returnCode">Return code that is handled.</param>
        /// <returns>Error response object.</returns>
        private ErrorResponse SvsErrorResponse(string mediumId, ReturnCode returnCode)
        {
            var mediumHandler = GetMediumHandler(mediumId, _svsCardPool);

            if (mediumHandler == null)
                throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherInvalidBarcode"), mediumHandler.GetCardNumber(mediumId)));

            var voucherNumber = mediumHandler.GetCardNumber(mediumId);
            var errorResponse = new ErrorResponse();
            errorResponse.Error.BookingSystem = this.BookingSystemName;
            errorResponse.Error.PartnerCode = returnCode.returnCode;
            errorResponse.Error.PartnerMessage = returnCode.returnDescription;

            switch (returnCode.returnCode)
            {
                case "01": //Approval
                    return null;
                case "02": //Inactive Card
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherInactive;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherInactive"), returnCode.returnCode);
                    break;
                case "03": //Invalid Card Number
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherInvalidNumber;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherInvalidNumber"), returnCode.returnCode);
                    break;
                case "04": //Invalid Transaction Code
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherInvalidTransactionCode;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherInvalidTransactionCode"), returnCode.returnCode);
                    break;
                case "05": //Insufficient Funds
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherInsufficientFunds;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherInsufficientFunds"), returnCode.returnCode);
                    break;
                case "08": //VoucherUnknown
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherUnknown;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherUnknown"), returnCode.returnCode);
                    break;
                case "15": //Host Unavailable
                    errorResponse.Error.Code = Enums.ErrorCode.HostUnavailable;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("HostUnavailable"), returnCode.returnCode);
                    break;
                case "19": //Invalid CCV or SSC
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherInvalidCcvOrSsc;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherInvalidCcvOrSsc"), returnCode.returnCode);
                    break;
                case "20": //Pin Invalid
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherInvalidPin;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherInvalidPin"), returnCode.returnCode);
                    break;
                case "21": //Card Already Issued
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherAlreadyIssued;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherAlreadyIssued"), returnCode.returnCode);
                    break;
                case "22": //Card Already Issued
                    errorResponse.Error.Code = Enums.ErrorCode.VoucherNotIssued;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherNotIssued"), returnCode.returnCode);
                    break;
                default:
                    errorResponse.Error.Code = Enums.ErrorCode.Error;
                    errorResponse.Error.Message = string.Format(Resources.Dictionary.GetString("VoucherError"), returnCode.returnCode);
                    break;
            }
            return errorResponse;
        }

        /// <summary>
        /// Check if SVS response is an error response.
        /// </summary>
        /// <param name="returnCode">Return code.</param>
        /// <returns>True, if it is an error, otherwise false.</returns>
        private bool SvsResponseHasError(ReturnCode returnCode)
        {
            // 01 - Approval
            if (returnCode.returnCode.Equals("01"))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Initialize object with a user defined configuration.
        /// </summary>
        /// <param name="configuration">User defined server configuration.</param>
        private void Initialize(in SvsServiceConfiguration configuration)
        {
            _config = configuration;
            _svsCardPool.Clear();

            foreach (var card in configuration.Arguments.Cards)
            {
                SvsCardHandler svsCard = new SvsCardHandler(card.Range)
                {
                    Type = card.Type,
                    MaxCharge = card.MaxCharge,
                    OnlyFullRedemption = card.OnlyFullRedemption,
                    PinPattern = card.PinPattern,
                };
                _svsCardPool.Add(svsCard);
            }
        }
        #endregion
    }
}