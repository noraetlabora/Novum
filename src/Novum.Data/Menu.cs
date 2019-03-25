using System;

namespace Novum.Data
{
    public class Menu
    {

        #region Constructor
        public Menu()
        {

        }
        #endregion

        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public uint Columns { get; set; }

        #endregion


    }

}