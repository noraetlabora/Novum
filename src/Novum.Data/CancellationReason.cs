using System;

namespace Novum.Data
{
    public class CancellationResason
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        #endregion

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


    }

}