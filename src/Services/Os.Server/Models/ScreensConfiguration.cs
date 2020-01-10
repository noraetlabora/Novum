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
    public partial class ScreensConfiguration : IEquatable<ScreensConfiguration>
    {
        /// <summary>
        /// Configuration of the tableSeleciton screen.
        /// </summary>
        /// <value>Configuration of the tableSeleciton screen.</value>
        [DataMember(Name = "tableSelection")]
        public Object TableSelection { get; set; }

        /// <summary>
        /// Configuration of the order screen.
        /// </summary>
        /// <value>Configuration of the order screen.</value>
        [DataMember(Name = "order")]
        public Object Order { get; set; }

        /// <summary>
        /// Configuration of the payment screen.
        /// </summary>
        /// <value>Configuration of the payment screen.</value>
        [DataMember(Name = "payment")]
        public Object Payment { get; set; }

        /// <summary>
        /// Configuration of the paymentMedia screen.
        /// </summary>
        /// <value>Configuration of the paymentMedia screen.</value>
        [DataMember(Name = "paymentMedia")]
        public Object PaymentMedia { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ScreensConfiguration {\n");
            sb.Append("  TableSelection: ").Append(TableSelection).Append("\n");
            sb.Append("  Order: ").Append(Order).Append("\n");
            sb.Append("  Payment: ").Append(Payment).Append("\n");
            sb.Append("  PaymentMedia: ").Append(PaymentMedia).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ScreensConfiguration)obj);
        }

        /// <summary>
        /// Returns true if ScreensConfiguration instances are equal
        /// </summary>
        /// <param name="other">Instance of ScreensConfiguration to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ScreensConfiguration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TableSelection == other.TableSelection ||
                    TableSelection != null &&
                    TableSelection.Equals(other.TableSelection)
                ) &&
                (
                    Order == other.Order ||
                    Order != null &&
                    Order.Equals(other.Order)
                ) &&
                (
                    Payment == other.Payment ||
                    Payment != null &&
                    Payment.Equals(other.Payment)
                ) &&
                (
                    PaymentMedia == other.PaymentMedia ||
                    PaymentMedia != null &&
                    PaymentMedia.Equals(other.PaymentMedia)
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
                if (TableSelection != null)
                    hashCode = hashCode * 59 + TableSelection.GetHashCode();
                if (Order != null)
                    hashCode = hashCode * 59 + Order.GetHashCode();
                if (Payment != null)
                    hashCode = hashCode * 59 + Payment.GetHashCode();
                if (PaymentMedia != null)
                    hashCode = hashCode * 59 + PaymentMedia.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ScreensConfiguration left, ScreensConfiguration right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ScreensConfiguration left, ScreensConfiguration right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}