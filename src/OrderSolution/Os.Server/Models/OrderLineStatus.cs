/*
 * OrderSolution HTTP API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Os.Server.Models
{
    /// <summary>
    /// Represents the status of a given order line. Unknown = 0, Ordered = 1, // This order line was not yet committed/confirmed Committed = 2, Paid = 3
    /// </summary>
    /// <value>Represents the status of a given order line. Unknown = 0, Ordered = 1, // This order line was not yet committed/confirmed Committed = 2, Paid = 3</value>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OrderLineStatus
    {

        /// <summary>
        /// Enum UnknownEnum for unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        UnknownEnum = 1,

        /// <summary>
        /// Enum OrderedEnum for ordered
        /// </summary>
        [EnumMember(Value = "ordered")]
        OrderedEnum = 2,

        /// <summary>
        /// Enum CommittedEnum for committed
        /// </summary>
        [EnumMember(Value = "committed")]
        CommittedEnum = 3,

        /// <summary>
        /// Enum PaidEnum for paid
        /// </summary>
        [EnumMember(Value = "paid")]
        PaidEnum = 4
    }
}
