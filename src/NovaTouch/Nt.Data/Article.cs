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
        public bool AskForPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool AskForName { get; set; }

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