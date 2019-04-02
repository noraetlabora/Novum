using System;
using System.Collections.Generic;
using Novum.Data.Os;
using Novum.Database.Api;

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
        public static List<Novum.Data.Os.CancellationReason> GetCancellationReasons(string department)
        {
            var osCReasons = new List<Novum.Data.Os.CancellationReason>();
            var novCReasons = Novum.Database.DB.Api.Misc.GetCancellationReason(department);
            foreach (var novCReason in novCReasons.Values)
            {
                var osCReason = new Novum.Data.Os.CancellationReason();
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
        public static List<Novum.Data.Os.PaymentMedium> GetPaymentMedia(string department)
        {
            var novPaymentTypes = Novum.Database.DB.Api.Payment.GetPaymentTypes(department);
            var osPaymentMedia = new List<Novum.Data.Os.PaymentMedium>();

            foreach (Novum.Data.PaymentType novPaymentType in novPaymentTypes.Values)
            {
                var osPaymentMedium = new Novum.Data.Os.PaymentMedium();
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
        public static List<Novum.Data.Os.Printer> GetPrinters(string department)
        {
            var novPrinters = Novum.Database.DB.Api.Printer.GetInvoicePrinters(department);
            var osPrinters = new List<Novum.Data.Os.Printer>();

            foreach (Novum.Data.Printer novPrinter in novPrinters.Values)
            {
                var osPrinter = new Novum.Data.Os.Printer();
                osPrinter.Name = novPrinter.Name;
                osPrinter.Path = novPrinter.Id;
                osPrinters.Add(osPrinter);
            }

            return osPrinters;
        }

        #endregion

        #region Articles
        public static List<Novum.Data.Os.Article> GetArticles(string department)
        {
            var osArticles = new List<Novum.Data.Os.Article>();
            var novArticles = Novum.Database.DB.Api.Article.GetArticles(department);
            var modifierMenus = Novum.Database.DB.Api.Modifier.GetModifierMenus(department);
            var menuModifiers = Novum.Database.DB.Api.Modifier.GetMenuModifiers(department);
            Novum.Data.ModifierMenu modifierMenu = null;

            foreach (Novum.Data.Article novArticle in novArticles.Values)
            {
                var osArticle = new Novum.Data.Os.Article();

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

                    var articleModifierGroup = new Novum.Data.Os.ArticleModifierGroup();
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
        public static List<Novum.Data.Os.Category> GetCategories(string department, string menuId)
        {
            var categories = new List<Novum.Data.Os.Category>();
            var mainMenus = Novum.Database.DB.Api.Menu.GetMainMenu(department, menuId);
            var handledMenuIds = new List<string>();

            foreach (Novum.Data.Menu menu in mainMenus.Values)
            {
                var category = new Novum.Data.Os.Category();
                category.Name = menu.Name;

                var contentEntries = GetCategoryContent(department, menu.Id, ref handledMenuIds);

                category.Content = contentEntries;
                categories.Add(category);
            }

            return categories;
        }

        private static List<Novum.Data.Os.CategoryContentEntry> GetCategoryContent(string department, string menuId, ref List<string> handledMenuIds)
        {
            var contentEntries = new List<Novum.Data.Os.CategoryContentEntry>();
            var articles = Novum.Database.DB.Api.Article.GetArticles(department, menuId);

            foreach (Novum.Data.Article article in articles.Values)
            {
                //ignore all not supported articles 
                if (ArticleIdNotSupported(article.Id))
                    continue;

                var contentEntry = new Novum.Data.Os.CategoryContentEntry();

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
                    contentEntry.Category = GetSubCategory(department, subMenuId, ref handledMenuIds);
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

        private static Novum.Data.Os.Category GetSubCategory(string department, string menuId, ref List<string> handledMenuIds)
        {
            var category = new Novum.Data.Os.Category();
            var subMenu = Novum.Database.DB.Api.Menu.GetSubMenu(department, menuId);
            category.Name = subMenu.Name;
            category.Content = GetCategoryContent(department, menuId, ref handledMenuIds);

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
        public static List<Novum.Data.Os.ModifierGroup> GetModifierGroups(string department)
        {
            var modifierGroups = new List<Novum.Data.Os.ModifierGroup>();
            var modifierMenus = Novum.Database.DB.Api.Modifier.GetModifierMenus(department);

            foreach (Novum.Data.ModifierMenu modifierMenu in modifierMenus.Values)
            {
                var modifierGroup = new Novum.Data.Os.ModifierGroup();
                modifierGroup.Id = modifierMenu.Id;
                modifierGroup.Name = modifierMenu.Name;
                modifierGroup.MinChoices = (int)modifierMenu.MinSelection;
                modifierGroup.MaxChoices = (int)modifierMenu.MaxSelection;
                modifierGroup.Question = "";
                modifierGroup.Type = Novum.Data.Os.ModifierGroup.ModifierType.PickOneEnum;
                modifierGroup.Choices = new List<ModifierChoice>();

                var modifiers = Novum.Database.DB.Api.Modifier.GetModifiers(department, modifierMenu.Id);
                var lastModifierId = "";

                foreach (Novum.Data.Modifier modifier in modifiers.Values)
                {
                    // ignore two sequently equal modifiers (eg. rare and rare)
                    if (lastModifierId.Equals(modifier.Id))
                        continue;
                    lastModifierId = modifier.Id;

                    var modifierChoice = new Novum.Data.Os.ModifierChoice();

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
        public static List<Novum.Data.Os.ServiceArea> GetServiceAreas(string department)
        {
            var ocServiceAreas = new List<Novum.Data.Os.ServiceArea>();
            var novServiceAreas = Novum.Database.DB.Api.Misc.GetServiceAreas(department);
            foreach (var novServiceArea in novServiceAreas.Values)
            {
                var osServiceArea = new Novum.Data.Os.ServiceArea();
                osServiceArea.Id = novServiceArea.Id;
                osServiceArea.Name = novServiceArea.Name;
                ocServiceAreas.Add(osServiceArea);
            }
            return ocServiceAreas;
        }

        #endregion

        #region Users

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.Os.User> GetUsers(string department)
        {
            var osUsers = new List<Novum.Data.Os.User>();
            var novWaiters = Novum.Database.DB.Api.Waiter.GetWaiters(department);
            foreach (var novWaiter in novWaiters.Values)
            {
                var osUser = new Novum.Data.Os.User();
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
        public static List<Novum.Data.Os.TableResult> GetTables(string department)
        {
            var osTables = new List<Novum.Data.Os.TableResult>();
            var novTables = Novum.Database.DB.Api.Table.GetTables(department);
            foreach (var novTable in novTables.Values)
            {
                var osTable = new Novum.Data.Os.TableResult();
                osTable.Id = novTable.Id;
                osTable.Name = novTable.Name;
                osTable.BookedAmount = (int)novTable.Amount * 100;
                osTable.LastActivityTime = (int)Utils.Unix.Timestamp(novTable.Updated);
                osTables.Add(osTable);
            }
            return osTables;
        }

        #endregion
    }
}