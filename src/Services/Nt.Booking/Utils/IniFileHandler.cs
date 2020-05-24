using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Ini;

namespace Nt.Booking.Utils
{
    /// <summary>
    /// The IniFileHandler is used to handle the input and output of INI files.
    /// You can load INI files from a specified file path, modify entries and save
    /// the changes to a defined output file.
    /// 
    /// Base structure of a INI file.
    /// 
    /// [section]
    /// key=value
    /// 
    /// For more information see https://en.wikipedia.org/wiki/INI_file
    /// </summary>
    public class IniFileHandler : IniConfigurationProvider
    {
        /// <summary>
        /// Default constructor. Creates an empty INI data structure.
        /// </summary>
        public IniFileHandler() : base(new IniConfigurationSource()) { }

        /// <summary>
        /// Constructor to load INI file data from a specified file path.
        /// </summary>
        /// <param name="iniFilePath">File path to the INI file.</param>
        public IniFileHandler(in string iniFilePath) : this()
        {
            Load(iniFilePath);
        }

        /// <summary>
        /// Get the main data key identifier.
        /// </summary>
        /// <returns>INI data key identifier.</returns>
        public ICollection<string> GetKeys() 
        {
            return Data.Keys;
        }

        /// <summary>
        /// Get value from the INI data structure. If value does not exists the default value will be returned.
        /// </summary>
        /// <param name="key">Key that points to a value.</param>
        /// <returns>The value that belongs to a specified key. If it does not exist, the default value will be returned.</returns>
        public T GetValueOrDefault<T>(in string key, in T defaultVal = default(T))
        {
            string val;
            if (TryGet(key, out val))
            {
                return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(val);
            }
            return defaultVal;
        }

        /// <summary>
        /// Set value as string to the INI data structure. If value is null, the default value will be taken.
        /// </summary>
        /// <param name="key">Key in the following format: section:key. Section is declared as [section] in the INI file.</param>
        /// <param name="value">Value that belongs to that key.</param>
        /// <param name="defaultVal">Default value that is set, if value is null.</param>
        public void SetValueOrDefault<T>(in string key, in T value, in string defaultVal = "")
        {
            string convertedValue = value == null ? default : value.ToString();
            Set(key, convertedValue);
        }

        /// <summary>
        /// Save INI data to a specified file.
        /// </summary>
        /// <param name="iniFilePath">Output file path. If exists, it will be overwritten.</param>
        public void Save(in string iniFilePath)
        {
            if (iniFilePath == null || iniFilePath.Length == 0)
            {
                throw new ArgumentNullException("iniFilePath");
            }

            using (var writer = File.CreateText(iniFilePath))
            {
                var data = new Dictionary<string, Dictionary<string, string>>();
                foreach (KeyValuePair<string, string> entry in Data)
                {
                    int index = entry.Key.IndexOf(':');
                    string key = entry.Key;
                    string section = "";
                    if (index != 0)
                    {
                        section = entry.Key.Substring(0, index);
                        key = entry.Key.Substring(index + 1, entry.Key.Length - index - 1);
                    }
                    if (data.ContainsKey(section))
                    {
                        data[section].Add(key, entry.Value);
                    }
                    else
                    {
                        data.Add(section, new Dictionary<string, string>() { [key] = entry.Value });
                    }
                }

                string lastSection = "";
                foreach (KeyValuePair<string, Dictionary<string, string>> entry in data)
                {
                    if (entry.Key != lastSection)
                    {
                        lastSection = entry.Key;

                        if (lastSection != "")
                        {
                            writer.WriteLine("[" + lastSection + "]");
                        }
                    }
                    foreach (KeyValuePair<string, string> line in entry.Value)
                    {
                        writer.WriteLine(line.Key + "=" + line.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Load INI file from a specified file path.
        /// </summary>
        /// <param name="iniFilePath">Path of the INI file.</param>
        public void Load(in string iniFilePath)
        {
            if (!File.Exists(iniFilePath))
            {
                throw new FileNotFoundException();
            }
            using (var fileStream = new FileStream(iniFilePath, FileMode.Open))
            {
                Load(fileStream);
            }
        }
    }
}