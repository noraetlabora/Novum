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
        /// 0 – pickOne - One option to select from ModifierGroupChoices. 1 – pickMultiple - Select several options from ModifierGroupChoices. 2 – pickNumeric - Select numeric choices from ModifierGroupChoices 3 – textInput -  Asks for arbitary text input 4 – faxInput - Asks for handwritten input
        /// </summary>
        /// <value>0 – pickOne - One option to select from ModifierGroupChoices. 1 – pickMultiple - Select several options from ModifierGroupChoices. 2 – pickNumeric - Select numeric choices from ModifierGroupChoices 3 – textInput -  Asks for arbitary text input 4 – faxInput - Asks for handwritten input</value>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum ModifierType
        {
            
            /// <summary>
            /// Enum PickOneEnum for pickOne
            /// </summary>
            [EnumMember(Value = "pickOne")]
            PickOneEnum = 0,
            
            /// <summary>
            /// Enum PickMultipleEnum for pickMultiple
            /// </summary>
            [EnumMember(Value = "pickMultiple")]
            PickMultipleEnum = 1,
            
            /// <summary>
            /// Enum PickNumericEnum for pickNumeric
            /// </summary>
            [EnumMember(Value = "pickNumeric")]
            PickNumericEnum = 2,
            
            /// <summary>
            /// Enum TextInputEnum for textInput
            /// </summary>
            [EnumMember(Value = "textInput")]
            TextInputEnum = 3,
            
            /// <summary>
            /// Enum FaxInputEnum for faxInput
            /// </summary>
            [EnumMember(Value = "faxInput")]
            FaxInputEnum = 4
        }
}
