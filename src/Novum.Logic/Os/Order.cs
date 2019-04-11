using System;
using System.Collections.Generic;
using Novum.Data;
using Novum.Data.Os;
using Novum.Database;


namespace Novum.Logic.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OrderLineResult Add(Session session, string subTableId, OrderLineAdd data)
        {
            var orderLineResult = new OrderLineResult();

            if (data.EnteredPrice != 0)
                DB.Api.Article.CheckEnteredPrice(session, data.ArticleId, decimal.Multiply((decimal)data.EnteredPrice, 100));
            var order = DB.Api.Order.GetNewOrder(session, data.ArticleId);
            order.Quantity = (decimal)data.Quantity;
            session.AddOrder(order);

            orderLineResult.Id = order.Id;
            orderLineResult.SinglePrice = (int)decimal.Multiply(order.UnitPrice, 100);

            return orderLineResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OrderLineVoidResult Void(Session session, string subTableId, OrderLineVoid data)
        {


            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="subTableId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OrderLineResult Modify(Session session, string subTableId, OrderLineModify data)
        {


            return null;
        }
    }
}