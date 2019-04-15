using System;
using System.Collections.Generic;
using Novum.Data.Os;
using Novum.Database;

namespace Novum.Logic.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class Data
    {
        #region Cancellation Reasons

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<CancellationReason> GetCancellationReasons()
        {
            var osCReasons = new List<CancellationReason>();
            var novCReasons = DB.Api.Misc.GetCancellationReason();
            foreach (var novCReason in novCReasons.Values)
            {
                var osCReason = new CancellationReason();
                osCReason.Id = novCReason.Id;
                osCReason.Name = novCReason.Name;
                osCReasons.Add(osCReason);
            }
            return osCReasons;
        }

        #endregion

        #region PaymentMedia
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<PaymentMedium> GetPaymentMedia()
        {
            var osPaymentMedia = new List<PaymentMedium>();
            var novPaymentTypes = DB.Api.Payment.GetPaymentTypes();

            foreach (Novum.Data.PaymentType novPaymentType in novPaymentTypes.Values)
            {
                var osPaymentMedium = new PaymentMedium();
                osPaymentMedium.Id = novPaymentType.Id;
                osPaymentMedium.Name = novPaymentType.Name;
                osPaymentMedia.Add(osPaymentMedium);
            }

            return osPaymentMedia;
        }
        #endregion

        #region Printers

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Printer> GetPrinters()
        {
            var osPrinters = new List<Printer>();
            var novPrinters = DB.Api.Printer.GetInvoicePrinters();

            foreach (Novum.Data.Printer novPrinter in novPrinters.Values)
            {
                var osPrinter = new Printer();
                osPrinter.Name = novPrinter.Name;
                osPrinter.Path = novPrinter.Id;
                osPrinters.Add(osPrinter);
            }

            return osPrinters;
        }

        #endregion

        #region Articles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Article> GetArticles()
        {
            var osArticles = new List<Article>();
            var novArticles = DB.Api.Article.GetArticles();
            var novModifierMenus = DB.Api.Modifier.GetModifierMenus();
            var novMenuModifiers = DB.Api.Modifier.GetMenuModifiers();
            Novum.Data.ModifierMenu novModifierMenu = null;

            foreach (Novum.Data.Article novArticle in novArticles.Values)
            {
                var osArticle = new Article();

                osArticle.Id = novArticle.Id;
                osArticle.Name = novArticle.Name;
                osArticle.ReceiptName = novArticle.ReceiptName;
                osArticle.Plu = novArticle.Plu;
                if (novArticle.AskForPrice)
                    osArticle.MustEnterPrice = 1;
                else
                    osArticle.MustEnterPrice = 0;

                // search the menu modifier (menu, row, col)
                // if found add the modifier menu id to the article
                foreach (Novum.Data.MenuModifier novMenuModifier in novMenuModifiers)
                {
                    if (!novMenuModifier.MenuId.Equals(novArticle.MenuId))
                        continue;
                    if (!novMenuModifier.Row.Equals(novArticle.Row))
                        continue;
                    if (!novMenuModifier.Column.Equals(novArticle.Column))
                        continue;
                    if (osArticle.ModifierGroups == null)
                        osArticle.ModifierGroups = new List<ArticleModifierGroup>();

                    var osArticleModifierGroup = new ArticleModifierGroup();
                    osArticleModifierGroup.ModifierGroupId = novMenuModifier.ModifierMenuId;
                    osArticle.ModifierGroups.Add(osArticleModifierGroup);

                    if (novModifierMenus.TryGetValue(novMenuModifier.ModifierMenuId, out novModifierMenu))
                    {
                        if (novModifierMenu.MinSelection > 0)
                            osArticle.ForceShowModifiers = true;
                    }
                }

                osArticles.Add(osArticle);
            }

            return osArticles;
        }
        #endregion

        #region Categories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Category> GetCategories(string menuId)
        {
            var osCategories = new List<Category>();
            var novMainMenus = DB.Api.Menu.GetMainMenu(menuId);
            var handledMenuIds = new List<string>();

            foreach (Novum.Data.Menu novMainMenu in novMainMenus.Values)
            {
                var osCategory = new Category();
                osCategory.Name = novMainMenu.Name;

                var contentEntries = GetCategoryContent(novMainMenu.Id, ref handledMenuIds);

                osCategory.Content = contentEntries;
                osCategories.Add(osCategory);
            }

            return osCategories;
        }

        private static List<CategoryContentEntry> GetCategoryContent(string menuId, ref List<string> handledMenuIds)
        {
            var osContentEntries = new List<CategoryContentEntry>();
            var novArticles = DB.Api.Article.GetArticles(menuId);

            foreach (Novum.Data.Article novArticle in novArticles.Values)
            {
                //ignore all not supported articles 
                if (ArticleIdNotSupported(novArticle.Id))
                    continue;

                var osContentEntry = new CategoryContentEntry();

                // submenu
                if (novArticle.Id.StartsWith("$"))
                {
                    var subMenuId = novArticle.Id.Substring(1);
                    // don't loop the same menu "Dessert -> Dessert -> Dessert -> ..."
                    if (menuId.Equals(subMenuId))
                        continue;
                    // don't add a menu which is already handled, could make an infinity loop
                    if (handledMenuIds.Contains(subMenuId))
                        continue;
                    handledMenuIds.Add(subMenuId);
                    //get subMenu
                    osContentEntry.Category = GetSubCategory(subMenuId, ref handledMenuIds);
                }
                // normal article
                else
                {
                    osContentEntry.ArticleId = novArticle.Id;
                }

                osContentEntries.Add(osContentEntry);
            }
            return osContentEntries;
        }

        private static Category GetSubCategory(string menuId, ref List<string> handledMenuIds)
        {
            var osCategory = new Category();
            var subMenu = DB.Api.Menu.GetSubMenu(menuId);
            osCategory.Name = subMenu.Name;
            osCategory.Content = GetCategoryContent(menuId, ref handledMenuIds);

            return osCategory;
        }

        private static string[] notSupportedArticleIds = { "PLU", "$KONTO:", "$GUTSCHEIN:", "$GUTSCHEINBET:", "$RABATT:", "GANG:", "$FILTER:" };
        /// <summary>
        /// Ignore all not supported articles like PLU, GANG, RABATT, etc.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        private static bool ArticleIdNotSupported(string articleId)
        {
            foreach (string notSupportedArticleId in notSupportedArticleIds)
            {
                if (articleId.StartsWith(notSupportedArticleId))
                    return true;
            }
            return false;
        }

        #endregion

        #region ModifierGroups

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ModifierGroup> GetModifierGroups()
        {
            var osModifierGroups = new List<ModifierGroup>();
            var novModifierMenus = DB.Api.Modifier.GetModifierMenus();

            foreach (Novum.Data.ModifierMenu novModifierMenu in novModifierMenus.Values)
            {
                var osModifierGroup = new ModifierGroup();
                osModifierGroup.Id = novModifierMenu.Id;
                osModifierGroup.Name = novModifierMenu.Name;
                osModifierGroup.MinChoices = (int)novModifierMenu.MinSelection;
                osModifierGroup.MaxChoices = (int)novModifierMenu.MaxSelection;
                osModifierGroup.Question = "";
                osModifierGroup.Type = ModifierGroup.ModifierType.PickOneEnum;
                osModifierGroup.Choices = new List<ModifierChoice>();

                var modifiers = DB.Api.Modifier.GetModifiers(novModifierMenu.Id);
                var lastModifierId = "";

                foreach (Novum.Data.Modifier modifier in modifiers.Values)
                {
                    // ignore two sequently equal modifiers (eg. rare and rare)
                    if (lastModifierId.Equals(modifier.Id))
                        continue;
                    lastModifierId = modifier.Id;

                    var modifierChoice = new ModifierChoice();

                    modifierChoice.Id = modifier.Id;
                    modifierChoice.Name = modifier.Name;
                    modifierChoice.ReceiptName = modifier.Name;
                    modifierChoice.DefaultAmount = 0;
                    modifierChoice.MinAmount = (int)modifier.MinAmount;
                    modifierChoice.MaxAmount = (int)modifier.MaxAmount;

                    osModifierGroup.Choices.Add(modifierChoice);
                }
                osModifierGroups.Add(osModifierGroup);
            }

            return osModifierGroups;
        }

        #endregion

        #region Service Areas

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ServiceArea> GetServiceAreas()
        {
            var osServiceAreas = new List<ServiceArea>();
            var novServiceAreas = DB.Api.Misc.GetServiceAreas();
            foreach (var novServiceArea in novServiceAreas.Values)
            {
                var osServiceArea = new ServiceArea();
                osServiceArea.Id = novServiceArea.Id;
                osServiceArea.Name = novServiceArea.Name;
                osServiceAreas.Add(osServiceArea);
            }
            return osServiceAreas;
        }

        #endregion

        #region Users

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUsers()
        {
            var osUsers = new List<User>();
            var novWaiters = DB.Api.Waiter.GetWaiters();
            foreach (var novWaiter in novWaiters.Values)
            {
                var osUser = new User();
                osUser.Id = novWaiter.Id;
                osUser.Name = novWaiter.Name;
                osUsers.Add(osUser);
            }
            return osUsers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<TableResult> GetTables()
        {
            var osTables = new Dictionary<string, TableResult>();
            var novTables = DB.Api.Table.GetTables();

            // create main tables
            foreach (var novTable in novTables.Values)
            {
                var osTable = new TableResult();
                osTable.Id = GetMainTable(novTable.Id);

                if (osTables.ContainsKey(osTable.Id))
                    continue;

                osTable.Name = GetMainTable(novTable.Name);
                osTable.SubTables = new List<SubTable>();
                osTable.BookedAmount = 0;
                osTable.LastActivityTime = (int)Utils.Unix.Timestamp(novTable.Updated);
                osTables.Add(osTable.Id, osTable);
            }

            // add subtables to maintables
            foreach (var novTable in novTables.Values)
            {
                var osSubTable = new SubTable();
                osSubTable.Id = novTable.Id;
                osSubTable.Name = novTable.Name;
                osSubTable.IsSelected = false;

                var mainTableId = GetMainTable(novTable.Id);
                osTables[mainTableId].SubTables.Add(osSubTable);
                osTables[mainTableId].BookedAmount += (int)decimal.Multiply(novTable.Amount, 100);

                //take time of the table last updated further in the past
                var lastUpdated = (int)Utils.Unix.Timestamp(novTable.Updated);
                if (osTables[mainTableId].LastActivityTime > lastUpdated)
                    osTables[mainTableId].LastActivityTime = lastUpdated;
            }

            return new List<TableResult>(osTables.Values);
        }

        /// <summary>
        ///  returns the main table, it does not check if the string is an id or name of the table
        /// </summary>
        /// <param name="table"></param>
        /// <returns>1010 (table 1010), 1010 (table 1010.1), 10 (table 10) 10 (table 10.2)</returns>
        private static string GetMainTable(string table)
        {
            if (table.Contains("."))
            {
                var index = table.IndexOf(".");
                return table.Substring(0, index);
            }
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subTableId"></param>
        /// <returns></returns>
        public static List<OrderLine> GetOrderLines(string subTableId)
        {
            var osOrderLines = new List<OrderLine>();
            var novOrders = DB.Api.Order.GetOrders(subTableId);

            foreach (var novOrder in novOrders.Values)
            {
                var osOrderLine = new OrderLine();
                osOrderLine.Id = novOrder.Id;
                osOrderLine.ArticleId = novOrder.ArticleId;
                osOrderLine.Quantity = (int)novOrder.Quantity;
                osOrderLine.SinglePrice = (int)decimal.Multiply(novOrder.UnitPrice, 100);
                osOrderLine.Status = GetOsOrderLineStatus(novOrder.Status);
                osOrderLines.Add(osOrderLine);
            }
            return osOrderLines;
        }

        private static OrderLine.OrderLineStatus GetOsOrderLineStatus(Novum.Data.Order.OrderStatus novOrderStatus)
        {
            switch (novOrderStatus)
            {
                case Novum.Data.Order.OrderStatus.Ordered:
                    return OrderLine.OrderLineStatus.CommittedEnum;
                case Novum.Data.Order.OrderStatus.NewOrder:
                    return OrderLine.OrderLineStatus.OrderedEnum;
                default:
                    return OrderLine.OrderLineStatus.UnknownEnum;
            }
        }

        #endregion
    }
}