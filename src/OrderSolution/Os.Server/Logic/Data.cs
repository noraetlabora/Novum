using System;
using System.Collections.Generic;

namespace Os.Server.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Data
    {

        #region private fields;
            private static string[] notSupportedArticleIds = { "PLU", "$KONTO:", "$GUTSCHEIN:", "$GUTSCHEINBET:", "$RABATT:", "GANG:", "$FILTER:" };
            private static string[] notSupportedModifierIds = { "VORWAHL:" };

        #endregion  //private fields

        #region public methods

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

        #endregion  //Printers

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

                if (osArticleModifierGroups.ContainsKey(osArticle.Id))
                    osArticle.ModifierGroups = osArticleModifierGroups[osArticle.Id];

                osArticles.Add(osArticle);
            }

            return osArticles;
        }
        #endregion  //Articles

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

            foreach (var ntMainMenu in ntMainMenus.Values)
            {
                var osCategory = new Models.Category();
                osCategory.Name = ntMainMenu.Name;
                osCategory.Content = GetCategoryContent(ntMainMenu.Id, ntMenus, ntMenuItems, 0);

                osCategories.Add(osCategory);
            }

            return osCategories;
        }

        #endregion  //Categories

        #region ModifierGroups

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Models.ModifierGroup> GetModifierGroups()
        {
            var osModifierGroups = new List<Models.ModifierGroup>();
            var ntModifierMenus = Nt.Database.DB.Api.Modifier.GetModifierMenus();

            //////////////////////////////////////
            // iterate Modifier Menu
            //////////////////////////////////////
            foreach (var ntModifierMenu in ntModifierMenus.Values)
            {
                var osModifierGroup = new Models.ModifierGroup();
                osModifierGroup.Id = ntModifierMenu.Id;
                osModifierGroup.Name = ntModifierMenu.Name;
                osModifierGroup.MinChoices = (int)ntModifierMenu.MinSelection;
                osModifierGroup.MaxChoices = (int)ntModifierMenu.MaxSelection;
                osModifierGroup.Question = "";
                // type of modifier selection
                if (ntModifierMenu.MaxSelection.Equals(1))
                    osModifierGroup.Type = (int)Models.ModifierGroup.ModifierType.PickOneEnum;
                else 
                    osModifierGroup.Type = (int)Models.ModifierGroup.ModifierType.PickMultipleEnum;

                osModifierGroup.Choices = new List<Models.ModifierChoice>();
                var modifierItems = Nt.Database.DB.Api.Modifier.GetModifierItems(ntModifierMenu.Id);
                var lastModifierItemId = "";

                //////////////////////////////////////
                // Modifier Items
                //////////////////////////////////////
                foreach (var modifierItem in modifierItems.Values)
                {
                    // ignore not supported modifiers
                    if (ModifierIdNotSupported(modifierItem.Id))
                        continue;
                    // ignore two sequently equal modifiers (eg. rare and rare)
                    if (lastModifierItemId.Equals(modifierItem.Id))
                        continue;
                    lastModifierItemId = modifierItem.Id;

                    var modifierChoice = new Models.ModifierChoice();

                    modifierChoice.Id = modifierItem.Id;
                    modifierChoice.Name = modifierItem.Name;
                    modifierChoice.ReceiptName = modifierItem.Name;
                    modifierChoice.DefaultAmount = 0;
                    modifierChoice.MinAmount = (int)modifierItem.MinAmount;
                    modifierChoice.MaxAmount = (int)modifierItem.MaxAmount;

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

        #endregion  //POS

        #endregion  // public methods

        #region private methods  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Ignore all not supported modifiers like VORWAHL
        /// </summary>
        /// <param name="modifierId"></param>
        /// <returns></returns>
        private static bool ModifierIdNotSupported(string modifierId) {
            foreach (string notSupportedModifierId in notSupportedModifierIds)
            {
                if (modifierId.StartsWith(notSupportedModifierId))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="ntMenus"></param>
        /// <param name="ntMenuItems"></param>
        /// <param name="subMenu">number of submenu (mainMenu = 0, subMenu = 1, subsubMenu = 2, ...)</param>
        /// <returns></returns>
        private static List<Models.CategoryContentEntry> GetCategoryContent(string menuId, Dictionary<string, Nt.Data.Menu> ntMenus, List<Nt.Data.MenuItem> ntMenuItems, uint subMenu) {
            var categoryContent = new List<Models.CategoryContentEntry>();

            foreach(var ntMenuItem in ntMenuItems)
            {
                //
                if (!ntMenuItem.MenuId.Equals(menuId))
                    continue;
                //
                if (ArticleIdNotSupported(ntMenuItem.ArticleId))
                    continue;

                var osCategoryContentEntry = new Models.CategoryContentEntry();

                if (ntMenuItem.ArticleId.StartsWith("$")) {
                    // ignore sub/sub/.../menu 
                    if (subMenu > 3)
                        continue;
                    var subMenuId = ntMenuItem.ArticleId.Substring(1);
                    // ignore submenu when equals to menuId
                    if (subMenuId.Equals(menuId))
                        continue;
                    // subMenuId not known
                    if (!ntMenus.ContainsKey(subMenuId)) 
                        continue;
                    //
                    osCategoryContentEntry.Category = new Models.Category();
                    osCategoryContentEntry.Category.Name = ntMenus[subMenuId].Name;
                    osCategoryContentEntry.Category.Content = GetCategoryContent(subMenuId, ntMenus, ntMenuItems, subMenu + 1);
                }
                else {
                    osCategoryContentEntry.ArticleId = ntMenuItem.ArticleId;
                }

                categoryContent.Add(osCategoryContentEntry);             
            }

            return categoryContent;
        }

        #endregion  //private methods
    }
}