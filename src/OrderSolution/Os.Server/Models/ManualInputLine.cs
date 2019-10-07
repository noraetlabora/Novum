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
    /// Input definition of a single manual line.
    /// </summary>
    [DataContract]
    public partial class ManualInputLine : IEquatable<ManualInputLine>
    {
        /// <summary>
        /// The key for this input line that will be used in the data reporting (see ManualInputDataLine.Key).
        /// </summary>
        /// <value>The key for this input line that will be used in the data reporting (see ManualInputDataLine.Key).</value>
        [DataMember(Name = "key")]
        public string Key { get; set; }

        /// <summary>
        /// The default value used if the user does not enter anything in this line.
        /// </summary>
        /// <value>The default value used if the user does not enter anything in this line.</value>
        [DataMember(Name = "defaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Regular expression used for validating the input. Per default (if not specified) we use the expression .* which will allow any string.
        /// </summary>
        /// <value>Regular expression used for validating the input. Per default (if not specified) we use the expression .* which will allow any string.</value>
        [DataMember(Name = "regExpValidator")]
        public string RegExpValidator { get; set; }

        /// <summary>
        /// If set defines the input mask to used to validate input. Details about how to define the input mask please check https://doc.qt.io/qt-5/qlineedit.html#inputMask-prop.
        /// </summary>
        /// <value>If set defines the input mask to used to validate input. Details about how to define the input mask please check https://doc.qt.io/qt-5/qlineedit.html#inputMask-prop.</value>
        [DataMember(Name = "inputMask")]
        public string InputMask { get; set; }

        /// <summary>
        /// The label for the input line that describes what the user is up to enter in that line. Usually shown above or left to the input field.
        /// </summary>
        /// <value>The label for the input line that describes what the user is up to enter in that line. Usually shown above or left to the input field.</value>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ManualInputLine {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  DefaultValue: ").Append(DefaultValue).Append("\n");
            sb.Append("  RegExpValidator: ").Append(RegExpValidator).Append("\n");
            sb.Append("  InputMask: ").Append(InputMask).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ManualInputLine)obj);
        }

        /// <summary>
        /// Returns true if ManualInputLine instances are equal
        /// </summary>
        /// <param name="other">Instance of ManualInputLine to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ManualInputLine other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Key == other.Key ||
                    Key != null &&
                    Key.Equals(other.Key)
                ) &&
                (
                    DefaultValue == other.DefaultValue ||
                    DefaultValue != null &&
                    DefaultValue.Equals(other.DefaultValue)
                ) &&
                (
                    RegExpValidator == other.RegExpValidator ||
                    RegExpValidator != null &&
                    RegExpValidator.Equals(other.RegExpValidator)
                ) &&
                (
                    InputMask == other.InputMask ||
                    InputMask != null &&
                    InputMask.Equals(other.InputMask)
                ) &&
                (
                    Label == other.Label ||
                    Label != null &&
                    Label.Equals(other.Label)
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
                if (Key != null)
                    hashCode = hashCode * 59 + Key.GetHashCode();
                if (DefaultValue != null)
                    hashCode = hashCode * 59 + DefaultValue.GetHashCode();
                if (RegExpValidator != null)
                    hashCode = hashCode * 59 + RegExpValidator.GetHashCode();
                if (InputMask != null)
                    hashCode = hashCode * 59 + InputMask.GetHashCode();
                if (Label != null)
                    hashCode = hashCode * 59 + Label.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ManualInputLine left, ManualInputLine right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ManualInputLine left, ManualInputLine right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}