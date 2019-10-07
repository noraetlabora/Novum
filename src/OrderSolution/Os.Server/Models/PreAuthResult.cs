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
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PreAuthResult : IEquatable<PreAuthResult>
    {
        /// <summary>
        /// This is the code to be used in the later pay action to identify this authorized payment medium and it&#39;s amount.
        /// </summary>
        /// <value>This is the code to be used in the later pay action to identify this authorized payment medium and it&#39;s amount.</value>
        [DataMember(Name = "authCode")]
        public string AuthCode { get; set; }

        /// <summary>
        /// The amount that was authorized. This can be the same as the requested amount but can also be lower. Examples when this might be lower then the requested amount:     - if the lines include articles that are not allowed for this payment medium     - and others
        /// </summary>
        /// <value>The amount that was authorized. This can be the same as the requested amount but can also be lower. Examples when this might be lower then the requested amount:     - if the lines include articles that are not allowed for this payment medium     - and others</value>
        [Required]
        [DataMember(Name = "authAmount")]
        public int? AuthAmount { get; set; }

        /// <summary>
        /// The tip amount that was authorized. Examples when this might be lower then the requested amount:     - if the medium does not allow to pay the tip with it     - and others
        /// </summary>
        /// <value>The tip amount that was authorized. Examples when this might be lower then the requested amount:     - if the medium does not allow to pay the tip with it     - and others</value>
        [Required]
        [DataMember(Name = "authTip")]
        public int? AuthTip { get; set; }

        /// <summary>
        /// If set a popup dialog is presented to the user before the user can continue. Supported types: \&quot;ok\&quot; and \&quot;accept\&quot;.
        /// </summary>
        /// <value>If set a popup dialog is presented to the user before the user can continue. Supported types: \&quot;ok\&quot; and \&quot;accept\&quot;.</value>
        [DataMember(Name = "dialog")]
        public Object Dialog { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PreAuthResult {\n");
            sb.Append("  AuthCode: ").Append(AuthCode).Append("\n");
            sb.Append("  AuthAmount: ").Append(AuthAmount).Append("\n");
            sb.Append("  AuthTip: ").Append(AuthTip).Append("\n");
            sb.Append("  Dialog: ").Append(Dialog).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PreAuthResult)obj);
        }

        /// <summary>
        /// Returns true if PreAuthResult instances are equal
        /// </summary>
        /// <param name="other">Instance of PreAuthResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PreAuthResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    AuthCode == other.AuthCode ||
                    AuthCode != null &&
                    AuthCode.Equals(other.AuthCode)
                ) &&
                (
                    AuthAmount == other.AuthAmount ||
                    AuthAmount != null &&
                    AuthAmount.Equals(other.AuthAmount)
                ) &&
                (
                    AuthTip == other.AuthTip ||
                    AuthTip != null &&
                    AuthTip.Equals(other.AuthTip)
                ) &&
                (
                    Dialog == other.Dialog ||
                    Dialog != null &&
                    Dialog.Equals(other.Dialog)
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
                if (AuthCode != null)
                    hashCode = hashCode * 59 + AuthCode.GetHashCode();
                if (AuthAmount != null)
                    hashCode = hashCode * 59 + AuthAmount.GetHashCode();
                if (AuthTip != null)
                    hashCode = hashCode * 59 + AuthTip.GetHashCode();
                if (Dialog != null)
                    hashCode = hashCode * 59 + Dialog.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(PreAuthResult left, PreAuthResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PreAuthResult left, PreAuthResult right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}