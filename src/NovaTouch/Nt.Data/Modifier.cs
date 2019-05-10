using System;

namespace Nt.Data
{
    /// <summary>
    /// ModifierItem
    /// </summary>
    /// <example>
    /// </example>
    public class Modifier
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ArticleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal TotalPrice
        {
            get { return decimal.Multiply(Quantity, UnitPrice); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal Percent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal Rounding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string MenuId { get; set;}

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Modifier()
        {

        }
        #endregion

    }
}