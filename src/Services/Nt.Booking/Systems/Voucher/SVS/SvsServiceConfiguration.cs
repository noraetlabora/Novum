using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Nt.Booking.Systems.Voucher.SVS
{
    /// <summary>
    /// SVS service configuration.
    /// 
    /// The SVS service is used to handle voucher interactions. This configuration
    /// extracts all necessary information from the basic configuration to connect
    /// to the SVS service provider and handle user queries.
    /// </summary>
    [DataContract]
    public class SvsServiceConfiguration
    {
        [DataContract]
        public struct ServiceConnection
        {
            /// <summary>Service provider address.</summary>
            [DataMember(Name = "address")]
            public string Address { get; set; }
            /// <summary>Service provider port.</summary>
            [DataMember(Name = "port")]
            public int Port { get; set; }
            /// <summary>Creditial username.</summary>
            [DataMember(Name = "username")]
            public string Username { get; set; }
            /// <summary>Creditial password.</summary>
            [DataMember(Name = "password")]
            public string Password { get; set; }
            /// <summary>Service connection timeout.</summary>
            [DataMember(Name = "timeout")]
            public int Timeout { get; set; }
        }
        [DataContract]
        public struct ServiceOptions
        {
            /// <summary>System currency setting.</summary>
            [DataMember(Name = "currency")]
            public string Currency { get; set; }
            /// <summary>System language setting.</summary>
            [DataMember(Name = "language")]
            public string Language { get; set; }
        }
        [DataContract]
        public struct ServiceCard
        {
            /// <summary>SVS specific. Title of the card type.</summary>
            [DataMember(Name = "type")]
            public string Type { get; set; }
            /// <summary>SVS specific. Range of the card number.</summary>
            [DataMember(Name = "range")]
            public string Range { get; set; }
            /// <summary>SVS specific. Maximum charging amount of the card.</summary>
            [DataMember(Name = "maxCharge")]
            public decimal MaxCharge { get; set; }
            /// <summary>SVS specific. Flag to set only full redemption.</summary>
            [DataMember(Name = "onlyFullRedemption")]
            public bool OnlyFullRedemption { get; set; }
            /// <summary>SVS specific. PIN pattern of the card, e.g. PPPPPPPP</summary>
            [DataMember(Name = "pinPattern")]
            public string PinPattern { get; set; }
        }
        [DataContract]
        public struct ServiceArguments
        {
            /// <summary>SVS specific. Routing ID. See the SVS interface documentation.</summary>
            [DataMember(Name = "routingId")]
            public string RoutingId { get; set; }
            /// <summary>SVS specific. Merchant Number. See the SVS interface documentation.</summary>
            [DataMember(Name = "merchantNumber")]
            public string MerchantNumber { get; set; }
            /// <summary>SVS specific. Merchant Name. See the SVS interface documentation.</summary>
            [DataMember(Name = "merchantName")]
            public string MerchantName { get; set; }
            /// <summary>SVS specific. Supported cards.</summary>
            [DataMember(Name = "cards")]
            public List<ServiceCard> Cards { get; set; }
        }
        /// <summary>Configuration file version.</summary>
        [DataMember(Name = "version")]
        public string Version { get; set; }
        /// <summary>Used booking system.</summary>
        [DataMember(Name = "bookingSystem")]
        public int BookingSystem { get; set; }
        /// <summary>Service connection settings.</summary>
        [DataMember(Name = "connection")]
        public ServiceConnection Connection { get; set; }
        /// <summary>Service options.</summary>
        [DataMember(Name = "options")]
        public ServiceOptions Options { get; set; }
        /// <summary>Service arguments.</summary>
        [DataMember(Name = "arguments")]

        public ServiceArguments Arguments { get; set; }

        /// <summary>
        /// Initialize SVS service configuration from JSON file path.
        /// </summary>
        /// <param name="configFilePath">JSON configuration file path.</param>
        public SvsServiceConfiguration(in string configFilePath)
        {
            Initialize(new ServiceConfiguration(configFilePath));
        }

        /// <summary>
        /// Constructor. Creates a new SVS service configuration out of the basic 
        /// configuration object.
        /// </summary>
        /// <param name="config">Basic configuration object.</param>
        public SvsServiceConfiguration(in ServiceConfiguration config)
        {
            Initialize(config);
        }

        /// <summary>
        /// Initialize SVS service configuration with an existing basic configuration.
        /// </summary>
        /// <param name="config">Service configuration.</param>
        private void Initialize(in ServiceConfiguration config)
        {
            Version = config.Version;
            BookingSystem = (int)config.BookingSystem;

            var con = new ServiceConnection();
            con.Address = config.Address;
            con.Port = config.Port;
            con.Timeout = config.Timeout;
            con.Username = config.Username;
            con.Password = config.Password;
            Connection = con;

            var opt = new ServiceOptions();
            opt.Currency = config.Currency;
            opt.Language = config.Language;
            Options = opt;

            var arg = new ServiceArguments();
            arg.MerchantName = config.Arguments.GetValue<string>("merchantName", "");
            arg.MerchantNumber = config.Arguments.GetValue<string>("merchantNumber", "");
            arg.RoutingId = config.Arguments.GetValue<string>("routingId", "");

            var cards = config.Arguments.GetSection("cards");

            arg.Cards = new List<ServiceCard>();

            foreach (var card in cards.GetChildren())
            {
                var serviceCard = new ServiceCard
                {
                    Range = card.GetValue<string>("range", "0-0"),
                    Type = card.GetValue<string>("type", ""),
                    MaxCharge = card.GetValue<decimal>("maxCharge", -1),
                    OnlyFullRedemption = card.GetValue<bool>("onlyFullRedemption", false),
                    PinPattern = card.GetValue<string>("pinPattern", ""),
                };
                arg.Cards.Add(serviceCard);
            }
            Arguments = arg;
        }

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
