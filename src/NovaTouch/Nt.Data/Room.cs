using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Room
    {
        #region Properties

        /// <summary>
        /// Id Room
        /// </summary>
        /// <value>123</value>
        public string Id { get; set; }

        /// <summary>
        /// Name of guest
        /// </summary>
        /// <value>John Doe</value>
        public string Name { get; set; }

        /// <summary>
        /// Booking number
        /// </summary>
        /// <value>20191718-0</value>
        public string BookingNumber { get; set; }
        

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Room()
        {

        }
        #endregion
    }
}