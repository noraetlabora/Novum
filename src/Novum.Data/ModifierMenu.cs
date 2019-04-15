using System;

namespace Novum.Data
{
    /// <summary>
    /// MenuModifier contains for a specific menu/row/col the id of the modifier
    /// </summary>
    /// <example>
    /// Id 904, Name meat preparation 
    /// Id 905, Name accompaniment
    /// Id 909, drink preparation
    /// </example>
    public class ModifierMenu
    {
        #region Properties

        /// <summary>
        /// id of the modifier menu
        /// </summary>
        /// <value>904</value>
        public string Id { get; set; }
        /// <summary>
        /// name of the modifier menu
        /// </summary>
        /// <value>meat preparation </value>
        public string Name { get; set; }
        /// <summary>
        /// minimal selection of the menu
        /// </summary>
        /// <value></value>
        public uint MinSelection { get; set; }
        /// <summary>
        /// maximum selection of the menu
        /// </summary>
        /// <value></value>
        public uint MaxSelection { get; set; }
        /// <summary>
        /// number of columns in the modifier menu
        /// </summary>
        /// <value></value>
        public uint Columns { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// empty Constructor for ModifierMenu
        /// </summary>
        public ModifierMenu()
        {

        }
        #endregion

    }



}