using System;
using System.Collections.Generic;

namespace Novum.Data
{
    public class Session
    {

        #region Constructor
        public Session()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Department = "";
            this.DeviceType = "";
            this.OperatingSystem = "";
            this.PosId = "";
            this.SerialNumber = "";
            this.ServiceAreaId = "";
            this.WaiterId = "";
        }
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DeviceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string OperatingSystem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string PosId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ServiceAreaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string WaiterId { get; set; }


        #endregion



    }
}