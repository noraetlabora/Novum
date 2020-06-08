using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Nt.Booking.Systems.Voucher.SVS
{
    [DataContract]
    public class SvsServiceConfiguration
    {
        [DataContract]
        public struct ServiceConnection
        {
            [DataMember(Name = "address")]
            public string Address { get; set; }
            [DataMember(Name = "port")]
            public int Port { get; set; }
            [DataMember(Name = "username")]
            public string Username { get; set; }
            [DataMember(Name = "password")]
            public string Password { get; set; }
            [DataMember(Name = "timeout")]
            public int Timeout { get; set; }
        }
        [DataContract]
        public struct ServiceOptions
        {
            [DataMember(Name = "currency")]
            public string Currency { get; set; }
            [DataMember(Name = "language")]
            public string Language { get; set; }
        }
        [DataContract]
        public struct ServiceCards
        {
            [DataMember(Name = "type")]
            public string Type { get; set; }
            [DataMember(Name = "range")]
            public string Range { get; set; }
            [DataMember(Name = "maxCharge")]
            public int MaxCharge { get; set; }
            [DataMember(Name = "onlyFullRedemption")]
            public bool OnlyFullRedemption { get; set; }
            [DataMember(Name = "pinPattern")]
            public string PinPattern { get; set; }
        }
        [DataContract]
        public struct ServiceArguments
        {
            [DataMember(Name = "routingId")]
            public string RoutingId { get; set; }
            [DataMember(Name = "merchantNumber")]

            public string MerchantNumber { get; set; }
            [DataMember(Name = "merchantName")]

            public string MerchantName { get; set; }
            [DataMember(Name = "cards")]

            public List<ServiceCards> Cards { get; set; }
        }
        [DataMember(Name = "version")]

        public string Version { get; set; }
        [DataMember(Name = "bookingSystem")]

        public int BookingSystem { get; set; }
        [DataMember(Name = "connection")]

        public ServiceConnection Connection { get; set; }
        [DataMember(Name = "options")]

        public ServiceOptions Options { get; set; }
        [DataMember(Name = "arguments")]


        public ServiceArguments Arguments { get; set; }

        /// <summary>
        /// Serialze data structure to a JSON string.
        /// </summary>
        /// <returns>JSON formatted data abstraction of the current configuration.</returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
