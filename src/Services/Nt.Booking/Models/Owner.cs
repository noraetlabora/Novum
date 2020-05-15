using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// owner of the medium
    /// </summary>
    [DataContract]
    public partial class Owner
    {
        /// <summary>
        /// name of the owner (John Doe)
        /// </summary>
        /// <value>name of the owner (John Doe)</value>
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
