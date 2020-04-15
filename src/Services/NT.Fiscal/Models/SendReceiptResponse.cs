using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Fiscal.Models
{
    /// <summary>
    /// This object contains all information of a booking (payment/cancellation)
    /// </summary>
    [DataContract]
    public partial class SendReceiptResponse : IEquatable<SendReceiptResponse>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets DisplayMessage
        /// </summary>
        [DataMember(Name = "DisplayMessage")]
        public string DisplayMessage { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SendReceiptResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  DisplayMessage: ").Append(DisplayMessage).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
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
            return obj.GetType() == GetType() && Equals((SendReceiptResponse)obj);
        }

        /// <summary>
        /// Returns true if SendReceiptResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of SendReceiptResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SendReceiptResponse other)
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
                    DisplayMessage == other.DisplayMessage ||
                    DisplayMessage != null &&
                    DisplayMessage.Equals(other.DisplayMessage)
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
                if (DisplayMessage != null)
                    hashCode = hashCode * 59 + DisplayMessage.GetHashCode();
                return hashCode;
            }
        }

        #region Operators

#pragma warning disable 1591

        public static bool operator ==(SendReceiptResponse left, SendReceiptResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SendReceiptResponse left, SendReceiptResponse right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591

        #endregion Operators
    }
}