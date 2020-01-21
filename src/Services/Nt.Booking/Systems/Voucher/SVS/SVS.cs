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

        public override Models.BookingResponse Cancel()
        {
            throw new NotImplementedException();
        }

        public override InformationResponse GetMediumInformation(string mediumId)
        {
            //svsSoapClient.networkAsync()
            throw new NotImplementedException();
        }

        public override List<InformationResponse> GetMediumInformation()
        {
            throw new NotImplementedException();
        }

        public override Models.BookingResponse Pay()
        {
            throw new NotImplementedException();
        }
    }
}
