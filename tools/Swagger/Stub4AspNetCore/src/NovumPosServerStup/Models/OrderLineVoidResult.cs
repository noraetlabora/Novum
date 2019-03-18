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
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NovumPosServerStup.Models
{ 
    /// <summary>
    /// Provides information about the new status of the orderline after void processing.
    /// </summary>
    [DataContract]
    public partial class OrderLineVoidResult : IEquatable<OrderLineVoidResult>
    { 
        /// <summary>
        /// The ID of the orderline that a void operation was executed on.
        /// </summary>
        /// <value>The ID of the orderline that a void operation was executed on.</value>
        [DataMember(Name="orderLineId")]
        public string OrderLineId { get; set; }

        /// <summary>
        /// The single price of the orderline. This will typically be the same as before but it might change in case of volumen discounts.
        /// </summary>
        /// <value>The single price of the orderline. This will typically be the same as before but it might change in case of volumen discounts.</value>
        [Required]
        [DataMember(Name="singlePrice")]
        public int? SinglePrice { get; set; }

        /// <summary>
        /// The new quantiy of this orderline.
        /// </summary>
        /// <value>The new quantiy of this orderline.</value>
        [Required]
        [DataMember(Name="quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderLineVoidResult {\n");
            sb.Append("  OrderLineId: ").Append(OrderLineId).Append("\n");
            sb.Append("  SinglePrice: ").Append(SinglePrice).Append("\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OrderLineVoidResult)obj);
        }

        /// <summary>
        /// Returns true if OrderLineVoidResult instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderLineVoidResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderLineVoidResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    OrderLineId == other.OrderLineId ||
                    OrderLineId != null &&
                    OrderLineId.Equals(other.OrderLineId)
                ) && 
                (
                    SinglePrice == other.SinglePrice ||
                    SinglePrice != null &&
                    SinglePrice.Equals(other.SinglePrice)
                ) && 
                (
                    Quantity == other.Quantity ||
                    Quantity != null &&
                    Quantity.Equals(other.Quantity)
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
                    if (OrderLineId != null)
                    hashCode = hashCode * 59 + OrderLineId.GetHashCode();
                    if (SinglePrice != null)
                    hashCode = hashCode * 59 + SinglePrice.GetHashCode();
                    if (Quantity != null)
                    hashCode = hashCode * 59 + Quantity.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(OrderLineVoidResult left, OrderLineVoidResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderLineVoidResult left, OrderLineVoidResult right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
