using System;

namespace Nt.Data
{
    /// <summary>
    /// Modifier 
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
        public uint Row { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint Column { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ForegroundColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint MinAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint MaxAmount { get; set; }

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