using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class ServiceArea
    {
        #region Properties

        /// <summary>
        /// Id of the service area
        /// </summary>
        /// <value>"BAR", "STU", "TER"</value>
        public string Id { get; set; }

        /// <summary>
        /// Name of the service area
        /// </summary>
        /// <value>"Bar", "Stube", "Terasse"</value>
        public string Name { get; set; }

        /// <summary>
        /// price level
        /// </summary>
        /// <value></value>
        public string PriceLevel { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ServiceArea()
        {

        }
        #endregion
    }
}