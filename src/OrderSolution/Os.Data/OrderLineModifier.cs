/*
 * OrderSolution HTTP API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.9.6
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
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Os.Data
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class OrderLineModifier : IEquatable<OrderLineModifier>
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets ModifierGroupId
        /// </summary>
        [DataMember(Name="modifierGroupId")]
        public string ModifierGroupId { get; set; }

        /// <summary>
        /// Gets or Sets TextInput
        /// </summary>
        [DataMember(Name="textInput")]
        public string TextInput { get; set; }

        /// <summary>
        /// The ID of the FaxInput image. Can be a number, url, ... The format if this ID is defined by the POS storage behavior. TODO: When the FaxInput storage API is defined we may have to update this.
        /// </summary>
        /// <value>The ID of the FaxInput image. Can be a number, url, ... The format if this ID is defined by the POS storage behavior. TODO: When the FaxInput storage API is defined we may have to update this.</value>
        [DataMember(Name="faxInputID")]
        public string FaxInputID { get; set; }

        /// <summary>
        /// Gets or Sets Choices
        /// </summary>
        [DataMember(Name="choices")]
        public List<OrderLineModifierChoice> Choices { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderLineModifier {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OrderLineModifier)obj);
        }

        /// <summary>
        /// Returns true if OrderLineModifier instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderLineModifier to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderLineModifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
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
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
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

        public static bool operator ==(OrderLineModifier left, OrderLineModifier right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderLineModifier left, OrderLineModifier right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}