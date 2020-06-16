﻿using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class MetaData
    {
        /// <summary>
        /// ID of the requesting system.
        /// </summary>
        /// <value>ID of the requesting system.</value>
        [DataMember(Name = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Name of the requesting system.
        /// </summary>
        /// <value>Name of the requesting system.</value>
        [DataMember(Name = "clientName")]
        public string ClientName { get; set; }

        /// <summary>
        /// ID of the POS operator.
        /// </summary>
        /// <value>ID of the POS operator.</value>
        [DataMember(Name = "waiterId")]
        public string WaiterId { get; set; }

        /// <summary>
        /// Name of the POS operator.
        /// </summary>
        /// <value>Name of the POS operator.</value>
        [DataMember(Name = "waiterName")]
        public string WaiterName { get; set; }

        /// <summary>
        /// ID of the POS.
        /// </summary>
        /// <value>ID of the POS.</value>
        [DataMember(Name = "posId")]
        public string PosId { get; set; }

        /// <summary>
        /// Name of the POS.
        /// </summary>
        /// <value>Name of the POS.</value>
        [DataMember(Name = "posName")]
        public string PosName { get; set; }

        /// <summary>
        /// ID of the service area.
        /// </summary>
        /// <value>ID of the service area.</value>
        [DataMember(Name = "serviceAreaId")]
        public string ServiceAreaId { get; set; }

        /// <summary>
        /// Name of the service area.
        /// </summary>
        /// <value>Name of the service area.</value>
        [DataMember(Name = "serviceAreaName")]
        public string ServiceAreaName { get; set; }

        /// <summary>
        /// ID of the table.
        /// </summary>
        /// <value>ID of the table.</value>
        [DataMember(Name = "tableId")]
        public string TableId { get; set; }

        /// <summary>
        /// Name of the table.
        /// </summary>
        /// <value>Name of the table.</value>
        [DataMember(Name = "tableName")]
        public string TableName { get; set; }

        /// <summary>
        /// Transaction identification number.
        /// </summary>
        /// <value>Identification number of the transaction.</value>
        [DataMember(Name = "transactionId")]
        public string TransactionId { get; set; }

        /// <summary>
        /// Store number.
        /// </summary>
        /// <value>Identification number of the store.</value>
        [DataMember(Name = "branchId")]
        public string BranchId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MetaData {\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  ClientName: ").Append(ClientName).Append("\n");
            sb.Append("  WaiterId: ").Append(WaiterId).Append("\n");
            sb.Append("  WaiterName: ").Append(WaiterName).Append("\n");
            sb.Append("  PosId: ").Append(PosId).Append("\n");
            sb.Append("  PosName: ").Append(PosName).Append("\n");
            sb.Append("  ServiceAreaId: ").Append(ServiceAreaId).Append("\n");
            sb.Append("  ServiceAreaName: ").Append(ServiceAreaName).Append("\n");
            sb.Append("  TableId: ").Append(TableId).Append("\n");
            sb.Append("  TableName: ").Append(TableName).Append("\n");
            sb.Append("  InvoiceId: ").Append(TransactionId).Append("\n");
            sb.Append("  BranchId: ").Append(BranchId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            //return JsonConvert.SerializeObject(this, Formatting.Indented);
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
