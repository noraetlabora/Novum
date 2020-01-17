/*
 * NT.Booking
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 0.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// debit object
    /// </summary>
    [DataContract]
    public partial class Debit : IEquatable<Debit>
    {
        /// <summary>
        /// debt amount/debt level (*100)
        /// </summary>
        /// <value>debt amount/debt level (*100)</value>
        [DataMember(Name = "amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// debt limit (*100)
        /// </summary>
        /// <value>debt limit (*100)</value>
        [DataMember(Name = "limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// warning of debt limit (*100)
        /// </summary>
        /// <value>warning of debt limit (*100)</value>
        [DataMember(Name = "limitWarning")]
        public int? LimitWarning { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Debit {\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Limit: ").Append(Limit).Append("\n");
            sb.Append("  LimitWarning: ").Append(LimitWarning).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            //return JsonConvert.SerializeObject(this, Formatting.Indented);
            return System.Text.Json.JsonSerializer.Serialize(this);
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
            return obj.GetType() == GetType() && Equals((Debit)obj);
        }

        /// <summary>
        /// Returns true if Debit instances are equal
        /// </summary>
        /// <param name="other">Instance of Debit to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Debit other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Amount == other.Amount ||
                    Amount != null &&
                    Amount.Equals(other.Amount)
                ) &&
                (
                    Limit == other.Limit ||
                    Limit != null &&
                    Limit.Equals(other.Limit)
                ) &&
                (
                    LimitWarning == other.LimitWarning ||
                    LimitWarning != null &&
                    LimitWarning.Equals(other.LimitWarning)
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
                if (Amount != null)
                    hashCode = hashCode * 59 + Amount.GetHashCode();
                if (Limit != null)
                    hashCode = hashCode * 59 + Limit.GetHashCode();
                if (LimitWarning != null)
                    hashCode = hashCode * 59 + LimitWarning.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Debit left, Debit right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Debit left, Debit right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
