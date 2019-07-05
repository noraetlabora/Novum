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
    /// Information about the subtable
    /// </summary>
    [DataContract]
    public partial class SubTable : IEquatable<SubTable>
    { 
        /// <summary>
        /// The ID of the subtable.             
        /// </summary>
        /// <value>The ID of the subtable.             </value>
        [DataMember(Name="id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the subtable.
        /// </summary>
        /// <value>The name of the subtable.</value>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// If true this subtable will be selected when the table is opened. Can be used to directly show another subtable as the first one int he list to the user.
        /// </summary>
        /// <value>If true this subtable will be selected when the table is opened. Can be used to directly show another subtable as the first one int he list to the user.</value>
        [DataMember(Name="isSelected")]
        public bool? IsSelected { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubTable {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  IsSelected: ").Append(IsSelected).Append("\n");
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
            return obj.GetType() == GetType() && Equals((SubTable)obj);
        }

        /// <summary>
        /// Returns true if SubTable instances are equal
        /// </summary>
        /// <param name="other">Instance of SubTable to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SubTable other)
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
                    IsSelected == other.IsSelected ||
                    IsSelected != null &&
                    IsSelected.Equals(other.IsSelected)
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
                    if (IsSelected != null)
                    hashCode = hashCode * 59 + IsSelected.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(SubTable left, SubTable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SubTable left, SubTable right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}