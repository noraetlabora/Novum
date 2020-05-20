using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// credit object
    /// </summary>
    [DataContract]
    public partial class CreditResponse : Response
    {
        /// <summary>
        /// credit balance
        /// </summary>
        /// <value>credit balance</value>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// credit balance at opening
        /// </summary>
        /// <value>credit balance at opening</value>
        [DataMember(Name = "openingAmount")]
        public decimal OpeningAmount { get; set; }


        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
