using System;
using System.Collections.Generic;

namespace Os.Logic
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
        public static List<Os.Data.CancellationReason> GetCancellationReasons()
        {
            var osCReasons = new List<Os.Data.CancellationReason>();
            var novCReasons = Nt.Database.DB.Api.Misc.GetCancellationReason();
            foreach (var novCReason in novCReasons.Values)
            {
                var osCReason = new Os.Data.CancellationReason();
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
        public static List<Os.Data.PaymentMedium> GetPaymentMedia()
        {
            var osPaymentMedia = new List<Os.Data.PaymentMedium>();
            var novPaymentTypes = Nt.Database.DB.Api.Payment.GetPaymentTypes();

            foreach (var novPaymentType in novPaymentTypes.Values)
            {
                var osPaymentMedium = new Os.Data.PaymentMedium();
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
        public static List<Os.Data.Printer> GetPrinters()
        {
            var osPrinters = new List<Os.Data.Printer>();
            var novPrinters = Nt.Database.DB.Api.Printer.GetInvoicePrinters();

            foreach (var novPrinter in novPrinters.Values)
            {
                var osPrinter = new Os.Data.Printer();
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
        public static List<Os.Data.Article> GetArticles()
        {
            var osArticles = new List<Os.Data.Article>();
            var novArticles = Nt.Database.DB.Api.Article.GetArticles();
            var novModifierMenus = Nt.Database.DB.Api.Modifier.GetModifierMenus();
            var novMenuModifiers = Nt.Database.DB.Api.Modifier.GetMenuModifiers();
            Nt.Data.ModifierMenu novModifierMenu = null;

            foreach (var novArticle in novArticles.Values)
            {
                var osArticle = new Os.Data.Article();

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
                foreach (var novMenuModifier in novMenuModifiers)
                {
                    if (!novMenuModifier.MenuId.Equals(novArticle.MenuId))
                        continue;
                    if (!novMenuModifier.Row.Equals(novArticle.Row))
                        continue;
                    if (!novMenuModifier.Column.Equals(novArticle.Column))
                        continue;
                    if (osArticle.ModifierGroups == null)
                        osArticle.ModifierGroups = new List<Os.Data.ArticleModifierGroup>();

                    var osArticleModifierGroup = new Os.Data.ArticleModifierGroup();
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
        public static List<Os.Data.Category> GetCategories(string menuId)
        {
            var osCategories = new List<Os.Data.Category>();
            var novMainMenus = Nt.Database.DB.Api.Menu.GetMainMenu(menuId);
            var handledMenuIds = new List<string>();

            foreach (var novMainMenu in novMainMenus.Values)
            {
                var osCategory = new Os.Data.Category();
                osCategory.Name = novMainMenu.Name;

                var contentEntries = GetCategoryContent(novMainMenu.Id, ref handledMenuIds);

                osCategory.Content = contentEntries;
                osCategories.Add(osCategory);
            }

            return osCategories;
        }

        private static List<Os.Data.CategoryContentEntry> GetCategoryContent(string menuId, ref List<string> handledMenuIds)
        {
            var osContentEntries = new List<Os.Data.CategoryContentEntry>();
            var novArticles = Nt.Database.DB.Api.Article.GetArticles(menuId);

            foreach (var novArticle in novArticles.Values)
            {
                //ignore all not supported articles 
                if (ArticleIdNotSupported(novArticle.Id))
                    continue;

                var osContentEntry = new Os.Data.CategoryContentEntry();

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

        private static Os.Data.Category GetSubCategory(string menuId, ref List<string> handledMenuIds)
        {
            var osCategory = new Os.Data.Category();
            var subMenu = Nt.Database.DB.Api.Menu.GetSubMenu(menuId);
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
        public static List<Os.Data.ModifierGroup> GetModifierGroups()
        {
            var osModifierGroups = new List<Os.Data.ModifierGroup>();
            var novModifierMenus = Nt.Database.DB.Api.Modifier.GetModifierMenus();

            foreach (var novModifierMenu in novModifierMenus.Values)
            {
                var osModifierGroup = new Os.Data.ModifierGroup();
                osModifierGroup.Id = novModifierMenu.Id;
                osModifierGroup.Name = novModifierMenu.Name;
                osModifierGroup.MinChoices = (int)novModifierMenu.MinSelection;
                osModifierGroup.MaxChoices = (int)novModifierMenu.MaxSelection;
                osModifierGroup.Question = "";
                osModifierGroup.Type = Os.Data.ModifierGroup.ModifierType.PickOneEnum;
                osModifierGroup.Choices = new List<Os.Data.ModifierChoice>();

                var modifiers = Nt.Database.DB.Api.Modifier.GetModifiers(novModifierMenu.Id);
                var lastModifierId = "";

                foreach (var modifier in modifiers.Values)
                {
                    // ignore two sequently equal modifiers (eg. rare and rare)
                    if (lastModifierId.Equals(modifier.Id))
                        continue;
                    lastModifierId = modifier.Id;

                    var modifierChoice = new Os.Data.ModifierChoice();

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
        public static List<Os.Data.ServiceArea> GetServiceAreas()
        {
            var osServiceAreas = new List<Os.Data.ServiceArea>();
            var novServiceAreas = Nt.Database.DB.Api.Misc.GetServiceAreas();
            foreach (var novServiceArea in novServiceAreas.Values)
            {
                var osServiceArea = new Os.Data.ServiceArea();
                osServiceArea.Id = novServiceArea.Id;
                osServiceArea.Name = novServiceArea.Name;
                osServiceAreas.Add(osServiceArea);
            }
            return osServiceAreas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        public static string GetServiceAreaId(string posId)
        {
            return Nt.Database.DB.Api.Pos.GetServiceAreaId(posId);
        }

        #endregion

        #region Users

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Os.Data.User> GetUsers()
        {
            var osUsers = new List<Os.Data.User>();
            var novWaiters = Nt.Database.DB.Api.Waiter.GetWaiters();
            foreach (var novWaiter in novWaiters.Values)
            {
                var osUser = new Os.Data.User();
                osUser.Id = novWaiter.Id;
                osUser.Name = novWaiter.Name;
                osUsers.Add(osUser);
            }
            return osUsers;
        }

        #endregion

        #region Pos

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetClientId()
        {
            return Nt.Database.InterSystems.Data.ClientId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static string GetPosId(string deviceId)
        {
            return Nt.Database.DB.Api.Pos.GetPosId(deviceId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceAreaId"></param>
        /// <returns></returns>
        public static string GetPriceLevel(string serviceAreaId)
        {
            return Nt.Database.DB.Api.Pos.GetPriceLevel(serviceAreaId);
        }

        #endregion
    }
}