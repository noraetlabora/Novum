namespace Nt.Data
{
    /// <summary>
    /// Menu 
    /// </summary>
    /// <example>
    /// Id 1, Name appetizer, Columns 4
    /// Id 2, Name main dishe, Columns 4
    /// Id 3, Name dessert, Columns 2
    /// Id 4, Name soft drink, Columns 3
    /// Id 5, Name beer, Columns 3
    /// Id 6, Name wine, Columns 5
    /// </example>
    public class Menu
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
        public uint Columns { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Menu()
        {

        }
        #endregion

    }

}