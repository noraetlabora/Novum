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
    /// A dialog type used in some POS responses to force the OsApp to show a dialog to the user.              NOTE: At the moment only a dialog with an OK button is supported. So it&#39;s an option to show final information to the user         but there is no option to get some results back.
    /// </summary>
    [DataContract]
    public partial class OsDialog : IEquatable<OsDialog>
    {
        /// <summary>
        /// Header to be shown for this message on the dialog.
        /// </summary>
        /// <value>Header to be shown for this message on the dialog.</value>
        [DataMember(Name = "header")]
        public string Header { get; set; }

        /// <summary>
        /// Message to be shown in the dialog. The text can be formated by using Qt&#39;s subset of html/richtext. Details see https://doc.qt.io/qt-5/richtext-html-subset.html.
        /// </summary>
        /// <value>Message to be shown in the dialog. The text can be formated by using Qt&#39;s subset of html/richtext. Details see https://doc.qt.io/qt-5/richtext-html-subset.html.</value>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Defines the type of the dialog to be shown to the user.s ATTENTION: Some types will not be available for some actions. Please check in the actions descriptions.
        /// </summary>
        /// <value>Defines the type of the dialog to be shown to the user.s ATTENTION: Some types will not be available for some actions. Please check in the actions descriptions.</value>
        [Required]
        [DataMember(Name = "type")]
        public DialogType Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OsDialog {\n");
            sb.Append("  Header: ").Append(Header).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OsDialog)obj);
        }

        /// <summary>
        /// Returns true if OsDialog instances are equal
        /// </summary>
        /// <param name="other">Instance of OsDialog to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OsDialog other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Header == other.Header ||
                    Header != null &&
                    Header.Equals(other.Header)
                ) &&
                (
                    Message == other.Message ||
                    Message != null &&
                    Message.Equals(other.Message)
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
                if (Header != null)
                    hashCode = hashCode * 59 + Header.GetHashCode();
                if (Message != null)
                    hashCode = hashCode * 59 + Message.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(OsDialog left, OsDialog right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OsDialog left, OsDialog right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
