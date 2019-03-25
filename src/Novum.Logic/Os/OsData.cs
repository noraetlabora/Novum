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
            foreach (var novCReason in novCReasons)
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

            foreach (Data.Menu menu in mainMenus)
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
            var menuItems = Menu.GetMenuItems(department, menuId);

            foreach (Data.MenuItem menuItem in menuItems)
            {
                //ignore all not supported menuItems 
                if (NotSupportedMenuItem(menuItem.Id))
                    continue;

                var contentEntry = new Novum.Data.Os.CategoryContentEntry();

                // submenu (eg. "$9")
                if (menuItem.Id.StartsWith("$"))
                {
                    var subMenuId = menuItem.Id.Substring(1);
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
                    contentEntry.ArticleId = menuItem.Id;
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
        public static List<Novum.Data.Os.Article> GetArticles()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ModifierGroups
        public static List<Novum.Data.Os.ModifierGroup> GetModifierGroups()
        {
            throw new NotImplementedException();
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

            foreach (Novum.Data.Printer novPrinter in novPrinters)
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

            foreach (Novum.Data.PaymentType novPaymentType in novPaymentTypes)
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