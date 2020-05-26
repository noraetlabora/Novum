using System.Runtime.Serialization;

namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of the payment with a medium
    /// </summary>
    [DataContract]
    public partial class DebitRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "metaData")]
        public MetaData MetaData { get; set; }



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
