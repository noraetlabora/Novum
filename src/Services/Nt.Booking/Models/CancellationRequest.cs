using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of the cancellation with a medium
    /// </summary>
    [DataContract]
    public partial class CancellationRequest
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

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
