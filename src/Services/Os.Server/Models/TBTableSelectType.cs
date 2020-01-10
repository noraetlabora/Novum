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
    /// Gets or Sets TBTableSelectType
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum TBTableSelectType
    {

        /// <summary>
        /// Enum CalculatorEnum for calculator
        /// </summary>
        [EnumMember(Value = "calculator")]
        CalculatorEnum = 1,

        /// <summary>
        /// Enum PaymentEnum for payment
        /// </summary>
        [EnumMember(Value = "payment")]
        PaymentEnum = 2,

        /// <summary>
        /// Enum ServiceAreaEnum for serviceArea
        /// </summary>
        [EnumMember(Value = "serviceArea")]
        ServiceAreaEnum = 3,

        /// <summary>
        /// Enum PrinterSelectionEnum for printerSelection
        /// </summary>
        [EnumMember(Value = "printerSelection")]
        PrinterSelectionEnum = 4,

        /// <summary>
        /// Enum ActionEnum for action
        /// </summary>
        [EnumMember(Value = "action")]
        ActionEnum = 5
    }
}