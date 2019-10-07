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
    /// Holds the track data of an MSR read action. NOTE: Will only contain the tracks that were enabled in the MsrInput definition.
    /// </summary>
    [DataContract]
    public partial class MsrData : IEquatable<MsrData>
    {
        /// <summary>
        /// Gets or Sets Track1
        /// </summary>
        [DataMember(Name = "track1")]
        public string Track1 { get; set; }

        /// <summary>
        /// Gets or Sets Track2
        /// </summary>
        [DataMember(Name = "track2")]
        public string Track2 { get; set; }

        /// <summary>
        /// Gets or Sets Track3
        /// </summary>
        [DataMember(Name = "track3")]
        public string Track3 { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MsrData {\n");
            sb.Append("  Track1: ").Append(Track1).Append("\n");
            sb.Append("  Track2: ").Append(Track2).Append("\n");
            sb.Append("  Track3: ").Append(Track3).Append("\n");
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
            return obj.GetType() == GetType() && Equals((MsrData)obj);
        }

        /// <summary>
        /// Returns true if MsrData instances are equal
        /// </summary>
        /// <param name="other">Instance of MsrData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MsrData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Track1 == other.Track1 ||
                    Track1 != null &&
                    Track1.Equals(other.Track1)
                ) &&
                (
                    Track2 == other.Track2 ||
                    Track2 != null &&
                    Track2.Equals(other.Track2)
                ) &&
                (
                    Track3 == other.Track3 ||
                    Track3 != null &&
                    Track3.Equals(other.Track3)
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
                if (Track1 != null)
                    hashCode = hashCode * 59 + Track1.GetHashCode();
                if (Track2 != null)
                    hashCode = hashCode * 59 + Track2.GetHashCode();
                if (Track3 != null)
                    hashCode = hashCode * 59 + Track3.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(MsrData left, MsrData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MsrData left, MsrData right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}