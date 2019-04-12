using System;

namespace Novum.Data
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
        public string Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public uint Columns { get; set; }

        #endregion

        #region Constructor
        public Menu()
        {

        }
        #endregion

    }

}