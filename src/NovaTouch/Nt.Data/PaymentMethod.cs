using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class PaymentMethod
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string PaymentTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string AssignmentTypeId { get; set; }

        /// <summary>
        /// Type of the printer
        /// </summary>
        /// <value>^, ^WKR2K7CO, ^WKR2K7KST</value>
        public string Program { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal Tip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string TransactionId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PaymentMethod()
        {

        }
        #endregion


        #region public methods


        #endregion
    }
}