using System;
using System.Runtime.Serialization;
using System.Text;

namespace Nt.Booking.Models
{
    /// <summary>
    /// This object contains all information of an article
    /// </summary>
    [DataContract]
    public partial class Article
    {
        /// <summary>
        /// ID of the article
        /// </summary>
        /// <value>ID of the article</value>
        [DataMember(Name = "articleId")]
        public string ArticleId { get; set; }

        /// <summary>
        /// Name of the article
        /// </summary>
        /// <value>Name of the article</value>
        [DataMember(Name = "articleName")]
        public string ArticleName { get; set; }

        /// <summary>
        /// Quantity  (*100)
        /// </summary>
        /// <value>Quantity  (*100)</value>
        [DataMember(Name = "quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Unit of the quantity
        /// </summary>
        /// <value>Unit of the quantity</value>
        [DataMember(Name = "quantityUnit")]
        public string QuantityUnit { get; set; }

        /// <summary>
        /// ID of the article group
        /// </summary>
        /// <value>ID of the article group</value>
        [DataMember(Name = "articleGroupId")]
        public string ArticleGroupId { get; set; }

        /// <summary>
        /// Name of the article group
        /// </summary>
        /// <value>Name of the article group</value>
        [DataMember(Name = "articleGroupName")]
        public string ArticleGroupName { get; set; }

        /// <summary>
        /// Unit price including tax  (*100)
        /// </summary>
        /// <value>Unit price including tax  (*100)</value>
        [DataMember(Name = "unitPrice")]
        public int? UnitPrice { get; set; }

        /// <summary>
        /// Tax rate  (*100)
        /// </summary>
        /// <value>Tax rate  (*100)</value>
        [DataMember(Name = "taxRate")]
        public int? TaxRate { get; set; }

        /// <summary>
        /// discount (discount to the total amount &#x3D; unitPrice * quantity)  (*100)
        /// </summary>
        /// <value>discount (discount to the total amount &#x3D; unitPrice * quantity)  (*100)</value>
        [DataMember(Name = "discountAmount")]
        public int? DiscountAmount { get; set; }

        /// <summary>
        /// ID of the discount group
        /// </summary>
        /// <value>ID of the discount group</value>
        [DataMember(Name = "discountGroupId")]
        public string DiscountGroupId { get; set; }

        /// <summary>
        /// Name of the discount group
        /// </summary>
        /// <value>Name of the discount group</value>
        [DataMember(Name = "discountGroupName")]
        public string DiscountGroupName { get; set; }

       
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
