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
            //
            var binding = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.None);
            binding.OpenTimeout = timespan;
            binding.CloseTimeout = timespan;
            binding.SendTimeout = timespan;
            binding.ReceiveTimeout = timespan;
            //
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
        /// <param name="mediumId"></param>
        /// <returns></returns>
        public async Task<InformationResponse> GetMediumInformation(string mediumId)
        {
            var response = new InformationResponse();

            var svsRequest = new NetworkRequest();
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.merchant = new Merchant();
            svsRequest.merchant.merchantName = "Gift Card Merchant, INC";
            svsRequest.merchant.merchantNumber = "061286";
            svsRequest.merchant.storeNumber = "0000009999";
            svsRequest.merchant.division = "00000";
            svsRequest.routingID = "301";
            svsRequest.stan = "123456"; //(HHMMSS)
            

            response.Currency = "EUR";

            try
            {
                var svsResponse = await svsSoapClient.networkAsync(svsRequest);
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
        /// <returns></returns>
        public Task<List<InformationResponse>> GetMediumInformation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debitRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> Debit(DebitRequest debitRequest)
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
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> Credit(Models.CreditRequest creditRequest)
        {
            var response = new BookingResponse();
            var svsRequest = new IssueGiftCardRequest();

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
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> CancelDebit(CancellationRequest cancellationRequest)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationRequest"></param>
        /// <returns></returns>
        public async Task<BookingResponse> CancelCredit(CancellationRequest cancellationRequest)
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