using System;
using System.Collections.Generic;

namespace Novum.Logic.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsData
    {

        #region Cancellation Reason

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.Os.CancellationReason> GetCancellationReasons(string department)
        {
            var osCReasons = new List<Novum.Data.Os.CancellationReason>();
            var novCReasons = CancellationReason.GetCancellationReasons(department);
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

        #region Categories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.Os.Category> GetCategories(string department, string menuId)
        {
            var categories = new List<Novum.Data.Os.Category>();
            var mainMenus = Menu.GetMainMenu(department, menuId);
            var handledMenuIds = new List<string>();

            foreach (Data.Menu menu in mainMenus.Values)
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
            var articles = Article.GetArticles(department, menuId);

            foreach (Data.Article article in articles.Values)
            {
                //ignore all not supported menuItems 
                if (NotSupportedMenuItem(article.Id))
                    continue;

                var contentEntry = new Novum.Data.Os.CategoryContentEntry();

                // submenu (eg. "$9")
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

        /// <summary>
        /// Ignore all not supported menuItems like PLU, GANG, RABATT, etc.
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        private static bool NotSupportedMenuItem(string menuItemId)
        {
            if (menuItemId.StartsWith("PLU"))
                return true;
            if (menuItemId.StartsWith("$KONTO:"))
                return true;
            if (menuItemId.StartsWith("$GUTSCHEIN:"))
                return true;
            if (menuItemId.StartsWith("$GUTSCHEINBET:"))
                return true;
            if (menuItemId.StartsWith("$RABATT:"))
                return true;
            if (menuItemId.StartsWith("GANG:"))
                return true;
            if (menuItemId.StartsWith("$FILTER:"))
                return true;
            return false;
        }

        #endregion

        #region Articles
        public static List<Novum.Data.Os.Article> GetArticles(string department)
        {
            var osArticles = new List<Novum.Data.Os.Article>();
            var novArticles = Article.GetArticles(department);
            var modifierMenus = Modifier.GetModifierMenus(department);
            var menuModifiers = Modifier.GetMenuModifiers(department);
            Data.ModifierMenu modifierMenu = null;

            foreach (Data.Article novArticle in novArticles.Values)
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
                foreach (Data.MenuModifier menuModifier in menuModifiers)
                {
                    if (!menuModifier.MenuId.Equals(novArticle.MenuId))
                        continue;
                    if (!menuModifier.Row.Equals(novArticle.Row))
                        continue;
                    if (!menuModifier.Column.Equals(novArticle.Column))
                        continue;
                    if (osArticle.ModifierGroups == null)
                        osArticle.ModifierGroups = new List<Data.Os.ArticleModifierGroup>();

                    var articleModifierGroup = new Data.Os.ArticleModifierGroup();
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

        #region ModifierGroups
        public static List<Novum.Data.Os.ModifierGroup> GetModifierGroups(string department)
        {
            var modifierGroups = new List<Novum.Data.Os.ModifierGroup>();
            var modifierMenus = Modifier.GetModifierMenus(department);

            foreach (Data.ModifierMenu modifierMenu in modifierMenus.Values)
            {
                var modifierGroup = new Novum.Data.Os.ModifierGroup();
                modifierGroup.Id = modifierMenu.Id;
                modifierGroup.Name = modifierMenu.Name;
                modifierGroup.MinChoices = (int)modifierMenu.MinSelection;
                modifierGroup.MaxChoices = (int)modifierMenu.MaxSelection;
                modifierGroup.Question = "";
                modifierGroup.Type = Novum.Data.Os.ModifierGroup.ModifierType.PickOneEnum;
                modifierGroup.Choices = new List<Data.Os.ModifierChoice>();

                var modifiers = Modifier.GetModifiers(department, modifierMenu.Id);
                var lastModifierId = "";

                foreach (Data.Modifier modifier in modifiers.Values)
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

        #region Printer

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.Os.Printer> GetInvoicePrinters(string department)
        {
            var novPrinters = Printer.GetInvoicePrinters(department);
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

        #region PaymentMedia
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.Os.PaymentMedium> GetPaymentMedia(string department)
        {
            var novPaymentTypes = Payment.GetPaymentTypes(department);
            var osPaymentMedia = new List<Novum.Data.Os.PaymentMedium>();

            foreach (Novum.Data.PaymentType novPaymentType in novPaymentTypes.Values)
            {
                var osPaymentMedium = new Novum.Data.Os.PaymentMedium();
                osPaymentMedium.Id = novPaymentType.Id;
                osPaymentMedium.Name = novPaymentType.Name;
                osPaymentMedium.AllowOverPayment = 0;
                osPaymentMedia.Add(osPaymentMedium);
            }

            return osPaymentMedia;
        }
        #endregion
    }
}