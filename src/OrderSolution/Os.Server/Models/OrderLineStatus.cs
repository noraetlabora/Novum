/*
 * OrderSolution HTTP API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Os.Server.Models
{ 
        /// <summary>
        /// Represents the status of a given Orderline. Unknown = 0, Ordered = 1, // This orderline was not yet commited/confirmed Committed = 2, Paid = 3
        /// </summary>
        /// <value>Represents the status of a given Orderline. Unknown = 0, Ordered = 1, // This orderline was not yet commited/confirmed Committed = 2, Paid = 3</value>
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
