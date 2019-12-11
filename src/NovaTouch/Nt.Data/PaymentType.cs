using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class PaymentType
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// id of the PaymentType
        /// </summary>
        /// <value>usualy an unsigned integer</value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>^, ^WKR2K7CO, ^WKR2K7KST</value>
        public string Program { get; set; }

        /// <summary>
        /// count of receipts to print 
        /// </summary>
        /// <value>unsigned integer</value>
        public uint ReceiptCount { get; set; }

        /// <summary>
        /// Signature indicates if the PaymentType needs a Signature
        /// </summary>
        /// <value>true or false</value>
        public bool Signature { get; set; }

        /// <summary>
        /// Id of partner system (EGUMA, NTV, IBELSA, PROTEL, ...)
        /// </summary>
        /// <value>true or false</value>
        public string PartnerId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PaymentType()
        {

        }
        #endregion
    }
}