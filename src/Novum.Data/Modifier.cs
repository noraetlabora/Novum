using System;

namespace Novum.Data
{
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
        public string ReceiptName { get; set; }
        public uint DefaultAmount { get; set; }
        public uint MinAmount { get; set; }
        public uint MaxAmount { get; set; }

        #endregion

    }
}