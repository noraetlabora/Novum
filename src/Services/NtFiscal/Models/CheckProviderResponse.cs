using System.Runtime.Serialization;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nt.Fiscal.Systems.Enums;

namespace Nt.Fiscal.Models
{
    public partial class CheckProviderResponse
    {
        /// <summary>
        /// Status code 
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Status text 
        /// </summary>
        [DataMember(Name = "statustext")]
        public string StatusText { get; set; }

        /// <summary>
        /// Configured provider
        /// </summary>
        [DataMember(Name = "provider")]
        public string Provider { get; set; }

        /// <summary>
        /// Configured country
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Is currently in transaction
        /// </summary>
        [DataMember(Name = "inTransaction")]
        public bool InTransaction { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CheckProviderResponse {\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Status text: ").Append(StatusText).Append("\n");
            sb.Append("  Provider: ").Append(Provider).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  InTransaction: ").Append(InTransaction).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

    }
}
