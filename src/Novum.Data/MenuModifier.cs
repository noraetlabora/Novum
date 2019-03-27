using System;

namespace Novum.Data
{
    /// <summary>
    /// MenuModifier contains for a specific menu/row/col the id of the modifier
    /// </summary>
    /// <example>
    /// MenuId 1 (Menu), Row 1, Column 1, Sort 1, ModifierMenuId 904 (meat preparation [rare, medium, well done])
    /// MenuId 1 (Menu), Row 1, Column 1, Sort 2, ModifierMenuId 905 (accompaniment [rice, potatoes, fries])
    /// MenuId 9 (Beer), Row 3, Column 2, Sort 1, ModifierMenuId 909 (drink preparation [cold, warm, with water])
    /// </example>
    public class MenuModifier
    {

        #region Constructor
        public MenuModifier()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// id of the menu
        /// </summary>
        /// <value>eg. 7 for main dish</value>
        public string MenuId { get; set; }
        /// <summary>
        /// row in the menu
        /// </summary>
        /// <value></value>
        public uint Row { get; set; }
        /// <summary>
        /// column in the menu
        /// </summary>
        /// <value></value>
        public uint Column { get; set; }
        /// <summary>
        /// sequential number
        /// </summary>
        /// <value></value>
        public uint Sort { get; set; }
        /// <summary>
        /// id of the modifier menu
        /// </summary>
        /// <value>eg. 904 for meat preparation, 905 for accompaniment</value>
        public string ModifierMenuId { get; set; }

        #endregion
    }
}