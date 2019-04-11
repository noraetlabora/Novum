using System;

namespace Novum.Data
{
    public class ServiceArea
    {

        #region Constructor
        public ServiceArea()
        {

        }
        #endregion

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
    }
}