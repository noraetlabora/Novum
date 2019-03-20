using System;

namespace Novum.Data
{
    public class MenuItem
    {

        #region Constructor
        public MenuItem()
        {

        }
        #endregion

        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public uint Row { get; set; }
        public uint Column { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string SubMenu { get; set; }



        #endregion
    }
}