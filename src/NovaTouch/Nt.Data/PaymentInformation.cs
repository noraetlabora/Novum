using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class PaymentInformation
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal BenefitAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string BenefitId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DiscountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Guests { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string PriceLevel { get; set; }



        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PaymentInformation()
        {

        }
        #endregion


        #region public methods


        #endregion
    }
}