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
    /// The result of a successful orderline split operation.
    /// </summary>
    [DataContract]
    public partial class OrderLineSplitResult : IEquatable<OrderLineSplitResult>
    { 
        /// <summary>
        /// The original orderline from which data was split away with it&#39;s updated data. e.g. if from 3xCoke we split 2xCoke away this one will be the 1xCoke line.
        /// </summary>
        /// <value>The original orderline from which data was split away with it&#39;s updated data. e.g. if from 3xCoke we split 2xCoke away this one will be the 1xCoke line.</value>
        [DataMember(Name="originalOl")]
        public OrderLineSplitPart OriginalOl { get; set; }

        /// <summary>
        /// The new orderline that was created by this split operation. e.g. if from 3xCoke we split 2xCoke away this one will be the 2xCoke line.
        /// </summary>
        /// <value>The new orderline that was created by this split operation. e.g. if from 3xCoke we split 2xCoke away this one will be the 2xCoke line.</value>
        [DataMember(Name="splittedOl")]
        public OrderLineSplitPart SplittedOl { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderLineSplitResult {\n");
            sb.Append("  OriginalOl: ").Append(OriginalOl).Append("\n");
            sb.Append("  SplittedOl: ").Append(SplittedOl).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OrderLineSplitResult)obj);
        }

        /// <summary>
        /// Returns true if OrderLineSplitResult instances are equal
        /// </summary>
        /// <param name="other">Instance of OrderLineSplitResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderLineSplitResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    OriginalOl == other.OriginalOl ||
                    OriginalOl != null &&
                    OriginalOl.Equals(other.OriginalOl)
                ) && 
                (
                    SplittedOl == other.SplittedOl ||
                    SplittedOl != null &&
                    SplittedOl.Equals(other.SplittedOl)
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
                    if (OriginalOl != null)
                    hashCode = hashCode * 59 + OriginalOl.GetHashCode();
                    if (SplittedOl != null)
                    hashCode = hashCode * 59 + SplittedOl.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(OrderLineSplitResult left, OrderLineSplitResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OrderLineSplitResult left, OrderLineSplitResult right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
