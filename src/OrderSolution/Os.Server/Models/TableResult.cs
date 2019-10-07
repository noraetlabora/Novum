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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TableResult : IEquatable<TableResult>
    {
        /// <summary>
        /// ID of the table.
        /// </summary>
        /// <value>ID of the table.</value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the table.
        /// </summary>
        /// <value>Name of the table.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Last time there was activity on this table. (Unix time-stamp in seconds &#x3D; seconds since Jan 01 1970 UTC)
        /// </summary>
        /// <value>Last time there was activity on this table. (Unix time-stamp in seconds &#x3D; seconds since Jan 01 1970 UTC)</value>
        [Required]
        [DataMember(Name = "lastActivityTime")]
        public int? LastActivityTime { get; set; }

        /// <summary>
        /// The amount (that finally has to be paid) that is currently booked on this table . This is the sum of all orderlines in all subtables of this table including possible choice prices.
        /// </summary>
        /// <value>The amount (that finally has to be paid) that is currently booked on this table . This is the sum of all orderlines in all subtables of this table including possible choice prices.</value>
        [Required]
        [DataMember(Name = "bookedAmount")]
        public int? BookedAmount { get; set; }

        /// <summary>
        /// The subtables of this table.
        /// </summary>
        /// <value>The subtables of this table.</value>
        [DataMember(Name = "subTables")]
        public List<SubTable> SubTables { get; set; }

        /// <summary>
        /// The ID of the service area this table belongs to.
        /// </summary>
        /// <value>The ID of the service area this table belongs to.</value>
        [DataMember(Name = "serviceAreaId")]
        public string ServiceAreaId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TableResult {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  LastActivityTime: ").Append(LastActivityTime).Append("\n");
            sb.Append("  BookedAmount: ").Append(BookedAmount).Append("\n");
            sb.Append("  SubTables: ").Append(SubTables).Append("\n");
            sb.Append("  ServiceAreaId: ").Append(ServiceAreaId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((TableResult)obj);
        }

        /// <summary>
        /// Returns true if TableResult instances are equal
        /// </summary>
        /// <param name="other">Instance of TableResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TableResult other)
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
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    LastActivityTime == other.LastActivityTime ||
                    LastActivityTime != null &&
                    LastActivityTime.Equals(other.LastActivityTime)
                ) &&
                (
                    BookedAmount == other.BookedAmount ||
                    BookedAmount != null &&
                    BookedAmount.Equals(other.BookedAmount)
                ) &&
                (
                    SubTables == other.SubTables ||
                    SubTables != null &&
                    SubTables.SequenceEqual(other.SubTables)
                ) &&
                (
                    ServiceAreaId == other.ServiceAreaId ||
                    ServiceAreaId != null &&
                    ServiceAreaId.Equals(other.ServiceAreaId)
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
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (LastActivityTime != null)
                    hashCode = hashCode * 59 + LastActivityTime.GetHashCode();
                if (BookedAmount != null)
                    hashCode = hashCode * 59 + BookedAmount.GetHashCode();
                if (SubTables != null)
                    hashCode = hashCode * 59 + SubTables.GetHashCode();
                if (ServiceAreaId != null)
                    hashCode = hashCode * 59 + ServiceAreaId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(TableResult left, TableResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TableResult left, TableResult right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
