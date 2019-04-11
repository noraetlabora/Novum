using System;
using System.Collections.Generic;

namespace Novum.Data
{
    public class Session
    {
        private List<string> openTableIds;
        private Dictionary<string, Order> orders;

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
            this.openTableIds = new List<string>();
            this.orders = new Dictionary<string, Order>();
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
        public Table CurrentTable { get; }


        public bool OpenTable(string tableId)
        {
            if (!openTableIds.Contains(tableId))
            {
                openTableIds.Add(tableId);
                return true;
            }
            return false;
        }

        public bool CloseTable(string tableId)
        {
            if (openTableIds.Contains(tableId))
            {
                openTableIds.Remove(tableId);
                return true;
            }
            return false;
        }

        public int CloseAllTables()
        {
            var closedTables = openTableIds.Count;
            openTableIds = new List<string>();
            return closedTables;
        }

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