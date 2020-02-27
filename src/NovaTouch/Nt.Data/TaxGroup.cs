using System;

namespace Nt.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class TaxGroup
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public UInt32 TaxRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public UInt32 TaxRate2 { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public TaxGroup()
        {

        }
        #endregion
    }
}
