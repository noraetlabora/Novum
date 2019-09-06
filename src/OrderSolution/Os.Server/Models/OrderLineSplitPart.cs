using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Os.Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class OrderLineSplitPart
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "articleId")]
        public string ArticleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>

        [DataMember(Name = "quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>

        [DataMember(Name = "singelPrice")]
        public int? SingelPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "status")]
        public OrderLineStatus? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "modifiers")]
        public List<OrderLineModifier> Modifiers { get; set; }
    }
}
