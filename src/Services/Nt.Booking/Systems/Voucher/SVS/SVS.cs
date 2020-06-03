using Nt.Booking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Voucher.SVS
{
    /// <summary>
    /// 
    /// </summary>
    public class SVS : IBookingSystem
    {
        /// <summary> </summary>
        public NtBooking.BookingSystemType BookingSystem { get => NtBooking.BookingSystemType.SVS; }
        /// <summary> </summary>
        public string BookingSystemName { get => "SVS"; }

        private SvsSoapClient _svsSoapClient = null;
        private ServerConfiguration _config = null;
        private Random _random = new Random();
        /// <summary>
        /// SVS voucher service. Create an object to handle SVS voucher service queries.
        /// </summary>
        /// <param name="configuration">Main configuration file.</param>
        public SVS(in ServerConfiguration configuration)
        {
            if(configuration == null) 
                throw new ArgumentNullException("Configuration has not been initialized.");

            if(configuration.Version != "0.2")
                throw new ArgumentNullException("Invalid configuration. Please update to newer version.");

            _config = configuration;

            var timespan = new TimeSpan(0, 0, 0, configuration.Timeout, 0);
            var binding = SvsSoapClient.GetBindingForEndpoint();
            binding.OpenTimeout = timespan;
            binding.CloseTimeout = timespan;
            binding.SendTimeout = timespan;
            binding.ReceiveTimeout = timespan;
            var endpoint = new System.ServiceModel.EndpointAddress(configuration.Address);
            //
            _svsSoapClient = new SvsSoapClient(binding, endpoint);
            //credentials
            _svsSoapClient.ClientCredentials.UserName.UserName = configuration.Username;
            _svsSoapClient.ClientCredentials.UserName.Password = configuration.Password;
            //
        }

        /// <summary>
        /// get information of a medium
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public async Task<Response> GetMediumInformation(string mediumId, MetaData metaData)
        {
            //check if information is possible, throws exception
            CheckInfo(mediumId, metaData);

            //assemble request for SVS
            var svsRequest = new BalanceInquiryRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.MerchantNumber;
            svsRequest.merchant.merchantName = _config.MerchantName;
            svsRequest.routingID = _config.RoutingId;
            svsRequest.stan = _random.Next(100000, 999999).ToString();
            svsRequest.amount = new Amount();
            svsRequest.amount.currency = NtBooking.serverConfiguration.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.serverConfiguration.Currency;
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);

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
        /// get information of all media, not used in SVS
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task<List<Response>> GetMediumInformation(MetaData metadata)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// pay with voucher (debit)
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public async Task<Response> Debit(string mediumId, DebitRequest debitRequest)
        {
            //check if debit is possible, throws exception
            CheckDebit(mediumId, debitRequest);

            //assemble request for SVS
            var svsRequest = new RedemptionRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.MerchantNumber;
            svsRequest.merchant.merchantName = _config.MerchantName;
            svsRequest.routingID = _config.RoutingId;
            svsRequest.stan = debitRequest.MetaData.transactionId;
            svsRequest.redemptionAmount = new Amount();
            svsRequest.redemptionAmount.amount = (double)debitRequest.Amount;
            svsRequest.redemptionAmount.currency = NtBooking.serverConfiguration.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.serverConfiguration.Currency;
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);

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
        /// increase the value of the voucher
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public async Task<Response> Credit(string mediumId, Models.CreditRequest creditRequest)
        {
            //check if credit is possible, throws exception
            CheckCredit(mediumId, creditRequest);

            //assemble request for SVS
            var svsRequest = new IssueGiftCardRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.MerchantNumber;
            svsRequest.merchant.merchantName = _config.MerchantName;
            svsRequest.routingID = _config.RoutingId; 
            svsRequest.stan = creditRequest.MetaData.transactionId;
            svsRequest.issueAmount = new Amount();
            svsRequest.issueAmount.amount = (double)creditRequest.Amount;
            svsRequest.issueAmount.currency = NtBooking.serverConfiguration.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.serverConfiguration.Currency;
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);

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
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<Response> CancelDebit(string mediumId, CancellationRequest cancellationRequest)
        {
            return await Cancel(mediumId, cancellationRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<Response> CancelCredit(string mediumId, CancellationRequest cancellationRequest)
        {
            return await Cancel(mediumId, cancellationRequest);
        }

        #region private methods

        private async Task<Response> Cancel(string mediumId, CancellationRequest cancellationRequest)
        {
            //check if credit is possible, throws exception
            CheckCancel(mediumId, cancellationRequest);

            //assemble request for SVS
            var svsRequest = new CancelRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = _config.MerchantNumber;
            svsRequest.merchant.merchantName = _config.MerchantName;
            svsRequest.routingID = _config.RoutingId; 
            svsRequest.stan = cancellationRequest.MetaData.transactionId;
            svsRequest.transactionAmount = new Amount();
            svsRequest.transactionAmount.amount = (double)cancellationRequest.Amount;
            svsRequest.transactionAmount.currency = NtBooking.serverConfiguration.Currency;
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = NtBooking.serverConfiguration.Currency;
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);
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

        private ErrorResponse SvsErrorResponse(string mediumId, ReturnCode returnCode)
        {
            var voucherNumber = GetCardNumber(mediumId);
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

        private bool SvsResponseHasError(ReturnCode returnCode)
        {
            // 01 - Approval
            if (returnCode.returnCode.Equals("01"))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Secondary Security Code(SSC) is encoded in the QR-Code like xxxxxxxxxxxxxxxxxxxSSSS
        /// Pin from the manual input should be encoded like            xxxxxxxxxxxxxxxxxxx????PPPP
        /// The PinNumber is either SSSS0000 or 0000PPPP
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        private string GetPinNumber(string mediumId)
        {
            // Breuninger Center Voucher (no SSC or pin)
            if (IsBreuningerCenterVoucher(mediumId))
                return string.Empty;
            // SSC - send pin like "SSSS0000"   
            if (mediumId?.Length == 23)
                return mediumId.Substring(19, 4) + "0000";
            // Pin - send pin like "0000PPPP
            if (mediumId?.Length >= 27)
                return "0000" + mediumId.Substring(23, 4);

            Nt.Logging.Log.Server.Info("SVS - pin number not detachable from mediumId " + mediumId);
            return string.Empty;
        }

        /// <summary>
        /// extract 19 digits card number (or less)
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        private string GetCardNumber(string mediumId)
        {
            if (mediumId?.Length >= 19)
                return mediumId.Substring(0, 19);

            return mediumId;
        }


        private void CheckInfo(string mediumId, MetaData metaData)
        {
            //not yet implemented
        }

        private void CheckDebit(string mediumId, DebitRequest debitRequest)
        {
            CheckBreuningerCenterVoucherDebit(mediumId, debitRequest);
        }

        private void CheckCredit(string mediumId, CreditRequest creditRequest)
        {
            //not yet implemented
        }

        private void CheckCancel(string mediumId, CancellationRequest cancellationRequest)
        {
            //not yet implemented
        }

        #endregion

        #region Breuninger specifics
        private void CheckBreuningerCenterVoucherDebit(string mediumId, DebitRequest debitRequest)
        {
            //Breuninger Center Voucher just full debit
            if (IsBreuningerCenterVoucher(mediumId))
            {
                int voucherAmount = 0;
                if (mediumId?.Length >= 23)
                    voucherAmount = int.Parse(mediumId.Substring(19, 4));
                if (debitRequest.Amount != voucherAmount)
                {
                    throw new Exception(string.Format(Resources.Dictionary.GetString("VoucherNoFullRedemption"), GetCardNumber(mediumId)));
                }
            }
        }

        /// <summary>
        /// Determine if medium ID is between defined range.
        /// </summary>
        /// <param name="ranges">String that represents the ranges by minRange-maxRange</param>
        /// <param name="mediumId">Medium ID, e.g. voucher number.</param>
        /// <returns>True if in range, otherwise false.</returns>
        private bool IsInRange(in string ranges, in string mediumId)
        {
            var xRange = ranges.Split('-');

            if (int.TryParse(xRange[0].Trim(), out int minRange))
            {
                if (int.TryParse(xRange[1].Trim(), out int maxRange))
                {
                    if (int.TryParse(mediumId.Trim(), out int id))
                    {
                        if(id >= minRange && id <= maxRange)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if medium is of type center voucher.
        /// </summary>
        /// <param name="mediumId">Medium ID, e.g. voucher number.</param>
        /// <returns>True if in range, otherwise false.</returns>
        private bool IsBreuningerCenterVoucher(in string mediumId)
        {
            return IsInRange(_config.BRGECRange, mediumId);
        }

        #endregion
    }
}