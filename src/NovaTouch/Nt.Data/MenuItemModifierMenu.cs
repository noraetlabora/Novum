namespace Nt.Data
{
    /// <summary>
    /// MenuModifier contains for a specific menu/row/col the id of the modifier
    /// </summary>
    /// <example>
    /// Id 904, Name meat preparation 
    /// Id 905, Name accompaniment
    /// Id 909, drink preparation
    /// </example>
    public class MenuItemModifierMenu
    {
        #region Properties


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string MenuItemMenuId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint MenuItemRow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint MenuItemColumn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public uint Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ModifierMenuId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// empty Constructor for MenuItemModifierMenu
        /// </summary>
        public MenuItemModifierMenu()
        {

        }
        #endregion

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public bool ContainsMenuItem(Nt.Data.MenuItem menuItem)
        {
            if (!this.MenuItemMenuId.Equals(menuItem.MenuId))
                return false;
            if (this.MenuItemColumn < menuItem.FromColumn ||
                this.MenuItemColumn > menuItem.ToColumn)
                return false;
            if (this.MenuItemRow < menuItem.FromRow ||
                this.MenuItemRow > menuItem.ToRow)
                return false;
            return true;
        }

        #endregion

    }

}