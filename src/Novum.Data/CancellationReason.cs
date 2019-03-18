using System;

namespace Novum.Data
{
    public class CancellationResason
    {

        #region Constructor
        public CancellationResason()
        {

        }

        public CancellationResason(string id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion

        #region Properties

        public string Id { get; set; }
        public string Name { get; set; }

        #endregion


    }

}