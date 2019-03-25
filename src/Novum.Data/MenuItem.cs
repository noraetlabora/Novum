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
        public string ReceiptName { get; set; }
        public uint Row { get; set; }
        public uint Column { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string SubMenu { get; set; }
        public bool AskForPrice { get; set; }
        public bool AskForName { get; set; }
        public bool ShowModifiers { get; set; }
        public string PLU { get; set; }

        #endregion
    }
}