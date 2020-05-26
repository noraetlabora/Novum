using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace Nt.Util
{
    /// <summary>
    /// Main configuration of the current server settings.
    /// </summary>
    public class Configuration
    {
        /// <summary>Stores loaded config file information.</summary>
        protected IConfigurationRoot Data { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Configuration() { }

        /// <summary>
        /// Constructor. Base configuration of the server settings.
        /// </summary>
        /// <param name="configFilePath">Path to the configuration file.</param>
        public Configuration(in string configFilePath) : this()
        {
            Load(configFilePath);
        }

        /// <summary>
        /// Create configuration with the data of another configuration.
        /// </summary>
        /// <param name="config">New configuration.</param>
        public Configuration(in Configuration config)
        {
            Data = config.Data;
        }

        /// <summary>
        /// Load configuration file from a defined file path.
        /// Supported file formats are .ini, .json, .xml.
        /// </summary>
        /// <param name="configFilePath">Path to the configuration file.</param>
        public void Load(in string configFilePath)
        {
            var fInfo = new FileInfo(configFilePath);

            if (!fInfo.Exists)
                throw new FileNotFoundException(string.Format("File '{0}' does not exist.", configFilePath));

            var builder = new ConfigurationBuilder();
            var extension = fInfo.Extension.ToLower();
            switch(extension)
            {
                //case ".ini":
                //    builder.AddIniFile(configFilePath, optional: false, reloadOnChange: true);
                //    break;
                case ".json":
                    builder.AddJsonFile(configFilePath, optional: false, reloadOnChange: true);
                    break;
                //case ".xml":
                //    builder.AddXmlFile(configFilePath, optional: false, reloadOnChange: true);
                //    break;
                default:
                    throw new ArgumentException(string.Format("Unsupported file format '{0}'.", configFilePath));

            }
            Data = builder.Build();
        }
    }
}
