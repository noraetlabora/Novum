using Nt.Booking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Booking.Systems.Voucher.SVS
{
    public class SVS : IBookingSystem
    {
        private SvsSoapClient svsSoapClient;

        public NtBooking.BookingSystemType Type { get => NtBooking.BookingSystemType.SVS; }

        public SVS(string uri, string username, string password, int timeout)
        {
            var timespan = new TimeSpan(0, 0, timeout);
            //
            var binding = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport);
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

        public async Task<BookingResponse> Cancel(CancellationRequest cancellationRequest)
        {
            var response = new BookingResponse();

            var svsRequest = new CancelRequest();
            svsRequest.card = new Card();
            svsRequest.card.cardNumber = "6006491286999929112";
            svsRequest.card.cardCurrency = "EUR"; //paymentRequest.Sales[0].Currency;
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.transactionAmount = new Amount();
            svsRequest.transactionAmount.amount = 123.23;
            svsRequest.transactionAmount.currency = "EUR";
            svsRequest.invoiceNumber = "Belegnr: 12345678";

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
                //var svsResponse = await svsSoapClient.networkAsync(svsRequest);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                ThrowSvsException(ex);
            }

            return response;
        }

        public Task<List<InformationResponse>> GetMediumInformation()
        {
            throw new NotImplementedException();
        }

        public async Task<BookingResponse> Pay(PaymentRequest paymentRequest)
        {
            var response = new BookingResponse();

            var svsRequest = new RedemptionRequest();
            svsRequest.card = new Card();
            svsRequest.card.cardNumber = "6006491286999929112";
            svsRequest.card.cardCurrency = "EUR"; //paymentRequest.Sales[0].Currency;
            svsRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            svsRequest.redemptionAmount = new Amount();
            svsRequest.redemptionAmount.amount = 123.23;
            svsRequest.redemptionAmount.currency = "EUR";
            svsRequest.invoiceNumber = "Belegnr: 12345678";

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