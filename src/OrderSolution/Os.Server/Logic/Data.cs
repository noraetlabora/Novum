using System;
using System.Collections.Generic;

namespace Os.Server.Logic
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
        public static List<Models.CancellationReason> GetCancellationReasons()
        {
            var osCancellationReasons = new List<Models.CancellationReason>();
            var ntCancellationReasons = Nt.Database.DB.Api.Misc.GetCancellationReason();
            foreach (var ntCancellationReason in ntCancellationReasons.Values)
            {
                var osCancellationReason = new Models.CancellationReason();
                osCancellationReason.Id = ntCancellationReason.Id;
                osCancellationReason.Name = ntCancellationReason.Name;
                osCancellationReasons.Add(osCancellationReason);
            }
            return osCancellationReasons;
        }

        #endregion

        #region PaymentMedia
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Models.PaymentMedium> GetPaymentMedia()
        {
            var osPaymentMedia = new List<Models.PaymentMedium>();
            var ntPaymentTypes = Nt.Database.DB.Api.Payment.GetPaymentTypes();

            foreach (var ntPaymentType in ntPaymentTypes.Values)
            {
                var osPaymentMedium = new Models.PaymentMedium();
                osPaymentMedium.Id = ntPaymentType.Id;
                osPaymentMedium.Name = ntPaymentType.Name;
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
        public static List<Models.Printer> GetPrinters()
        {
            var osPrinters = new List<Models.Printer>();
            var ntPrinters = Nt.Database.DB.Api.Printer.GetInvoicePrinters();

            foreach (var ntPrinter in ntPrinters.Values)
            {
                var osPrinter = new Models.Printer();
                osPrinter.Name = ntPrinter.Name;
                osPrinter.Path = ntPrinter.Id;
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
        public static List<Models.Article> GetArticles()
        {
            var osArticles = new List<Models.Article>();
            var ntArticles = Nt.Database.DB.Api.Article.GetArticles();
            var osArticleModifierGroups = GetArticleModifierGroups();

            foreach (var ntArticle in ntArticles.Values)
            {
                var osArticle = new Models.Article();

                osArticle.Id = ntArticle.Id;
                osArticle.Name = ntArticle.Name;

                if (ntArticle.AskForPrice)
                    osArticle.MustEnterPrice = 1;
                else
                    osArticle.MustEnterPrice = 0;

                if (osArticleModifierGroups.ContainsKey(osArticle.Id))
                    osArticle.ModifierGroups = osArticleModifierGroups[osArticle.Id];

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
        public static List<Models.Category> GetCategories(string menuId)
        {
            var osCategories = new List<Models.Category>();
            var ntMainMenus = Nt.Database.DB.Api.Menu.GetMainMenus(menuId);
            var ntMenus = Nt.Database.DB.Api.Menu.GetMenus();
            var ntMenuItems = Nt.Database.DB.Api.Menu.GetMenuItems();
            var handledMenuIds = new List<string>();

            foreach (var ntMainMenu in ntMainMenus.Values)
            {
                var osCategory = new Models.Category();
                var osCatetgoryContentEntries = new List<Models.CategoryContentEntry>();
                osCategory.Name = ntMainMenu.Name;

                foreach(var ntMenuItem in ntMenuItems)
                {
                    if (!ntMenuItem.MenuId.Equals(ntMainMenu.Id))
                        continue;

                    var osCategoryContentEntry = new Models.CategoryContentEntry();
                    osCategoryContentEntry.ArticleId = ntMenuItem.ArticleId;
                    osCatetgoryContentEntries.Add(osCategoryContentEntry);             
                }

                osCategory.Content = osCatetgoryContentEntries;
                osCategories.Add(osCategory);
            }

            return osCategories;
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
        public static List<Models.ModifierGroup> GetModifierGroups()
        {
            var osModifierGroups = new List<Models.ModifierGroup>();
            var ntModifierMenus = Nt.Database.DB.Api.Modifier.GetModifierMenus();

            foreach (var ntModifierMenu in ntModifierMenus.Values)
            {
                var osModifierGroup = new Models.ModifierGroup();
                osModifierGroup.Id = ntModifierMenu.Id;
                osModifierGroup.Name = ntModifierMenu.Name;
                osModifierGroup.MinChoices = (int)ntModifierMenu.MinSelection;
                osModifierGroup.MaxChoices = (int)ntModifierMenu.MaxSelection;
                osModifierGroup.Question = "";
                osModifierGroup.Type = Models.ModifierGroup.ModifierType.PickOneEnum;
                osModifierGroup.Choices = new List<Models.ModifierChoice>();

                var modifiers = Nt.Database.DB.Api.Modifier.GetModifiers(ntModifierMenu.Id);
                var lastModifierId = "";

                foreach (var modifier in modifiers.Values)
                {
                    // ignore two sequently equal modifiers (eg. rare and rare)
                    if (lastModifierId.Equals(modifier.Id))
                        continue;
                    lastModifierId = modifier.Id;

                    var modifierChoice = new Models.ModifierChoice();

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
        public static List<Models.ServiceArea> GetServiceAreas()
        {
            var osServiceAreas = new List<Models.ServiceArea>();
            var ntServiceAreas = Nt.Database.DB.Api.Misc.GetServiceAreas();
            foreach (var ntServiceArea in ntServiceAreas.Values)
            {
                var osServiceArea = new Models.ServiceArea();
                osServiceArea.Id = ntServiceArea.Id;
                osServiceArea.Name = ntServiceArea.Name;
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
        public static List<Models.User> GetUsers()
        {
            var osUsers = new List<Models.User>();
            var ntWaiters = Nt.Database.DB.Api.Waiter.GetWaiters();
            foreach (var ntWaiter in ntWaiters.Values)
            {
                var osUser = new Models.User();
                osUser.Id = ntWaiter.Id;
                osUser.Name = ntWaiter.Name;
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
            return Nt.Database.DB.Api.Pos.GetClientId();
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

        #region private methods  

        private static Dictionary<string, List<Models.ArticleModifierGroup>> GetArticleModifierGroups() {
            var modifierDictionary = new Dictionary<string, List<Models.ArticleModifierGroup>>();
            var ntMenuItems = Nt.Database.DB.Api.Menu.GetMenuItems();
            var ntMenuItemsModifierMenus = Nt.Database.DB.Api.Modifier.GetMenuItemModifierMenus();

            foreach(var ntMenuItem in ntMenuItems) {
                var modifierList = new List<Models.ArticleModifierGroup>();
                foreach(var ntMenuItemsModifierMenu in ntMenuItemsModifierMenus) {
                    if (!ntMenuItemsModifierMenu.MenuItemMenuId.Equals(ntMenuItem.MenuId))
                        continue;
                    if (ntMenuItemsModifierMenu.MenuItemColumn < ntMenuItem.FromColumn ||
                        ntMenuItemsModifierMenu.MenuItemColumn > ntMenuItem.ToColumn     )
                        continue;
                    if (ntMenuItemsModifierMenu.MenuItemRow < ntMenuItem.FromRow ||
                        ntMenuItemsModifierMenu.MenuItemRow > ntMenuItem.ToRow     )
                        continue;
                    var articleModifierGroup = new Models.ArticleModifierGroup();
                    articleModifierGroup.ModifierGroupId = ntMenuItemsModifierMenu.ModifierMenuId;
                    modifierList.Add(articleModifierGroup);
                }

                if (modifierList.Count > 0) {
                    if (!modifierDictionary.ContainsKey(ntMenuItem.ArticleId))
                        modifierDictionary.Add(ntMenuItem.ArticleId, modifierList);
                }
            }

            return modifierDictionary;
        }

        #endregion
    }
}