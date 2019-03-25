using System;

namespace Novum.Data
{
    public class ModifierMenu
    {

        #region Constructor
        public ModifierMenu()
        {

        }
        #endregion

        #region Properties

        public string Id { get; set; }
        public string Name { get; set; }
        public uint MinSelection { get; set; }
        public uint MaxSelection { get; set; }

        #endregion


    }



}