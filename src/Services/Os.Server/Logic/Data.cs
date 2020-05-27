using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Os.Server.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Data
    {

        #region private fields;
        internal static Dictionary<string, Models.Article> osCachedArticles;
        internal static List<Models.Category> osCachedCategories;
        internal static Dictionary<string, Nt.Data.PaymentType> ntCachedPaymentTypes;
        internal static Dictionary<string, Nt.Data.AssignmentType> ntCachedAssignmentTypes;
        private static readonly string[] notSupportedArticleIds = { "PLU", "$KONTO:", "$GUTSCHEIN:", "$GUTSCHEINBET:", "$RABATT:", "GANG:", "$FILTER:" };
        private static readonly string[] notSupportedModifierIds = { "VORWAHL:" };

        #endregion  //private fields

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        public static async Task CheckStaticData()
        {
            // snapshot time exists, data is up to date
            var staticDataChanged = await Nt.Database.DB.Api.Misc.StaticDataChanged(Controllers.OsHostController.PosStatus.SessionId).ConfigureAwait(false);
            if (staticDataChanged)
                return;

            Nt.Logging.Log.Server.Info("new static data available");
            var tasks = new List<Task>();
            tasks.Add(Logic.Data.GetArticles());
            tasks.Add(Logic.Data.GetCategories());
            tasks.Add(Logic.Data.GetModifierGroups());
            tasks.Add(Logic.Data.GetPaymentMedia());
            tasks.Add(Logic.Printer.GetPrinters());
            tasks.Add(Logic.Data.GetUsers());
            tasks.Add(Nt.Database.DB.Api.Misc.GetArticleGroups());
            tasks.Add(Nt.Database.DB.Api.Misc.GetTaxGroups());
            await Task.WhenAll(tasks).ConfigureAwait(false);

            // confirm changed Static Dat
            await Nt.Database.DB.Api.Misc.ConfirmChangedStaticData(Controllers.OsHostController.PosStatus.SessionId, "Os.Server").ConfigureAwait(false);
            Nt.Logging.Log.Server.Info("new static data cached");

            var pubSubMessages = new List<Client.Model.PubSubMessage>();
            var pubSubMessage = new Client.Model.PubSubMessage();
            pubSubMessage.Payload = "";
            pubSubMessages.Add(pubSubMessage);
            Client.ClientApi.Subscribe.PubsubTopicsPost("host_staticDataChanged", pubSubMessages);
        }

        #region Cancellation Reasons

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.CancellationReason>> GetCancellationReasons()
        {
            var osCancellationReasons = new List<Models.CancellationReason>();
            var ntCancellationReasons = await Nt.Database.DB.Api.Misc.GetCancellationReason().ConfigureAwait(false);
            foreach (var ntCancellationReason in ntCancellationReasons.Values)
            {
                var osCancellationReason = new Models.CancellationReason();
                osCancellationReason.Id = ntCancellationReason.Id;
                osCancellationReason.Name = ntCancellationReason.Name;
                osCancellationReasons.Add(osCancellationReason);
            }
            return osCancellationReasons;
        }

        internal static string GetClientId()
        {
            return Nt.Database.DB.Api.Pos.GetClientId();
        }

        #endregion

        #region Course

        public static async Task<List<Models.Course>> GetCourses()
        {
            var osCourses = new List<Models.Course>();
            var ntCourses = await Nt.Database.DB.Api.Misc.GetCourses().ConfigureAwait(false);
            foreach (var ntCourse in ntCourses.Values)
            {
                var osCourse = new Models.Course();
                osCourse.Id = ntCourse.Id;
                osCourse.Name = ntCourse.Name;
                osCourses.Add(osCourse);
            }
            return osCourses;
        }

        #endregion

        #region PaymentMedia
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.PaymentMedium>> GetPaymentMedia()
        {
            var osPaymentMedia = new List<Models.PaymentMedium>();
            ntCachedPaymentTypes = await Nt.Database.DB.Api.Payment.GetPaymentTypes().ConfigureAwait(false);
            foreach (var ntPaymentType in ntCachedPaymentTypes.Values)
            {
                var osPaymentMedium = GetPaymentMedium(ntPaymentType);
                osPaymentMedia.Add(osPaymentMedium);
            }

            ntCachedAssignmentTypes = await Nt.Database.DB.Api.Payment.GetAssignmentTypes().ConfigureAwait(false);
            foreach (var ntAssignmentType in ntCachedAssignmentTypes.Values)
            {
                var osPaymentMedium = new Models.PaymentMedium();
                osPaymentMedium.Id = ntAssignmentType.Id;
                osPaymentMedium.Name = ntAssignmentType.Name;
                osPaymentMedia.Add(osPaymentMedium);
            }

            return osPaymentMedia;
        }

        private static Models.PaymentMedium GetPaymentMedium(Nt.Data.PaymentType ntPaymentType)
        {
            var osPaymentMedium = new Models.PaymentMedium();
            osPaymentMedium.Id = ntPaymentType.Id;
            osPaymentMedium.Name = ntPaymentType.Name;

            switch (ntPaymentType.Program)
            {
                // payment room number
                case "^WKG2K7E":
                    osPaymentMedium.FullPaymentOnly = true;
                    osPaymentMedium.RequestInput = new Models.InputQueryPrompt();
                    osPaymentMedium.RequestInput.MethodManual = new Models.ManualInput();
                    osPaymentMedium.RequestInput.MethodManual.Lines = new List<Models.ManualInputLine>();
                    //Roomnumber
                    var manualInputLine = new Models.ManualInputLine();
                    manualInputLine.Key = "ROOM";
                    manualInputLine.Label = Resources.Dictionary.GetString("RoomNumber");
                    manualInputLine.Type = Models.ManualInputType.TextEnum;
                    osPaymentMedium.RequestInput.MethodManual.Lines.Add(manualInputLine);
                    break;
            }

            return osPaymentMedium;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Nt.Data.PaymentType GetPaymentType(string paymentTypeId)
        {
            if (string.IsNullOrEmpty(paymentTypeId))
                throw new Exception("payment type must not be null or empty");

            if (!ntCachedPaymentTypes.ContainsKey(paymentTypeId))
                return null;

            return ntCachedPaymentTypes[paymentTypeId];
        }

        /// <summary>
        /// 
        /// </summary>
        public static Nt.Data.AssignmentType GetAssignmentType(string assignmentTypeId)
        {
            if (string.IsNullOrEmpty(assignmentTypeId))
                throw new Exception("assignment type must not be null or empty");

            if (!ntCachedAssignmentTypes.ContainsKey(assignmentTypeId))
                return null;

            return ntCachedAssignmentTypes[assignmentTypeId];
        }

        #endregion

        #region Articles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Models.Article> GetCachedArticles()
        {
            return osCachedArticles.Values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.Article>> GetArticles()
        {
            var osArticles = new Dictionary<string, Models.Article>();

            var taskGetArticles = Nt.Database.DB.Api.Article.GetArticles();
            var taskGetModifierMenus = Nt.Database.DB.Api.Modifier.GetModifierMenus();
            var taskGetArticleModifierGroups = GetArticleModifierGroups();
            await Task.WhenAll(taskGetArticles, taskGetModifierMenus, taskGetArticleModifierGroups).ConfigureAwait(false);
            var ntArticles = taskGetArticles.Result;
            var ntModifierMenus = taskGetModifierMenus.Result;
            var osArticleModifierGroups = taskGetArticleModifierGroups.Result;

            foreach (var ntArticle in ntArticles.Values)
            {
                var osArticle = new Models.Article();

                osArticle.Id = ntArticle.Id;
                osArticle.Name = ntArticle.Name;
                // must enter price
                if (ntArticle.AskForPrice)
                    osArticle.MustEnterPrice = 1;
                // force show modifiers
                if (osArticleModifierGroups.ContainsKey(osArticle.Id))
                {
                    osArticle.ModifierGroups = osArticleModifierGroups[osArticle.Id].Values.ToList();

                    foreach (var modifierGroup in osArticle.ModifierGroups)
                    {
                        if (ntModifierMenus.ContainsKey(modifierGroup.ModifierGroupId))
                        {
                            if (ntModifierMenus[modifierGroup.ModifierGroupId].MinSelection > 0)
                                osArticle.ForceShowModifiers = true;
                        }
                    }
                }
                osArticles.Add(osArticle.Id, osArticle);
            }

            osCachedArticles = osArticles;
            return osArticles.Values.ToList();
        }
        #endregion  //Articles

        #region Categories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Models.Category> GetCachedCategories(string menuId)
        {
            return osCachedCategories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.Category>> GetCategories()
        {
            var posId = await Nt.Database.DB.Api.Pos.GetPosId().ConfigureAwait(false);
            var menuId = await Nt.Database.DB.Api.Menu.GetMenuId(posId).ConfigureAwait(false);
            return await GetCategories(menuId).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static async Task<List<Models.Category>> GetCategories(string menuId)
        {
            var osCategories = new List<Models.Category>();

            var taskGetMainMenus = Nt.Database.DB.Api.Menu.GetMainMenus(menuId);
            var taskGetMenus = Nt.Database.DB.Api.Menu.GetMenus();
            var taskGetMenuItems = Nt.Database.DB.Api.Menu.GetMenuItems();
            await Task.WhenAll(taskGetMainMenus, taskGetMenus, taskGetMenuItems).ConfigureAwait(false);
            var ntMainMenus = taskGetMainMenus.Result;
            var ntMenus = taskGetMenus.Result;
            var ntMenuItems = taskGetMenuItems.Result;

            foreach (var ntMainMenu in ntMainMenus.Values)
            {
                var osCategory = new Models.Category();
                osCategory.Name = ntMainMenu.Name;
                osCategory.Content = GetCategoryContent(ntMainMenu.Id, ntMenus, ntMenuItems, 0);

                osCategories.Add(osCategory);
            }

            osCachedCategories = osCategories;
            return osCategories;
        }

        #endregion  //Categories

        #region ModifierGroups

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.ModifierGroup>> GetModifierGroups()
        {
            var osModifierGroups = new List<Models.ModifierGroup>();
            Models.ModifierGroup osModifierGroup;

            var taskGetModiferMenus = Nt.Database.DB.Api.Modifier.GetModifierMenus();
            var taskGetModifierItems = Nt.Database.DB.Api.Modifier.GetModifierItems();
            await Task.WhenAll(taskGetModiferMenus, taskGetModifierItems).ConfigureAwait(false);
            var ntModifierMenus = taskGetModiferMenus.Result;
            var ntModifierItems = taskGetModifierItems.Result;

            //////////////////////////////////////
            // iterate Modifier Menu
            //////////////////////////////////////
            foreach (var ntModifierMenu in ntModifierMenus.Values)
            {
                osModifierGroup = new Models.ModifierGroup();
                osModifierGroup.Id = ntModifierMenu.Id;
                osModifierGroup.Name = ntModifierMenu.Name;
                osModifierGroup.MinChoices = (int)ntModifierMenu.MinSelection;
                osModifierGroup.MaxChoices = (int)ntModifierMenu.MaxSelection;
                osModifierGroup.Question = "";
                // type of modifier selection
                if (ntModifierMenu.MaxSelection.Equals(1))
                    osModifierGroup.Type = Models.ModifierGroup.ModifierType.PickOneEnum;
                else
                    osModifierGroup.Type = Models.ModifierGroup.ModifierType.PickMultipleEnum;

                osModifierGroup.Choices = new List<Models.ModifierChoice>();
                var lastModifierItemId = "";

                //////////////////////////////////////
                // Modifier Items / Choices
                //////////////////////////////////////
                foreach (var ntModifierItem in ntModifierItems.Values)
                {
                    // ignore not supported modifiers
                    if (ModifierIdNotSupported(ntModifierItem.Id))
                        continue;
                    // ignore two sequently equal modifiers (eg. rare and rare)
                    if (lastModifierItemId.Equals(ntModifierItem.Id))
                        continue;
                    lastModifierItemId = ntModifierItem.Id;

                    var modifierChoice = new Models.ModifierChoice();

                    modifierChoice.Id = ntModifierItem.Id;
                    modifierChoice.Name = ntModifierItem.Name;
                    modifierChoice.ReceiptName = ntModifierItem.Name;
                    modifierChoice.DefaultAmount = 0;
                    modifierChoice.MinAmount = (int)ntModifierItem.MinAmount;
                    modifierChoice.MaxAmount = (int)ntModifierItem.MaxAmount;

                    osModifierGroup.Choices.Add(modifierChoice);
                }
                osModifierGroups.Add(osModifierGroup);
            }

            //////////////////////////////////////
            // text input
            //////////////////////////////////////
            osModifierGroup = new Models.ModifierGroup();
            osModifierGroup.Id = "text";
            osModifierGroup.Name = Resources.Dictionary.GetString("ModifierText");
            osModifierGroup.Type = Models.ModifierGroup.ModifierType.TextInputEnum;
            osModifierGroups.Add(osModifierGroup);

            //////////////////////////////////////
            // fax input
            //////////////////////////////////////
            osModifierGroup = new Models.ModifierGroup();
            osModifierGroup.Id = "fax";
            osModifierGroup.Name = Resources.Dictionary.GetString("ModifierFax");
            osModifierGroup.Type = Models.ModifierGroup.ModifierType.FaxInputEnum;
            osModifierGroups.Add(osModifierGroup);

            return osModifierGroups;
        }

        #endregion

        #region Service Areas

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.ServiceArea>> GetServiceAreas()
        {
            var osServiceAreas = new List<Models.ServiceArea>();
            var currentPosId = await Nt.Database.DB.Api.Pos.GetPosId().ConfigureAwait(false);
            var posIds = await Nt.Database.DB.Api.Pos.GetAlternativePosIds(currentPosId).ConfigureAwait(false);
            var serviceAreas = new Dictionary<string, string>();

            var pos = await Nt.Database.DB.Api.Pos.GetPos(currentPosId).ConfigureAwait(false);
            var osServiceArea = new Models.ServiceArea();
            osServiceArea.Id = pos.Id;
            osServiceArea.Name = await Nt.Database.DB.Api.Pos.GetServiceAreaName(pos.ServiceAreaId).ConfigureAwait(false);
            serviceAreas.Add(pos.ServiceAreaId, osServiceArea.Name);

            foreach (var posId in posIds)
            {
                pos = await Nt.Database.DB.Api.Pos.GetPos(posId).ConfigureAwait(false);
                osServiceArea = new Models.ServiceArea();
                osServiceArea.Id = pos.Id;
                osServiceArea.Name = await Nt.Database.DB.Api.Pos.GetServiceAreaName(pos.ServiceAreaId).ConfigureAwait(false);
                if (!serviceAreas.ContainsKey(pos.ServiceAreaId))
                    osServiceAreas.Add(osServiceArea);
            }

            return osServiceAreas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        //good
        public static Task<string> GetServiceAreaId(string posId)
        {
            return Nt.Database.DB.Api.Pos.GetServiceAreaId(posId);
        }
        //bad
        public static async Task<string> GetServiceAreaIdAsync(string posId)
        {
            return await Nt.Database.DB.Api.Pos.GetServiceAreaId(posId).ConfigureAwait(false);
        }

        #endregion

        #region Users

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Models.User>> GetUsers()
        {
            var osUsers = new List<Models.User>();
            var ntWaiters = await Nt.Database.DB.Api.Waiter.GetWaiters().ConfigureAwait(false);
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
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static Task<string> GetPosId(string deviceId)
        {
            return Nt.Database.DB.Api.Pos.GetPosId(deviceId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceAreaId"></param>
        /// <returns></returns>
        public static Task<string> GetPriceLevel(string serviceAreaId)
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
        private static async Task<Dictionary<string, Dictionary<string, Models.ArticleModifierGroup>>> GetArticleModifierGroups()
        {
            var osModifierDictionary = new Dictionary<string, Dictionary<string, Models.ArticleModifierGroup>>();

            var taskGetMenuItems = Nt.Database.DB.Api.Menu.GetMenuItems();
            var taskGetMenuItemModifierMenus = Nt.Database.DB.Api.Modifier.GetMenuItemModifierMenus();
            await Task.WhenAll(taskGetMenuItems, taskGetMenuItemModifierMenus).ConfigureAwait(false);
            var ntMenuItems = taskGetMenuItems.Result;
            var ntMenuItemsModifierMenus = taskGetMenuItemModifierMenus.Result;

            // loop through all ntMenuItems again and tne modifierMenus
            foreach (var ntMenuItem in ntMenuItems)
            {
                foreach (var ntMenuItemsModifierMenu in ntMenuItemsModifierMenus)
                {
                    if (!ntMenuItemsModifierMenu.ContainsMenuItem(ntMenuItem))
                        continue;
                    //    
                    var osArticleModifierGroup = new Models.ArticleModifierGroup();
                    osArticleModifierGroup.ModifierGroupId = ntMenuItemsModifierMenu.ModifierMenuId;
                    AddArticleModifierGroup(osModifierDictionary, ntMenuItem.ArticleId, osArticleModifierGroup);
                }
            }

            // loop again through all ntMenuItems again and add text input and fax input
            // the second loop makes sure, that the text and fax modifiers are at the end of the list
            foreach (var ntMenuItem in ntMenuItems)
            {
                foreach (var ntMenuItemsModifierMenu in ntMenuItemsModifierMenus)
                {
                    if (!ntMenuItemsModifierMenu.ContainsMenuItem(ntMenuItem))
                        continue;
                    // text input
                    var osTextModifierMenu = new Models.ArticleModifierGroup();
                    osTextModifierMenu.ModifierGroupId = "text";
                    AddArticleModifierGroup(osModifierDictionary, ntMenuItem.ArticleId, osTextModifierMenu);
                    // fax input
                    var osFaxModifierMenu = new Models.ArticleModifierGroup();
                    osFaxModifierMenu.ModifierGroupId = "fax";
                    AddArticleModifierGroup(osModifierDictionary, ntMenuItem.ArticleId, osFaxModifierMenu);
                }
            }

            return osModifierDictionary;
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="osModifierDictionary"></param>
        /// <param name="articleId"></param>
        /// <param name="osArticleModifierGroup"></param>
        private static void AddArticleModifierGroup(Dictionary<string, Dictionary<string, Models.ArticleModifierGroup>> osModifierDictionary, string articleId, Models.ArticleModifierGroup osArticleModifierGroup)
        {
            if (osModifierDictionary.ContainsKey(articleId))
            {
                if (!osModifierDictionary[articleId].ContainsKey(osArticleModifierGroup.ModifierGroupId))
                    osModifierDictionary[articleId].Add(osArticleModifierGroup.ModifierGroupId, osArticleModifierGroup);
            }
            else
            {
                var osModifierList = new Dictionary<string, Models.ArticleModifierGroup>();
                osModifierList.Add(osArticleModifierGroup.ModifierGroupId, osArticleModifierGroup);
                osModifierDictionary.Add(articleId, osModifierList);
            }
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
        private static bool ModifierIdNotSupported(string modifierId)
        {
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
        private static List<Models.CategoryContentEntry> GetCategoryContent(string menuId, Dictionary<string, Nt.Data.Menu> ntMenus, List<Nt.Data.MenuItem> ntMenuItems, uint subMenu)
        {
            var categoryContent = new List<Models.CategoryContentEntry>();

            foreach (var ntMenuItem in ntMenuItems)
            {
                //
                if (!ntMenuItem.MenuId.Equals(menuId))
                    continue;
                //
                if (ArticleIdNotSupported(ntMenuItem.ArticleId))
                    continue;

                var osCategoryContentEntry = new Models.CategoryContentEntry();

                if (ntMenuItem.ArticleId.StartsWith("$"))
                {
                    // ignore sub/.../menu 
                    if (subMenu >= 1)
                        continue;

                    var subMenuId = ntMenuItem.ArticleId.Substring(1);

                    //// subMenuId not known
                    if (!ntMenus.ContainsKey(subMenuId))
                        continue;

                    osCategoryContentEntry.Category = new Models.Category();
                    osCategoryContentEntry.Category.Name = ntMenus[subMenuId].Name;

                    // dont add content if submenu equals current menuId
                    if (subMenuId.Equals(menuId))
                        osCategoryContentEntry.Category.Content = new List<Models.CategoryContentEntry>();
                    else
                        osCategoryContentEntry.Category.Content = GetCategoryContent(subMenuId, ntMenus, ntMenuItems, subMenu + 1);
                }
                else
                {
                    osCategoryContentEntry.ArticleId = ntMenuItem.ArticleId;
                }

                categoryContent.Add(osCategoryContentEntry);
            }

            return categoryContent;
        }

        #endregion  //private methods
    }
}