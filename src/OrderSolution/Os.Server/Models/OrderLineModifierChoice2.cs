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
    /// 
    /// </summary>
    [DataContract]
    public partial class OrderLineModifierChoice2 : IEquatable<OrderLineModifierChoice2>
    { 
        /// <summary>
        /// The ID of the choice that was selected.
        /// </summary>
        /// <value>The ID of the choice that was selected.</value>
        [DataMember(Name="modifierChoiceId")]
        public string ModifierChoiceId { get; set; }

        /// <summary>
        /// The amount how often the choices was selected. Defaults to 1 if omitted.
        /// </summary>
        /// <value>The amount how often the choices was selected. Defaults to 1 if omitted.</value>
        [DataMember(Name="amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderLineModifierChoice2 {\n");
            sb.Append("  ModifierChoiceId: ").Append(ModifierChoiceId).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((OrderLineModifierChoice2)obj);
        }

        /// <summary>
        /// Returns true if OrderLineModifierChoice2 instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderLineModifierChoice2 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderLineModifierChoice2 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    ModifierChoiceId == other.ModifierChoiceId ||
                    ModifierChoiceId != null &&
                    ModifierChoiceId.Equals(other.ModifierChoiceId)
                ) && 
                (
                    Amount == other.Amount ||
                    Amount != null &&
                    Amount.Equals(other.Amount)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (ModifierChoiceId != null)
                    hashCode = hashCode * 59 + ModifierChoiceId.GetHashCode();
                    if (Amount != null)
                    hashCode = hashCode * 59 + Amount.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(OrderLineModifierChoice2 left, OrderLineModifierChoice2 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderLineModifierChoice2 left, OrderLineModifierChoice2 right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}