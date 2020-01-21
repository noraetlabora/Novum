using System;
using System.Collections.Generic;
using Nt.Booking.Models;

namespace Nt.Booking.Systems.Voucher.SVS
{
    public class SVS : BookingSystemBase
    {
        private SvsSoapClient svsSoapClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">uri of the svs endpoint eg. https://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay </param>
        /// <param name="username">username for the svs service</param>
        /// <param name="password">password for the svs service</param>
        /// <param name="timeout">timeout in seconds for opening or closing a connection, sending request, receiving a response</param>
        public SVS(string uri, string username, string password, int timeout)
        {
            BookingSystem = NtBooking.BookingSystemType.SVS;
            svsSoapClient = new SvsSoapClient(uri, username, password, timeout);
        }

        public override BookingResponse Cancel(CancellationRequest cancellationRequest)
        {
            throw new NotImplementedException();
        }

        public override InformationResponse GetMediumInformation(string mediumId)
        {
            var networkRequest = new NetworkRequest();
            networkRequest.date = System.DateTime.Now.ToString("s"); //2011-08-15T10:16:51  (YYYY-MM-DDTHH:MM:SS)
            networkRequest.merchant = new Merchant();
            networkRequest.merchant.merchantName = "Gift Card Merchant, INC";
            networkRequest.merchant.merchantNumber = "061286";
            networkRequest.merchant.storeNumber = "0000009999";
            networkRequest.merchant.division = "00000";
            networkRequest.routingID = "301";
            networkRequest.stan = "123456"; //(HHMMSS)


            var networkResponse = svsSoapClient.networkAsync(networkRequest);

            return null;
        }

        public override List<InformationResponse> GetMediumInformation()
        {
            throw new NotImplementedException();
        }

        public override Models.BookingResponse Pay(Models.PaymentRequest paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
