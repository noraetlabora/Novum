using System;
using System.Collections.Generic;

namespace Novum.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Session
    {
        private Dictionary<string, Order> orders;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Table CurrentTable { get; set; }

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
        public string ClientId { get; set; }

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
        public string PriceLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string WaiterId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Session()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ClientId = "";
            this.DeviceType = "";
            this.OperatingSystem = "";
            this.PosId = "";
            this.SerialNumber = "";
            this.ServiceAreaId = "";
            this.WaiterId = "";
            this.PriceLevel = "";
            this.orders = new Dictionary<string, Order>();
        }
        #endregion

        #region public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public decimal AddOrder(Order order)
        {
            if (orders.ContainsKey(order.Id))
                orders[order.Id].Quantity += order.Quantity;
            else
                orders.Add(order.Id, order);
            return orders[order.Id].Quantity;
        }

        #endregion
    }
}