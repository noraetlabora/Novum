using System.Collections.Generic;
using Nt.Booking.Utils;
namespace Nt.Booking
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerConfiguration
    {
        /// <summary>Declares the type of the booking system.</summary>
        public NtBooking.BookingSystemType BookingSystem { get; set; }

        /// <summary>Port where the Nt.Booking Service is listening.</summary>
        public int Port { get; set; }

        /// <summary>Address (Ip:Port, Uri, ..) of the Bookingsystem like "192.168.0.1:4000", "https://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay", ...</summary>
        public string Address { get; set; }

        /// <summary>Username needed for booking system.</summary>
        public string Username { get; set; }

        /// <summary>Password needed for booking system.</summary>
        public string Password { get; set; }

        /// <summary>Timeout in seconds.</summary>
        public int Timeout { get; set; }

        /// <summary>Currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)</summary>
        public string Currency { get; set; }

        /// <summary></summary>
        public Dictionary<string, string> Arguments { get; set; }

        /// <summary>Language code, e.g. de-AT.</summary>
        public string Language { get; set; }

        /// <summary>Server configuration version number.</summary>
        public string Version { get; set; }

        /// <summary>
        /// Constructor. Base configuration of the server settings.
        /// </summary>
        public ServerConfiguration()
        {
            Port = 4711;
            Timeout = 10;
            Language = "de-AT";
        }

        public ServerConfiguration(in string iniFilePath) : this()
        {
            Load(iniFilePath);
        }

        public void Save(in string iniFilePath)
        {
            var ini = new IniFileHandler();
            ini.SetValueOrDefault<string>("version", Version);
            ini.SetValueOrDefault<string>("connection:address", Address);
            ini.SetValueOrDefault<int>("connection:port", Port);
            ini.SetValueOrDefault<string>("connection:username", Username);
            ini.SetValueOrDefault<string>("connection:password", Password);
            ini.SetValueOrDefault<int>("connection:timeout", Timeout);
            ini.SetValueOrDefault<string>("options:currency", Currency);
            ini.SetValueOrDefault<string>("options:language", Language);
            ini.SetValueOrDefault<string>("arguments:routingId", Arguments.GetValueOrDefault("routingId", ""));
            ini.SetValueOrDefault<string>("arguments:merchantNumber", Arguments.GetValueOrDefault("merchantNumber", ""));
            ini.SetValueOrDefault<string>("arguments:merchantName", Arguments.GetValueOrDefault("merchantName", ""));
            ini.Save(iniFilePath);
        }

        public void Load(in string iniFilePath)
        {
            var ini = new IniFileHandler(iniFilePath);
            this.Version = ini.GetValueOrDefault<string>("version");
            this.Address = ini.GetValueOrDefault<string>("connection:address");
            this.Port = ini.GetValueOrDefault<int>("connection:port", 4711);
            this.Username = ini.GetValueOrDefault<string>("connection:username");
            this.Password = ini.GetValueOrDefault<string>("connection:password");
            this.Timeout = ini.GetValueOrDefault<int>("connection:timeout", 10);

            this.Currency = ini.GetValueOrDefault<string>("options:currency", "EUR");
            this.Language = ini.GetValueOrDefault<string>("options:language", "de-AT");

            string routingId = ini.GetValueOrDefault<string>("arguments:routingId");
            string merchantNumber = ini.GetValueOrDefault<string>("arguments:merchantNumber");
            string merchantName = ini.GetValueOrDefault<string>("arguments:merchantName");

            Arguments = new Dictionary<string, string>();
            if (routingId != null)
                Arguments.Add("routingId", routingId);
            if (merchantNumber != null)
                Arguments.Add("merchantNumber", merchantNumber);
            if (merchantName != null)
                Arguments.Add("merchantName", merchantName);
        }
        // public void LoadJson(in string jsonFilePath)
        // {
        //     if (!File.Exists(jsonFilePath))
        //         throw new FileNotFoundException(Resources.Dictionary.GetString("FileNotFound"));

        //     using (var streamReader = new StreamReader(jsonFilePath))
        //     {
        //         var options = new JsonSerializerOptions()
        //         {
        //             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //             WriteIndented = true
        //         };
        //         string json = streamReader.ReadToEnd();

        //         var tmp = JsonSerializer.Deserialize<ServerConfiguration>(json, options);
        //         this.Address = tmp.Address;
        //         this.Port = tmp.Port;
        //         this.Username = tmp.Username;
        //         this.Password = tmp.Password;
        //         this.Currency = tmp.Currency;
        //         this.Timeout = tmp.Timeout;
        //         this.Arguments = tmp.Arguments;
        //     }
        // }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("ServerConfiguration: ");
            builder.Append(" BookingSystem = ").Append(BookingSystem);
            builder.Append(" Port =").Append(Port);
            return builder.ToString();
        }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
