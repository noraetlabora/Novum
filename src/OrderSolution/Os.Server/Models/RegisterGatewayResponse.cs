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
    /// Provides site specific data on successful initialization to OsServer.
    /// </summary>
    [DataContract]
    public partial class RegisterGatewayResponse : IEquatable<RegisterGatewayResponse>
    { 
        /// <summary>
        /// The name of the restaurant we are working with.
        /// </summary>
        /// <value>The name of the restaurant we are working with.</value>
        [DataMember(Name="restaurantName")]
        public string RestaurantName { get; set; }

        /// <summary>
        /// A partner ID is necessary to run OrderSolution beyond demonstration mode. To get your free partner ID, please contact SystemIntegration@orderman.com. In case you have more than one product, you should ask for seperate partner IDs. Partner IDs should be kept confidential. In future versions, it is possible we display the given integration name visibly to the end customer.
        /// </summary>
        /// <value>A partner ID is necessary to run OrderSolution beyond demonstration mode. To get your free partner ID, please contact SystemIntegration@orderman.com. In case you have more than one product, you should ask for seperate partner IDs. Partner IDs should be kept confidential. In future versions, it is possible we display the given integration name visibly to the end customer.</value>
        [DataMember(Name="partnerId")]
        public string PartnerId { get; set; }

        /// <summary>
        /// The current UTC time stamp (in seconds since epoch time). Used for synchronization between clients and the server.
        /// </summary>
        /// <value>The current UTC time stamp (in seconds since epoch time). Used for synchronization between clients and the server.</value>
        [Required]
        [DataMember(Name="utcTime")]
        public int? UtcTime { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RegisterGatewayResponse {\n");
            sb.Append("  RestaurantName: ").Append(RestaurantName).Append("\n");
            sb.Append("  PartnerId: ").Append(PartnerId).Append("\n");
            sb.Append("  UtcTime: ").Append(UtcTime).Append("\n");
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
            return obj.GetType() == GetType() && Equals((RegisterGatewayResponse)obj);
        }

        /// <summary>
        /// Returns true if RegisterGatewayResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of RegisterGatewayResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RegisterGatewayResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    RestaurantName == other.RestaurantName ||
                    RestaurantName != null &&
                    RestaurantName.Equals(other.RestaurantName)
                ) && 
                (
                    PartnerId == other.PartnerId ||
                    PartnerId != null &&
                    PartnerId.Equals(other.PartnerId)
                ) && 
                (
                    UtcTime == other.UtcTime ||
                    UtcTime != null &&
                    UtcTime.Equals(other.UtcTime)
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
                    if (RestaurantName != null)
                    hashCode = hashCode * 59 + RestaurantName.GetHashCode();
                    if (PartnerId != null)
                    hashCode = hashCode * 59 + PartnerId.GetHashCode();
                    if (UtcTime != null)
                    hashCode = hashCode * 59 + UtcTime.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(RegisterGatewayResponse left, RegisterGatewayResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RegisterGatewayResponse left, RegisterGatewayResponse right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}