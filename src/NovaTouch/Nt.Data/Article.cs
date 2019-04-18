using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Article
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
        public string ReceiptName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string MenuId { get; set; }

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
        public bool AskForPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool AskForName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool ShowModifiers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Plu { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Article()
        {

        }
        #endregion

    }
}