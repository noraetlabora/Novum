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
        /// <param name="department"></param>
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
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<PaymentMedium> GetPaymentMedia()
        {
            var novPaymentTypes = DB.Api.Payment.GetPaymentTypes();
            var osPaymentMedia = new List<PaymentMedium>();

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
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Printer> GetPrinters()
        {
            var novPrinters = DB.Api.Printer.GetInvoicePrinters();
            var osPrinters = new List<Printer>();

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
        public static List<Article> GetArticles()
        {
            var osArticles = new List<Article>();
            var novArticles = DB.Api.Article.GetArticles();
            var modifierMenus = DB.Api.Modifier.GetModifierMenus();
            var menuModifiers = DB.Api.Modifier.GetMenuModifiers();
            Novum.Data.ModifierMenu modifierMenu = null;

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
                foreach (Novum.Data.MenuModifier menuModifier in menuModifiers)
                {
                    if (!menuModifier.MenuId.Equals(novArticle.MenuId))
                        continue;
                    if (!menuModifier.Row.Equals(novArticle.Row))
                        continue;
                    if (!menuModifier.Column.Equals(novArticle.Column))
                        continue;
                    if (osArticle.ModifierGroups == null)
                        osArticle.ModifierGroups = new List<ArticleModifierGroup>();

                    var articleModifierGroup = new ArticleModifierGroup();
                    articleModifierGroup.ModifierGroupId = menuModifier.ModifierMenuId;
                    osArticle.ModifierGroups.Add(articleModifierGroup);

                    if (modifierMenus.TryGetValue(menuModifier.ModifierMenuId, out modifierMenu))
                    {
                        if (modifierMenu.MinSelection > 0)
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
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Category> GetCategories(string menuId)
        {
            var categories = new List<Category>();
            var mainMenus = DB.Api.Menu.GetMainMenu(menuId);
            var handledMenuIds = new List<string>();

            foreach (Novum.Data.Menu menu in mainMenus.Values)
            {
                var category = new Category();
                category.Name = menu.Name;

                var contentEntries = GetCategoryContent(menu.Id, ref handledMenuIds);

                category.Content = contentEntries;
                categories.Add(category);
            }

            return categories;
        }

        private static List<CategoryContentEntry> GetCategoryContent(string menuId, ref List<string> handledMenuIds)
        {
            var contentEntries = new List<CategoryContentEntry>();
            var articles = DB.Api.Article.GetArticles(menuId);

            foreach (Novum.Data.Article article in articles.Values)
            {
                //ignore all not supported articles 
                if (ArticleIdNotSupported(article.Id))
                    continue;

                var contentEntry = new CategoryContentEntry();

                // submenu
                if (article.Id.StartsWith("$"))
                {
                    var subMenuId = article.Id.Substring(1);
                    // don't loop the same menu "Dessert -> Dessert -> Dessert -> ..."
                    if (menuId.Equals(subMenuId))
                        continue;
                    // don't add a menu which is already handled, could make an infinity loop
                    if (handledMenuIds.Contains(subMenuId))
                        continue;
                    handledMenuIds.Add(subMenuId);
                    //get subMenu
                    contentEntry.Category = GetSubCategory(subMenuId, ref handledMenuIds);
                }
                // normal article
                else
                {
                    contentEntry.ArticleId = article.Id;
                }

                contentEntries.Add(contentEntry);
            }
            return contentEntries;
        }

        private static Category GetSubCategory(string menuId, ref List<string> handledMenuIds)
        {
            var category = new Category();
            var subMenu = DB.Api.Menu.GetSubMenu(menuId);
            category.Name = subMenu.Name;
            category.Content = GetCategoryContent(menuId, ref handledMenuIds);

            return category;
        }

        private static string[] notSupportedArticleIds = { "PLU", "$KONTO:", "$GUTSCHEIN:", "$GUTSCHEINBET:", "$RABATT:", "GANG:", "$FILTER:" };
        /// <summary>
        /// Ignore all not supported articles like PLU, GANG, RABATT, etc.
        /// </summary>
        /// <param name="article id"></param>
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
        public static List<ModifierGroup> GetModifierGroups()
        {
            var modifierGroups = new List<ModifierGroup>();
            var modifierMenus = DB.Api.Modifier.GetModifierMenus();

            foreach (Novum.Data.ModifierMenu modifierMenu in modifierMenus.Values)
            {
                var modifierGroup = new ModifierGroup();
                modifierGroup.Id = modifierMenu.Id;
                modifierGroup.Name = modifierMenu.Name;
                modifierGroup.MinChoices = (int)modifierMenu.MinSelection;
                modifierGroup.MaxChoices = (int)modifierMenu.MaxSelection;
                modifierGroup.Question = "";
                modifierGroup.Type = ModifierGroup.ModifierType.PickOneEnum;
                modifierGroup.Choices = new List<ModifierChoice>();

                var modifiers = DB.Api.Modifier.GetModifiers(modifierMenu.Id);
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

                    modifierGroup.Choices.Add(modifierChoice);
                }
                modifierGroups.Add(modifierGroup);
            }

            return modifierGroups;
        }

        #endregion

        #region Service Areas

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
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
        /// <param name="department"></param>
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
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<TableResult> GetTables()
        {
            var osTables = new List<TableResult>();
            var novTables = DB.Api.Table.GetTables();

            foreach (var novTable in novTables.Values)
            {
                var osTable = new TableResult();
                osTable.Id = novTable.Id;
                osTable.Name = novTable.Name;
                osTable.BookedAmount = (int)decimal.Multiply(novTable.Amount, 100);
                osTable.LastActivityTime = (int)Utils.Unix.Timestamp(novTable.Updated);
                osTables.Add(osTable);
            }
            return osTables;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
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