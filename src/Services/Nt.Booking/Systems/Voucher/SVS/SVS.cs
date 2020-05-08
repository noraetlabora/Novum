using Microsoft.AspNetCore.DataProtection.Repositories;
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
        /// <summary></summary>
        private SvsSoapClient svsSoapClient;

        /// <summary> </summary>
        public NtBooking.BookingSystemType Type { get => NtBooking.BookingSystemType.SVS; }

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
        /// <param name="metadata"></param>
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public async Task<InformationResponse> GetMediumInformation(string mediumId, MetaData metadata)
        {
            var response = new InformationResponse();
            var svsResponse = new balanceInquiryResponse1();

            var svsRequest = new BalanceInquiryRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantName = metadata.ClientName;
            svsRequest.merchant.division = metadata.ServiceAreaName;
            svsRequest.routingID = "5045076327250000000";
            svsRequest.stan = System.DateTime.Now.ToString("HHmmss");
            svsRequest.amount = new Amount();
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = "EUR";
            svsRequest.card.cardNumber = mediumId;
            svsRequest.card.pinNumber = "0999";

            try
            {
                svsResponse = await svsSoapClient.balanceInquiryAsync(svsRequest);
                if (!svsResponse.balanceInquiryReturn.returnCode.returnCode.Equals("01"))
                {
                    response.Message = svsResponse.balanceInquiryReturn.returnCode.ToString();
                    return response;
                }
            }
            catch (System.ServiceModel.FaultException ex)
            {
                ThrowSvsException(ex);
            }

            response.Currency = svsResponse.balanceInquiryReturn.card.cardCurrency;
            response.Credit.Amount = (decimal)svsResponse.balanceInquiryReturn.balanceAmount.amount;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task<List<InformationResponse>> GetMediumInformation(MetaData metadata)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> Debit(string mediumId, DebitRequest debitRequest)
        {
            var response = new BookingResponse();
            var svsRequest = new RedemptionRequest();
            try
            {
                var svsResponse = await svsSoapClient.redemptionAsync(svsRequest);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                ThrowSvsException(ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> Credit(string mediumId, Models.CreditRequest creditRequest)
        {
            var response = new BookingResponse();
            var svsRequest = new IssueGiftCardRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantName = creditRequest.MetaData.ClientName;
            svsRequest.merchant.division = creditRequest.MetaData.ServiceAreaName;
            svsRequest.routingID = "5045076327250000000";
            svsRequest.stan = System.DateTime.Now.ToString("HHmmss");
            svsRequest.issueAmount = new Amount();
            svsRequest.issueAmount.amount = (double)creditRequest.Amount;
            svsRequest.issueAmount.currency = "EUR";
            svsRequest.card = new Card();
            svsRequest.card.cardCurrency = "EUR";
            svsRequest.card.cardNumber = mediumId;
            svsRequest.card.pinNumber = "0999";

            try
            {
                var svsResponse = await svsSoapClient.issueGiftCardAsync(svsRequest);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                ThrowSvsException(ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> CancelDebit(string mediumId, CancellationRequest cancellationRequest)
        {
            var response = new BookingResponse();
            var svsRequest = new CancelRequest();

            try
            {
                var svsResponse = await svsSoapClient.cancelAsync(svsRequest);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                ThrowSvsException(ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediumId"></param>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> CancelCredit(string mediumId, CancellationRequest cancellationRequest)
        {
            var response = new BookingResponse();
            var svsRequest = new CancelRequest();

            try
            {
                var svsResponse = await svsSoapClient.cancelAsync(svsRequest);
                //response.Id = svsResponse.
            }
            catch (System.ServiceModel.FaultException ex)
            {
                ThrowSvsException(ex);
            }

            return response;
        }

        private void ThrowSvsException(System.ServiceModel.FaultException ex)
        {
            switch (ex.Code.Name)
            {
                case "InvalidSecurityToken":
                    throw new BookingException(ex.Message, Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized, Type, ex.Message, ex.Code.Name, null);
            }

            //throw fault exception
            throw ex;
        }

    }
}