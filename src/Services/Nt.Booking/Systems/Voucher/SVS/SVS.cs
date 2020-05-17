using Microsoft.AspNetCore.DataProtection.Repositories;
using Nt.Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Voucher.SVS
{
    /// <summary>
    /// 
    /// </summary>
    public class SVS : IBookingSystem
    {
        /// <summary></summary>
        private SvsSoapClient svsSoapClient;
        private string RoutingId;
        private string MerchantName;
        private string MerchantNumber;
        private Random random = new Random();

        /// <summary> </summary>
        public NtBooking.BookingSystemType BookingSystem { get => NtBooking.BookingSystemType.SVS; }
        /// <summary> </summary>
        public string BookingSystemName { get => "SVS"; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="timeout"></param>
        public SVS(string uri, string username, string password, int timeout)
        {
            var timespan = new TimeSpan(0, 0, timeout);
            var binding = SvsSoapClient.GetBindingForEndpoint();
            binding.OpenTimeout = timespan;
            binding.CloseTimeout = timespan;
            binding.SendTimeout = timespan;
            binding.ReceiveTimeout = timespan;
            var endpoint = new System.ServiceModel.EndpointAddress(uri);
            //
            svsSoapClient = new SvsSoapClient(binding, endpoint);
            //credentials
            svsSoapClient.ClientCredentials.UserName.UserName = username;
            svsSoapClient.ClientCredentials.UserName.Password = password;
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        public void SetArguments(List<string> arguments)
        {
            RoutingId = arguments[0];
            MerchantNumber = arguments[1];
            MerchantName = arguments[2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public async Task<Response> GetMediumInformation(string mediumId, MetaData metadata)
        {
            var svsRequest = new BalanceInquiryRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = MerchantNumber;
            svsRequest.merchant.merchantName = MerchantName;
            svsRequest.routingID = RoutingId;
            svsRequest.stan = random.Next(100000, 999999).ToString();
            svsRequest.amount = new Amount();
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = "EUR";
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);

            var svsResponse = await svsSoapClient.balanceInquiryAsync(svsRequest);

            if (SvsResponseHasError(svsResponse.balanceInquiryReturn.returnCode))
                return SvsErrorResponse(svsResponse.balanceInquiryReturn.returnCode);

            var response = new InformationResponse();
            response.Currency = svsResponse.balanceInquiryReturn.card.cardCurrency;
            response.Credit = new CreditResponse();
            response.Credit.Amount = (decimal)svsResponse.balanceInquiryReturn.balanceAmount.amount;
            return response;
        }

        private ErrorResponse SvsErrorResponse(ReturnCode returnCode)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Error.BookingSystem = this.BookingSystemName;
            errorResponse.Error.PartnerCode = returnCode.returnCode;
            errorResponse.Error.PartnerMessage = returnCode.returnDescription;

            switch (returnCode.returnCode)
            {
                case "01": //Approval
                    return null;
                case "02": //Inactive Card
                    errorResponse.Error.Code = Enums.StatusCode.VoucherInactive;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("Voucher_Inactive");
                    break;
                case "03": //Invalid Card Number
                    errorResponse.Error.Code = Enums.StatusCode.VoucherInvalid;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("Voucher_Invalid");
                    break;
                case "05": //Insufficient Funds
                    errorResponse.Error.Code = Enums.StatusCode.VoucherInsufficient;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("Voucher_Insufficient");
                    break;
                case "15": //Host Unavailable
                    errorResponse.Error.Code = Enums.StatusCode.HostUnavailable;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("HostUnavailable");
                    break;
                case "20": //Pin Invalid
                    errorResponse.Error.Code = Enums.StatusCode.VoucherPinInvalid;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("Voucher_PinInvalid");
                    break;
                case "21": //Card Already Issued
                    errorResponse.Error.Code = Enums.StatusCode.VoucherAlreadyIssued;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("Voucher_AlreadyIssued");
                    break;
                default:
                    errorResponse.Error.Code = Enums.StatusCode.Error;
                    errorResponse.Error.Message = Resources.Dictionary.GetString("Voucher_Error");
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
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task<List<Response>> GetMediumInformation(MetaData metadata)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medium"></param>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public async Task<Response> Debit(string medium, DebitRequest debitRequest)
        {
            var response = new BookingResponse();
            var svsRequest = new RedemptionRequest();
            var svsResponse = await svsSoapClient.redemptionAsync(svsRequest);

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
            var svsRequest = new IssueGiftCardRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = MerchantNumber;
            svsRequest.merchant.merchantName = MerchantName;
            svsRequest.routingID = RoutingId;
            svsRequest.stan = random.Next(100000, 999999).ToString();
            svsRequest.issueAmount = new Amount();
            svsRequest.issueAmount.amount = (double)creditRequest.Amount;
            svsRequest.issueAmount.currency = "EUR";
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = "EUR";
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);

            var svsResponse = await svsSoapClient.issueGiftCardAsync(svsRequest);

            if (SvsResponseHasError(svsResponse.issueGiftCardReturn.returnCode))
                return SvsErrorResponse(svsResponse.issueGiftCardReturn.returnCode);

            var response = new BookingResponse();

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

        private async Task <Response> Cancel(string mediumId, CancellationRequest cancellationRequest)
        {
            var svsRequest = new CancelRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = MerchantNumber;
            svsRequest.merchant.merchantName = MerchantName;
            svsRequest.routingID = RoutingId;
            svsRequest.stan = random.Next(100000, 999999).ToString();
            svsRequest.transactionAmount = new Amount();
            //svsRequest.transactionAmount.amount = (double)cancellationRequest.
            //svsRequest.transactionAmount.currency = "EUR";
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = "EUR";
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);

            var svsResponse = await svsSoapClient.cancelAsync(svsRequest);

            var response = new BookingResponse();

            return response;

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
            if (mediumId?.Length == 23)                       // SSC             
                return mediumId.Substring(19, 4) + "0000";    // send pin like "SSSS0000"                              
            if (mediumId?.Length >= 27)                       // Pin
                return "0000" + mediumId.Substring(23, 4);    // send pin like "0000PPPP

            Nt.Logging.Log.Server.Error("SVS - pin number not detachable from mediumId " + mediumId);
            return "00000000";
        }

        /// <summary>
        /// extract 19 digits card number
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        private string GetCardNumber(string mediumId)
        {
            if (mediumId?.Length >= 19)
                return mediumId.Substring(0, 19);

            return mediumId;
        }


        //TODO: delete activation
        private void activate(string mediumId)
        {
            var svsRequest = new CardActivationRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantNumber = MerchantNumber;
            svsRequest.merchant.merchantName = MerchantName;
            svsRequest.routingID = RoutingId;
            svsRequest.stan = random.Next(100000, 999999).ToString();
            svsRequest.activationAmount = new Amount();
            svsRequest.activationAmount.amount = 100;
            svsRequest.activationAmount.currency = "EUR";
            svsRequest.card = new Card();
            svsRequest.card.cardNumber = GetCardNumber(mediumId);
            svsRequest.card.pinNumber = GetPinNumber(mediumId);
            var svsResponse = svsSoapClient.cardActivation(svsRequest);
            System.Diagnostics.Debug.WriteLine(svsResponse.returnCode.returnCode + " - " + svsResponse.returnCode.returnDescription);
        }
    }
}