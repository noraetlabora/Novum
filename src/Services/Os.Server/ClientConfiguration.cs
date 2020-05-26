namespace Os.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientConfiguration
    {

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Localization { get; set; }
        public string PriceEntryMode { get; set; }
        public bool DisableSubtables { get; set; }
        public string AuthenthicationMode { get; set; }
        public bool Coursing { get; set; }
        public bool FeatureMoveAllSubTables { get; set; }
        public bool FeatureMoveSingleSubTable { get; set; }
        public bool FeatureTip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ClientConfiguration()
        {
            Localization = "de-AT";
            PriceEntryMode = "0";
            DisableSubtables = false;
            AuthenthicationMode = "number";
            FeatureMoveAllSubTables = true;
            FeatureMoveSingleSubTable = true;
            FeatureTip = true;
            Coursing = true;
        }

        public ClientConfiguration(string json)
        {

        }

        /// <summary>
        /// Language Code ISO 639-1
        /// </summary>
        public string GetLanguageCode()
        {
            return Localization.Substring(0, 2);
        }

        /// <summary>
        /// CountryCode ISO 3166-2
        /// </summary>
        public string GetCountryCode()
        {
            return Localization.Substring(3, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("OsClientConfiguration: localization=").Append(this.Localization);
            builder.Append(", priceEntryMode=").Append(this.PriceEntryMode);
            builder.Append(", disableSubTables=").Append(this.DisableSubtables);
            builder.Append(", authenticationMode=").Append(this.AuthenthicationMode);
            builder.Append(", moveAllSubTables=").Append(this.FeatureMoveAllSubTables);
            builder.Append(", moveSingleSubTables=").Append(this.FeatureMoveSingleSubTable);
            builder.Append(", tip=").Append(this.FeatureTip);
            builder.Append(", coursing = ").Append(this.Coursing);

            return builder.ToString();
        }
    }
}
