namespace Nt.Data
{
    /// <summary>
    /// Menu 
    /// </summary>
    /// <example>
    /// </example>
    public class Image
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
        public int Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public byte[] Data { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Image()
        {

        }
        #endregion

    }

}