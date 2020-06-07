using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Text.Json;

namespace Nt.Booking
{
    /// <summary>
    /// Main configuration of the current service settings.
    /// </summary>
    public class ServiceConfiguration : Nt.Util.Configuration
    {
        /// <summary>Declares the type of the booking system.</summary>
        public NtBooking.BookingSystemType BookingSystem
        {
            get { return (Nt.Booking.NtBooking.BookingSystemType)Data.GetValue<int>("bookingSystem", -1); }
            set { Data.GetSection("bookingSystem").Value = value.ToString(); }
        }

        /// <summary>Port where the Nt.Booking Service is listening.</summary>
        public int Port
        {
            get { return Data.GetSection("connection").GetValue<int>("port", 0); }
            set { Data.GetSection("connection").GetSection("port").Value = value.ToString(); }
        }

        /// <summary>Address (Ip:Port, Uri, ..) of the booking system like "192.168.0.1:4000", "https://server.com", ...</summary>
        public string Address
        {
            get { return Data.GetSection("connection").GetValue<string>("address", null); }
            set { Data.GetSection("connection").GetSection("address").Value = value; }
        }

        /// <summary>Username needed for booking system.</summary>
        public string Username
        {
            get { return Data.GetSection("connection").GetValue<string>("username", null); }
            set { Data.GetSection("connection").GetSection("username").Value = value; }
        }
        /// <summary>Password needed for booking system.</summary>
        public string Password
        {
            get { return Data.GetSection("connection").GetValue<string>("password", null); }
            set { Data.GetSection("connection").GetSection("password").Value = value; }
        }
        /// <summary>Timeout in seconds.</summary>
        public int Timeout
        {
            get { return Data.GetSection("connection").GetValue<int>("timeout", 10); }
            set { Data.GetSection("connection").GetSection("timeout").Value = value.ToString(); }
        }
        /// <summary>Currency in ISO 4217 (EUR/CHF/GBP/USD/JPY/CNY/...)</summary>
        public string Currency
        {
            get { return Data.GetSection("options").GetValue<string>("currency", "EUR"); }
            set { Data.GetSection("options").GetSection("currency").Value = value; }
        }
        /// <summary>Holds additional arguments of the current configuration.</summary>
        public IConfigurationSection Arguments
        {
            get
            {
                return Data.GetSection("arguments");
            }
        }
        /// <summary>Language code, e.g. de-AT.</summary>
        public string Language
        {
            get { return Data.GetSection("options").GetValue<string>("language", "de-AT"); }
            set { Data.GetSection("options").GetSection("language").Value = value; }
        }
        /// <summary>Server configuration version number.</summary>
        public string Version
        {
            get { return Data.GetValue<string>("version", "0.0"); }
            set { Data.GetSection("version").Value = value; }
        }

        /// <summary>
        /// Default constructor. Base configuration of the server settings.
        /// </summary>
        public ServiceConfiguration() : base() { }

        /// <summary>
        /// Constructor. Base configuration of the server settings.
        /// </summary>
        /// <param name="configFilePath">Path to the configuration file.</param>
        public ServiceConfiguration(in string configFilePath) : base(configFilePath) { }

        /// <summary>
        /// Constructor. Base configuration of the server settings.
        /// </summary>
        /// <param name="config">New server configuration.</param>
        public ServiceConfiguration(in Nt.Util.Configuration config) : base(config) { }
    }
}
