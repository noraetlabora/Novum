using System;
using System.Collections.Generic;

namespace Novum.Logic.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsData
    {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static List<Novum.Data.Os.Category> GetCategories(string department, string menuId)
        {
            var categories = new List<Novum.Data.Os.Category>();
            var mainMenus = Menu.GetMainMenu(department, menuId);

            foreach (Data.Menu menu in mainMenus)
            {
                var category = new Novum.Data.Os.Category();
                category.Name = menu.Name;

                var contentEntries = new List<Novum.Data.Os.CategoryContentEntry>();
                var menuItems = Menu.GetMenuItems(department, menu.Id);

                foreach (Data.MenuItem menuItem in menuItems)
                {
                    var contentEntry = new Novum.Data.Os.CategoryContentEntry();
                    contentEntry.ArticleId = menuItem.Id;
                    contentEntries.Add(contentEntry);
                }

                category.Content = contentEntries;
                categories.Add(category);
            }

            return categories;
        }
    }
}