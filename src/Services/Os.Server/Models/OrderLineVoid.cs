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
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class OrderLineVoid : IEquatable<OrderLineVoid>
    {
        /// <summary>
        /// Defines the quantity that is voided from an existing orderline. If the quantity is 0 nothing is changed. If the quantity is &amp;gt;&#x3D; the quantity of the orderline the whole orderline will be voided.
        /// </summary>
        /// <value>Defines the quantity that is voided from an existing orderline. If the quantity is 0 nothing is changed. If the quantity is &amp;gt;&#x3D; the quantity of the orderline the whole orderline will be voided.</value>
        [DataMember(Name = "quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Defines the cancellation reason that must be provided if a already committed orderline should be voided.
        /// </summary>
        /// <value>Defines the cancellation reason that must be provided if a already committed orderline should be voided.</value>
        [DataMember(Name = "cancellationReasonId")]
        public string CancellationReasonId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderLineVoid {\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
            sb.Append("  CancellationReasonId: ").Append(CancellationReasonId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OrderLineVoid)obj);
        }

        /// <summary>
        /// Returns true if OrderLineVoid instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderLineVoid to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderLineVoid other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Quantity == other.Quantity ||
                    Quantity != null &&
                    Quantity.Equals(other.Quantity)
                ) &&
                (
                    CancellationReasonId == other.CancellationReasonId ||
                    CancellationReasonId != null &&
                    CancellationReasonId.Equals(other.CancellationReasonId)
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
                if (Quantity != null)
                    hashCode = hashCode * 59 + Quantity.GetHashCode();
                if (CancellationReasonId != null)
                    hashCode = hashCode * 59 + CancellationReasonId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(OrderLineVoid left, OrderLineVoid right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderLineVoid left, OrderLineVoid right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}