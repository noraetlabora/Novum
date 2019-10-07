using System.Collections.Generic;
using System.Text;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Order : IDbOrder
    {
        #region constructor

        public Order()
        {

        }

        #endregion

        #region public methods        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Dictionary<string, Nt.Data.Order> GetOrders(string tableId)
        {
            var orders = new Dictionary<string, Nt.Data.Order>();
            var dbString = Interaction.CallClassMethod("cmNT.BonOman", "GetAllTischBonMitAenderer", Api.ClientId, "RK", tableId, "", "2");
            var ordersString = new DataString(dbString);
            var ordersArray = ordersString.SplitByCRLF();
            var orderLine = 0;
            var lastOrderId = "";

            foreach (string orderString in ordersArray)
            {
                if (string.IsNullOrEmpty(orderString))
                    continue;
                if (orderString.Contains("FAX"))
                    continue;

                var dataString = new DataString(orderString);
                var dataList = new DataList(dataString.SplitByChar96());
                var indexString = new DataString(dataList.GetString(0));
                var indexList = new DataList(indexString.SplitByDoublePipes());
                var menuString = new DataString(dataList.GetString(9));
                var menuList = new DataList(menuString.SplitBySemicolon());
                var type = dataList.GetString(6);

                // orderline
                if (string.IsNullOrEmpty(type) || type.Equals("I"))
                {
                    var order = new Nt.Data.Order();
                    orderLine++;
                    order.TableId = tableId;
                    order.Line = orderLine;
                    order.AssignmentTypeId = indexList.GetString(0);
                    order.ArticleId = indexList.GetString(1);
                    order.UnitPrice = indexList.GetDecimal(2);
                    order.ReferenceId = indexList.GetString(3);
                    order.Quantity = dataList.GetDecimal(1);
                    order.Status = (Nt.Data.Order.OrderStatus)dataList.GetUInt(2);
                    order.Name = dataList.GetString(4);
                    order.CourseMenu = dataList.GetString(7);
                    order.CourseNumber = dataList.GetString(8);
                    order.CourseName = dataList.GetString(16);
                    order.ArticleGroupId = dataList.GetString(23);
                    if (Misc.cachedArticleGroups.ContainsKey(order.ArticleGroupId))
                    {
                        order.TaxGroupId = Misc.cachedArticleGroups[order.ArticleGroupId].TaxGroupId;
                    }
                    if (Misc.cachedTaxGroups.ContainsKey(order.TaxGroupId))
                    {
                        order.TaxRate = Misc.cachedTaxGroups[order.TaxGroupId].TaxRate;
                    }

                    if (orders.ContainsKey(order.Id))
                        orders[order.Id].Quantity += order.Quantity;
                    else
                        orders.Add(order.Id, order);

                    lastOrderId = order.Id;
                }
                // modifier
                else if (type.Equals("A"))
                {
                    var modifier = new Nt.Data.Modifier();
                    var articleId = indexList.GetString(1);
                    // text input
                    if (string.IsNullOrEmpty(articleId))
                    {
                        modifier.Name = dataList.GetString(4);
                    }
                    // choice
                    else
                    {
                        modifier.ArticleId = indexList.GetString(1);
                        modifier.UnitPrice = indexList.GetDecimal(2);
                        modifier.Quantity = dataList.GetDecimal(1);
                        modifier.Name = dataList.GetString(4);
                        modifier.MenuId = menuList.GetString(3);
                    }
                    //
                    if (orders.ContainsKey(lastOrderId))
                        orders[lastOrderId].AddModifier(modifier);
                }
            }

            return orders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public Nt.Data.Order GetNewOrder(Nt.Data.Session session, string articleId)
        {
            var order = new Nt.Data.Order();
            var dbString = Interaction.CallClassMethod("cmNT.BonOman", "GetPLUDaten", session.ClientId, session.PosId, session.WaiterId, "tableId", session.PriceLevel, "N", articleId);
            var dataString = new DataString(dbString);
            var dataArray = dataString.SplitByChar96();
            var dataList = new DataList(dataArray);

            var availability = dataList.GetString(21);
            Article.CheckAvailibility(availability);

            order.TableId = session.CurrentTable.Id;
            order.Line = session.GetOrders().Count + 1;
            order.ArticleId = dataList.GetString(0);
            order.Name = dataList.GetString(1);
            order.UnitPrice = dataList.GetDecimal(4);
            order.AssignmentTypeId = dataList.GetString(5);
            order.CourseMenu = dataList.GetString(19);
            order.CourseNumber = dataList.GetString(20);
            order.CourseName = dataList.GetString(22);
            order.Status = Nt.Data.Order.OrderStatus.NewOrder;

            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="order"></param>
        /// <param name="newQuantity"></param>
        /// <param name="cancellationReasonId"></param>
        /// <param name="authorizingWaiterId"></param>
        public void VoidOrderedOrder(Nt.Data.Session session, string tableId, Nt.Data.Order order, decimal newQuantity, string cancellationReasonId, string authorizingWaiterId)
        {
            Interaction.CallVoidClassMethod("cmNT.BonOman", "SetBonDaten", session.ClientId, session.PosId, session.WaiterId, tableId);
            var orderDataString = GetVoidDataString(order);
            Interaction.CallVoidClassMethod("cmNT.BonOman", "SetBonZeile", session.ClientId, session.PosId, session.WaiterId, tableId, session.PriceLevel, orderDataString, newQuantity.ToString(), cancellationReasonId);
            Interaction.CallVoidClassMethod("cmNT.BonOman", "SetBonStornoOK", session.ClientId, session.PosId, session.WaiterId, tableId, session.PriceLevel, authorizingWaiterId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="articleId"></param>
        /// <param name="voidQuantity"></param>
        /// <param name="voidPrice"></param>
        /// <param name="assignmentTypeId"></param>
        /// <param name="authorizingWaiterId"></param>
        public void VoidNewOrder(Nt.Data.Session session, string tableId, string articleId, decimal voidQuantity, decimal voidPrice, string assignmentTypeId, string authorizingWaiterId)
        {
            Interaction.CallVoidClassMethod("cmNT.BonOman", "SetSofortStornoJournal", session.ClientId, session.PosId, session.WaiterId, tableId, articleId, voidQuantity.ToString(), voidPrice.ToString(), assignmentTypeId, authorizingWaiterId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        /// <param name="voidQuantity"></param>
        /// <param name="orderSequenceNumber"></param>
        /// <param name="authorizingWaiterId"></param>
        public void VoidPrebookedOrder(Nt.Data.Session session, string tableId, decimal voidQuantity, string orderSequenceNumber, string authorizingWaiterId)
        {
            Interaction.CallVoidClassMethod("cmNT.BonOmanVormerk", "SetVormerkStorno", session.ClientId, session.PosId, session.WaiterId, tableId, orderSequenceNumber, authorizingWaiterId, voidQuantity.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tableId"></param>
        public void FinalizeOrder(Nt.Data.Session session, string tableId)
        {
            var orders = session.GetOrders();
            FinalizeOrder(session, orders, tableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="orders"></param>
        /// <param name="tableId"></param>
        public void FinalizeOrder(Nt.Data.Session session, List<Nt.Data.Order> orders, string tableId)
        {
            //
            if (orders == null || orders.Count == 0)
                return;
            //
            if (string.IsNullOrEmpty(tableId))
                return;
            //
            var newOrdersDataString = GetOrderDataString(orders);
            //
            if (string.IsNullOrEmpty(newOrdersDataString))
                return;
            //
            Interaction.CallVoidClassMethod("cmNT.BonOman", "SetAllBonDatenMitAenderer", session.ClientId, session.PosId, session.WaiterId, tableId, session.PriceLevel, newOrdersDataString);
        }

        #endregion

        #region internal methods

        internal static string GetOrderDataString(List<Nt.Data.Order> orders)
        {
            var dataString = new StringBuilder();
            foreach (var order in orders)
            {
                if (dataString.Length > 0)
                    dataString.Append(DataString.CRLF);
                dataString.Append(GetOrderDataString(order));
            }
            return dataString.ToString();
        }

        internal static string GetOrderDataString(Nt.Data.Order order)
        {
            var dataString = new StringBuilder();
            //
            dataString.Append(order.AssignmentTypeId).Append(DataString.DoublePipes);
            dataString.Append(order.ArticleId).Append(DataString.DoublePipes);
            dataString.Append(order.UnitPrice).Append(DataString.DoublePipes);
            dataString.Append(order.ReferenceId).Append(DataString.DoublePipes);
            dataString.Append(order.SequenceNumber).Append(DataString.Char96);
            //
            dataString.Append(order.Quantity).Append(DataString.Char96);
            dataString.Append((int)order.Status).Append(DataString.Char96);
            dataString.Append(order.ArticleId).Append(DataString.Char96);
            dataString.Append(order.Name).Append(DataString.Char96);
            dataString.Append(order.TotalPrice).Append(DataString.Char96);
            dataString.Append("").Append(DataString.Char96);
            dataString.Append(order.CourseMenu).Append(DataString.Char96);
            dataString.Append(order.CourseNumber).Append(DataString.Char96);
            dataString.Append("").Append(DataString.Char96); //SubmenuId;Zeile;Spalte
            dataString.Append("0").Append(DataString.Char96); //Gewicht
            dataString.Append("").Append(DataString.Char96); //WiegeNr
            dataString.Append("0").Append(DataString.Char96); //WiegeGrundPreis
            dataString.Append(order.Name).Append(DataString.Char96);
            dataString.Append("0").Append(DataString.Char96); //WiegeArtikel
            dataString.Append(order.TotalPrice).Append(DataString.Char96);
            dataString.Append(order.CourseName).Append(DataString.Char96);
            dataString.Append("").Append(DataString.Char96); //PlatzNr
            dataString.Append("").Append(DataString.Char96); //GutscheinNummer;GutscheinNummerDisplay;GutscheinCOPA;GutscheinWertAlt;GutscheinWertNeu;GutscheinVorlage
            dataString.Append("").Append(DataString.Char96); //RabattBetrag
            dataString.Append("").Append(DataString.Char96); //HappyHourBetrag
            dataString.Append("").Append(DataString.Char96); //ZuschussBetrag
            dataString.Append("").Append(DataString.Char96); //HappyBezeichnung
            dataString.Append(order.ArticleGroupId).Append(DataString.Char96); //ArtikelGruppe
            dataString.Append("").Append(DataString.Char96); //ArtikelGruppeBezeichnung
            dataString.Append("").Append(DataString.Char96); //ArtikelSuchbegriffe
            dataString.Append("").Append(DataString.Char96); //ArtikelBehandlung
            dataString.Append("").Append(DataString.Char96); //HappyStufe
            dataString.Append("").Append(DataString.Char96); //RabattBetragVerrechnet
            dataString.Append(order.TaxRate).Append(DataString.Char96); //UstProzent
            dataString.Append("").Append(DataString.Char96); //UstProzent2
            dataString.Append("").Append(DataString.Char96); //Platzbonierung

            if (order.Modifiers != null)
            {
                foreach (var modifier in order.Modifiers)
                {
                    dataString.Append(DataString.CRLF);
                    dataString.Append(order.AssignmentTypeId).Append(DataString.DoublePipes);

                    if (modifier.Image == null)
                    {
                        dataString.Append(modifier.ArticleId).Append(DataString.DoublePipes);
                        dataString.Append(modifier.UnitPrice).Append(DataString.DoublePipes);
                        dataString.Append("").Append(DataString.DoublePipes);
                        dataString.Append("").Append(DataString.Char96);
                        dataString.Append("0").Append(DataString.Char96);
                        dataString.Append("11").Append(DataString.Char96);
                        dataString.Append("").Append(DataString.Char96);
                        dataString.Append(modifier.Name).Append("``A```0;0;0;");
                        dataString.Append(modifier.MenuId).Append("`0``0``0````;;;0;0;````````0`````0");
                    }
                    else
                    {
                        dataString.Append(order.ArticleId).Append(DataString.DoublePipes);
                        dataString.Append(order.UnitPrice).Append(DataString.DoublePipes);
                        dataString.Append("").Append(DataString.DoublePipes);
                        dataString.Append("").Append(DataString.DoublePipes);
                        dataString.Append(Image.GetDataString(modifier.Image));
                    }
                }
            }

            return dataString.ToString();
        }

        internal static string GetVoidDataString(Nt.Data.Order order)
        {
            var dataString = new StringBuilder();
            dataString.Append(order.AssignmentTypeId).Append(DataString.DoublePipes);
            dataString.Append(order.ArticleId).Append(DataString.DoublePipes);
            dataString.Append(order.UnitPrice).Append(DataString.DoublePipes);
            dataString.Append(order.ReferenceId).Append(DataString.DoublePipes);
            dataString.Append(order.SequenceNumber).Append(DataString.Char96);

            return dataString.ToString();
        }

        #endregion

    }
}