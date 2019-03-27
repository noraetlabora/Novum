using System;

namespace Novum.Data
{
    /// <summary>
    /// Modifier 
    /// </summary>
    /// <example>
    /// </example>
    public class Modifier
    {

        #region Constructor
        public Modifier()
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
        public uint MinAmount { get; set; }
        public uint MaxAmount { get; set; }

        #endregion

    }
}