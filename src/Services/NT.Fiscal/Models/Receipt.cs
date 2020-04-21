using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace NT.Fiscal.Models
{
    /// <summary>
    /// Contains information about the receipt
    /// </summary>
    [DataContract]
    public class Receipt
    {
        /// <summary>
        /// date and time of the receipt
        /// </summary>
        /// <value>date and time of the receipt</value>
        [DataMember(Name = "bookingTimeStamp")]
        [Required]
        public DateTime? BookingTimeStamp { get; set; }

        /// <summary>
        /// ID of the POS operator
        /// </summary>
        /// <value>ID of the POS operator</value>
        [DataMember(Name = "waiterId")]
        [Required]
        public string WaiterId { get; set; }

        /// <summary>
        /// Name of the POS operator
        /// </summary>
        /// <value>Name of the POS operator</value>
        [DataMember(Name = "waiterName")]
        public string WaiterName { get; set; }

        /// <summary>
        /// ID of the POS
        /// </summary>
        /// <value>ID of the POS</value>
        [DataMember(Name = "posId")]
        public string PosId { get; set; }

        /// <summary>
        /// Name of the POS
        /// </summary>
        /// <value>Name of the POS</value>
        [DataMember(Name = "posName")]
        public string PosName { get; set; }

        /// <summary>
        /// ID of the service area
        /// </summary>
        /// <value>ID of the service area</value>
        [DataMember(Name = "serviceAreaId")]
        public string ServiceAreaId { get; set; }

        /// <summary>
        /// Name of the service area
        /// </summary>
        /// <value>Name of the service area</value>
        [DataMember(Name = "serviceAreaName")]
        public string ServiceAreaName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Receipt {\n");
            sb.Append("  BookingTimeStamp: ").Append(BookingTimeStamp).Append("\n");
            sb.Append("  WaiterId: ").Append(WaiterId).Append("\n");
            sb.Append("  WaiterName: ").Append(WaiterName).Append("\n");
            sb.Append("  PosId: ").Append(PosId).Append("\n");
            sb.Append("  PosName: ").Append(PosName).Append("\n");
            sb.Append("  ServiceAreaId: ").Append(ServiceAreaId).Append("\n");
            sb.Append("  ServiceAreaName: ").Append(ServiceAreaName).Append("\n");
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
    }
}