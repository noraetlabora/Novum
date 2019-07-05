using System;
using System.Collections.Generic;
using System.Linq;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Session
    {
        private Dictionary<Permission.PermissionType, Permission> _permissions;
        private Dictionary<string, Order> _orders;
        private Dictionary<string, Table> _openTables;

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
        public string Printer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Nt.Data.Table CurrentTable { get; private set; }        

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
            this._orders = new Dictionary<string, Nt.Data.Order>();
            this._openTables = new Dictionary<string, Nt.Data.Table>();
        }
        #endregion

        #region public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool ContainsOrder(string orderId)
        {
            return _orders.ContainsKey(orderId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Nt.Data.Order GetOrder(string orderId)
        {
            if (_orders.ContainsKey(orderId))
                return _orders[orderId];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Nt.Data.Order> GetOrders()
        {
            return _orders.Values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public decimal AddOrder(Order order)
        {
            if (_orders.ContainsKey(order.Id))
                _orders[order.Id].Quantity += order.Quantity;
            else
                _orders.Add(order.Id, order);
            return _orders[order.Id].Quantity;
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
            if (!_orders.ContainsKey(orderId))
                return decimal.Zero;
            // remove order and return actual quantity
            if (_orders[orderId].Quantity <= voidQuantity)
            {
                var quantity = _orders[orderId].Quantity;
                _orders.Remove(orderId);
                return quantity;
            }
            // decrease quantity and return quantity of rest
            _orders[orderId].Quantity -= voidQuantity;
            return _orders[orderId].Quantity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="splitQuantity"></param>
        /// <returns></returns>
        public string SplitOrder(string orderId, decimal splitQuantity)
        {
            // no order with id found
            if (!_orders.ContainsKey(orderId))
                return null;
            // splitQuantity is larger/equal to orderline quantity => nothing to do
            if (_orders[orderId].Quantity >= splitQuantity)
                return null;
            // decrease quantity and return quantity of rest
            _orders[orderId].Quantity -= splitQuantity;
            var splitOrder = _orders[orderId];
            splitOrder.Quantity = splitQuantity;
            while(_orders.ContainsKey(splitOrder.Id)) 
            {
                splitOrder.Line++;
            }    
            _orders.Add(splitOrder.Id, splitOrder);
            return splitOrder.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>number of canceld orderlines</returns>
        public int ClearOrders()
        {
            var ordersCount = _orders.Count;
            _orders.Clear();
            return ordersCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        public void SetCurrentTable(Nt.Data.Table table)
        {
            if (!_openTables.ContainsKey(table.Id))
                _openTables.Add(table.Id, table);
            CurrentTable = table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool TableIdIsOpen(string tableId) 
        {
            if (_openTables.ContainsKey(tableId))
                return true;
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public string GetNewTableId(string tableId) 
        {
            //tableId is not yet open
            if (!TableIdIsOpen(tableId))
                return tableId;
            //return first subtable xxxx.1
            if (!tableId.Contains("."))
                return tableId + ".1";
            //get subtablenumber, icrement the number and return xxxx.yy
            var index = tableId.IndexOf('.');
            var subTableString = tableId.Substring(index+1);
            var mainTableId = tableId.Substring(0,index);
            var subTableNumber = uint.Parse(subTableString);
            subTableNumber++;
            return mainTableId + "." + subTableNumber.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        public void SetPermissions(Dictionary<Permission.PermissionType, Permission> permissions)
        {
            _permissions = permissions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionType"></param>
        /// <returns></returns>
        public bool Permitted(Permission.PermissionType permissionType)
        {
            if (!_permissions.ContainsKey(permissionType))
                return false;

            return _permissions[permissionType].Permitted;
        }

                /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionType"></param>
        /// <returns></returns>
        public bool NotPermitted(Permission.PermissionType permissionType)
        {
            return !Permitted(permissionType);
        }

        #endregion
    }
}