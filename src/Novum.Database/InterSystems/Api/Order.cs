using System;
using System.Collections.Generic;
using Novum.Data;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Order> GetOrders(string tableId)
        {
            var orders = new Dictionary<string, Novum.Data.Order>();
            var dbString = Interaction.CallClassMethod("cmNT.BonOman", "GetAllTischBonMitAenderer", Data.ClientId, "RK", tableId, "", "2");
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

        public Novum.Data.Order GetNewOrder(Session session, string articleId)
        {
            var order = new Novum.Data.Order();
            var dbString = Interaction.CallClassMethod("cmNT.BonOman", "GetPLUDaten", session.ClientId, session.PosId, session.WaiterId, "tableId", session.PriceLevel, "N", articleId);
            var orderString = new Novum.Data.Utils.DataString(dbString);
            var orderArray = orderString.SplitByChar96();
            var orderList = new Novum.Data.Utils.DataList(orderArray);

            var availability = orderList.GetString(21);
            Article.CheckAvailibility(availability);

            order.ArticleId = orderList.GetString(0);
            order.Name = orderList.GetString(1);
            order.UnitPrice = orderList.GetDecimal(4);
            order.CourseId = orderList.GetString(20);
            order.Status = Novum.Data.Order.OrderStatus.NewOrder;

            return order;
        }

    }
}