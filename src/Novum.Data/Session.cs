using System;
using System.Collections.Generic;
using System.Linq;

namespace Novum.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Session
    {
        private Dictionary<string, Order> orders;

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

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Table CurrentTable { get; private set; }

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
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Novum.Data.Order GetOrder(string orderId)
        {
            if (orders.ContainsKey(orderId))
                return orders[orderId];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Novum.Data.Order> GetOrders()
        {
            return orders.Values.ToList();
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="voidQuantity"></param>
        /// <returns></returns>
        public decimal VoidOrder(string orderId, decimal voidQuantity)
        {
            // no order with id found
            if (!orders.ContainsKey(orderId))
                return decimal.Zero;
            // remove order and return actual quantity
            if (orders[orderId].Quantity <= voidQuantity)
            {
                var quantity = orders[orderId].Quantity;
                orders.Remove(orderId);
                return quantity;
            }
            // decrease quantity and return quantity of rest
            orders[orderId].Quantity -= voidQuantity;
            return orders[orderId].Quantity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>number of canceld orderlines</returns>
        public int ClearOrders()
        {
            var ordersCount = orders.Count;
            orders.Clear();
            return ordersCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        public void SetCurrentTable(Novum.Data.Table table)
        {
            this.CurrentTable = table;
        }

        #endregion
    }
}
