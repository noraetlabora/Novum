using System.Collections.Generic;
using Nt.Booking.Utils;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace Nt.Booking
{
    /// <summary>
    /// Main configuration of the current server settings.
    /// </summary>
    public class ServerConfiguration
    {
        /// <summary>Declares the type of the booking system.</summary>
        public NtBooking.BookingSystemType BookingSystem
        {
            get { return (Nt.Booking.NtBooking.BookingSystemType)_config.GetValue<int>("bookingSystem", -1); }
            set { _config.GetSection("bookingSystem").Value = value.ToString(); }
        }
        /// <summary>Port where the Nt.Booking Service is listening.</summary>
        public int Port 
        { 
            get { return _config.GetSection("connection").GetValue<int>("port", 0); } 
            set { _config.GetSection("connection").GetSection("port").Value = value.ToString(); }
        }

        /// <summary>Address (Ip:Port, Uri, ..) of the Bookingsystem like "192.168.0.1:4000", "https://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay", ...</summary>
        public string Address 
        { 
            get { return _config.GetSection("connection").GetValue<string>("address", null);  }
            set { _config.GetSection("connection").GetSection("address").Value = value; } 
        }

        /// <summary>Username needed for booking system.</summary>
        public string Username
        {
            get { return _config.GetSection("connection").GetValue<string>("username", null); }
            set { _config.GetSection("connection").GetSection("username").Value = value; }
        }
        /// <summary>Password needed for booking system.</summary>
        public string Password
        {
            get { return _config.GetSection("connection").GetValue<string>("password", null); }
            set { _config.GetSection("connection").GetSection("password").Value = value; }
        }
        /// <summary>Timeout in seconds.</summary>
        public int Timeout
        {
            get { return _config.GetSection("connection").GetValue<int>("timeout", 10); }
            set { _config.GetSection("connection").GetSection("timeout").Value = value.ToString(); }
        }
        /// <summary>Currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)</summary>
        public string Currency
        {
            get { return _config.GetSection("options").GetValue<string>("currency", "EUR"); }
            set { _config.GetSection("options").GetSection("currency").Value = value; }
        }
        /// <summary>Holds additional arguments of the current configuration.</summary>
        public Dictionary<string, string> Arguments
        {
            get 
            {
                var arg = new Dictionary<string, string>();
                foreach (var item in _config.GetSection("arguments").GetChildren())
                {
                    arg.Add(item.Key, item.Value);
                }
                return arg;
            }
            set 
            { 
                foreach(var arg in value)
                {
                    _config.GetSection("arguments").GetSection(arg.Key).Value = arg.Value; 
                }
            }
        }
        /// <summary>Language code, e.g. de-AT.</summary>
        public string Language
        {
            get { return _config.GetSection("options").GetValue<string>("language", "de-AT"); }
            set { _config.GetSection("options").GetSection("language").Value = value; }
        }
        /// <summary>Server configuration version number.</summary>
        public string Version
        {
            get { return _config.GetValue<string>("version", "0.0"); }
            set { _config.GetSection("version").Value = value; }
        }
        /// <summary>Stores loaded config file information.</summary>
        private IConfigurationRoot _config = null;

        /// <summary>
        /// Constructor. Base configuration of the server settings.
        /// </summary>
        /// <param name="configFilePath">Path to the configuration file.</param>
        public ServerConfiguration(in string configFilePath)
        {          
            Load(configFilePath);
        }

        /// <summary>
        /// Load configuration file from a defined file path.
        /// </summary>
        /// <param name="configFilePath">Path to the configuration file.</param>
        public void Load(in string configFilePath)
        {
            var fInfo = new FileInfo(configFilePath);

            if (!fInfo.Exists)
                throw new FileNotFoundException();

            var builder = new ConfigurationBuilder();

            if(fInfo.Extension.ToLower() == ".json")
            {
                builder.AddJsonFile(configFilePath, optional: false, reloadOnChange: false);
            }
            else
            {
                throw new ArgumentException();
            }

            _config = builder.Build();
        }

        /// <summary>
        /// Convert data structure to string.
        /// </summary>
        /// <returns>String of currenct configuration.</returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("ServerConfiguration: ");
            builder.Append(" BookingSystem = ").Append(BookingSystem);
            builder.Append(" Port =").Append(Port);
            return builder.ToString();
        }
    }
}
