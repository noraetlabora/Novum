using System.Collections.Generic;
using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Order : IDbOrder
    {
        public Order()
        {

        }

        public Dictionary<string, Novum.Data.Order> GetOrders(string tableId)
        {
            var orders = new Dictionary<string, Novum.Data.Order>();
            var dbString = Interaction.CallClassMethod("cmNT.BonOman", "GetAllTischBonMitAenderer", Data.Department, "RK", tableId, "", "2");
            var ordersString = new Novum.Data.Utils.DataString(dbString);
            var ordersArray = ordersString.SplitByCRLF();

            foreach (string orderString in ordersArray)
            {
                if (string.IsNullOrEmpty(orderString))
                    continue;
                var table = new Novum.Data.Order(orderString);
                orders.Add(table.Id, table);
            }

            return orders;
        }
    }
}