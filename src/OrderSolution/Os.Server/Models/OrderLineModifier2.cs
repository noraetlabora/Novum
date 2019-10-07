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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class OrderLineModifier2 : IEquatable<OrderLineModifier2>
    {
        /// <summary>
        /// The ID of the modifier group this modifiers belong to. This must be the group ID the choices belong to.
        /// </summary>
        /// <value>The ID of the modifier group this modifiers belong to. This must be the group ID the choices belong to.</value>
        [DataMember(Name = "modifierGroupId")]
        public string ModifierGroupId { get; set; }

        /// <summary>
        /// The entered text in case the modifier group is of type free text entry.
        /// </summary>
        /// <value>The entered text in case the modifier group is of type free text entry.</value>
        [DataMember(Name = "textInput")]
        public string TextInput { get; set; }

        /// <summary>
        /// The ID of the FaxInput image in case the modifier group is of type fax. Can be a number, URL, ... The format if this ID is defined by the POS storage behavior. TODO: When the FaxInput storage API is defined we may have to update this.
        /// </summary>
        /// <value>The ID of the FaxInput image in case the modifier group is of type fax. Can be a number, URL, ... The format if this ID is defined by the POS storage behavior. TODO: When the FaxInput storage API is defined we may have to update this.</value>
        [DataMember(Name = "faxInputID")]
        public string FaxInputID { get; set; }

        /// <summary>
        /// The selected choices in case modifier group is of type PickOne, PickMultiple, PickNumeric, Pick...
        /// </summary>
        /// <value>The selected choices in case modifier group is of type PickOne, PickMultiple, PickNumeric, Pick...</value>
        [DataMember(Name = "choices")]
        public List<OrderLineModifierChoice2> Choices { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderLineModifier2 {\n");
            sb.Append("  ModifierGroupId: ").Append(ModifierGroupId).Append("\n");
            sb.Append("  TextInput: ").Append(TextInput).Append("\n");
            sb.Append("  FaxInputID: ").Append(FaxInputID).Append("\n");
            sb.Append("  Choices: ").Append(Choices).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OrderLineModifier2)obj);
        }

        /// <summary>
        /// Returns true if OrderLineModifier2 instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderLineModifier2 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderLineModifier2 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ModifierGroupId == other.ModifierGroupId ||
                    ModifierGroupId != null &&
                    ModifierGroupId.Equals(other.ModifierGroupId)
                ) &&
                (
                    TextInput == other.TextInput ||
                    TextInput != null &&
                    TextInput.Equals(other.TextInput)
                ) &&
                (
                    FaxInputID == other.FaxInputID ||
                    FaxInputID != null &&
                    FaxInputID.Equals(other.FaxInputID)
                ) &&
                (
                    Choices == other.Choices ||
                    Choices != null &&
                    Choices.SequenceEqual(other.Choices)
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
                if (ModifierGroupId != null)
                    hashCode = hashCode * 59 + ModifierGroupId.GetHashCode();
                if (TextInput != null)
                    hashCode = hashCode * 59 + TextInput.GetHashCode();
                if (FaxInputID != null)
                    hashCode = hashCode * 59 + FaxInputID.GetHashCode();
                if (Choices != null)
                    hashCode = hashCode * 59 + Choices.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(OrderLineModifier2 left, OrderLineModifier2 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderLineModifier2 left, OrderLineModifier2 right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
