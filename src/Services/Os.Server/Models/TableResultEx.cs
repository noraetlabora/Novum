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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Os.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TableResultEx : TableResult, IEquatable<TableResultEx>
    {
        /// <summary>
        /// Properties of this table. If undefined no properties have yet been assigned to this table.
        /// </summary>
        /// <value>Properties of this table. If undefined no properties have yet been assigned to this table.</value>
        [DataMember(Name = "properties")]
        public Object Properties { get; set; }

        /// <summary>
        /// Only supported if coursing features is enabled (see osConfiguration for details). If set this course is the selected one when entering the order screen. If not set the first course in the list of courses gets selected.
        /// </summary>
        /// <value>Only supported if coursing features is enabled (see osConfiguration for details). If set this course is the selected one when entering the order screen. If not set the first course in the list of courses gets selected.</value>
        [DataMember(Name = "courseSelectedId")]
        public string CourseSelectedId { get; set; }

        /// <summary>
        /// Only supported if coursing features is enabled (see osConfiguration for details). Get list of specific courses for this table that replaces the global defined courses list. IMPORTANT: For performance reasons the courses list should not be set if it is identical to the global courses list.
        /// </summary>
        /// <value>Only supported if coursing features is enabled (see osConfiguration for details). Get list of specific courses for this table that replaces the global defined courses list. IMPORTANT: For performance reasons the courses list should not be set if it is identical to the global courses list.</value>
        [DataMember(Name = "courses")]
        public List<Course> Courses { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TableResultEx {\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  CourseSelectedId: ").Append(CourseSelectedId).Append("\n");
            sb.Append("  Courses: ").Append(Courses).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson()
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
            return obj.GetType() == GetType() && Equals((TableResultEx)obj);
        }

        /// <summary>
        /// Returns true if TableResultEx instances are equal
        /// </summary>
        /// <param name="other">Instance of TableResultEx to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TableResultEx other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Properties == other.Properties ||
                    Properties != null &&
                    Properties.Equals(other.Properties)
                ) &&
                (
                    CourseSelectedId == other.CourseSelectedId ||
                    CourseSelectedId != null &&
                    CourseSelectedId.Equals(other.CourseSelectedId)
                ) &&
                (
                    Courses == other.Courses ||
                    Courses != null &&
                    Courses.SequenceEqual(other.Courses)
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
                if (Properties != null)
                    hashCode = hashCode * 59 + Properties.GetHashCode();
                if (CourseSelectedId != null)
                    hashCode = hashCode * 59 + CourseSelectedId.GetHashCode();
                if (Courses != null)
                    hashCode = hashCode * 59 + Courses.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(TableResultEx left, TableResultEx right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TableResultEx left, TableResultEx right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}