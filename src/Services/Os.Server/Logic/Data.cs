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
            var snapshotTimeExists = Task.Run(async () => await Nt.Database.DB.Api.Misc.HasSnapshotTime(Controllers.OsHostController.PosStatus.SessionId)).Result;
            if (snapshotTimeExists)
                return;

            Nt.Logging.Log.Server.Info("new static data available");
            var tasks = new List<Task>();
            tasks.Add(new Task(() => Logic.Data.GetArticles()));
            tasks.Add(new Task(() => Logic.Data.GetCategories()));
            tasks.Add(new Task(() => Logic.Data.GetModifierGroups()));
            tasks.Add(new Task(() => Logic.Data.GetPaymentMedia()));
            tasks.Add(new Task(() => Logic.Printer.GetPrinters()));
            tasks.Add(new Task(() => Logic.Data.GetUsers()));
            tasks.Add(Nt.Database.DB.Api.Misc.GetArticleGroups());
            tasks.Add(Nt.Database.DB.Api.Misc.GetTaxGroups());

            await Task.WhenAll(tasks);

            Nt.Logging.Log.Server.Info("new static data cached");

            var pubSubMessages = new List<Client.Model.PubSubMessage>();
            var pubSubMessage = new Client.Model.PubSubMessage();
            pubSubMessage.Payload = "";
            pubSubMessages.Add(pubSubMessage);
            Client.ClientApi.Subscribe.PubsubTopicsPost("host_staticDataChanged", pubSubMessages);

            // set snapshot time
            await Nt.Database.DB.Api.Misc.SetSnapshotTime(Controllers.OsHostController.PosStatus.SessionId);
        }

        #region Cancellation Reasons

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Models.CancellationReason> GetCancellationReasons()
        {
            var osCancellationReasons = new List<Models.CancellationReason>();
            var ntCancellationReasons = Task.Run(async () => await Nt.Database.DB.Api.Misc.GetCancellationReason()).Result;
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

        #region Course

        public static List<Models.Course> GetCourses()
        {
            var osCourses = new List<Models.Course>();
            var ntCourses = Task.Run(async () => await Nt.Database.DB.Api.Misc.GetCourses()).Result;
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
        public static List<Models.PaymentMedium> GetPaymentMedia()
        {
            var osPaymentMedia = new List<Models.PaymentMedium>();
            ntCachedPaymentTypes = Task.Run(async () => await Nt.Database.DB.Api.Payment.GetPaymentTypes()).Result;
            foreach (var ntPaymentType in ntCachedPaymentTypes.Values)
            {
                var osPaymentMedium = GetPaymentMedium(ntPaymentType);
                osPaymentMedia.Add(osPaymentMedium);
            }

            ntCachedAssignmentTypes = Task.Run(async () => await Nt.Database.DB.Api.Payment.GetAssignmentTypes()).Result;
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

            switch(ntPaymentType.Program)
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
                    manualInputLine.Label = Resources.Dictionary.GetString("Room_Number");
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
        public static List<Models.Article> GetArticles()
        {
            var osArticles = new Dictionary<string, Models.Article>();
            var ntArticles = Task.Run(async () => await Nt.Database.DB.Api.Article.GetArticles()).Result;
            var ntModifierMenus = Task.Run(async () => await Nt.Database.DB.Api.Modifier.GetModifierMenus()).Result;
            var osArticleModifierGroups = GetArticleModifierGroups();

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
        public static List<Models.Category> GetCategories()
        {
            var posId = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetPosId()).Result;
            var menuId = Task.Run(async () => await Nt.Database.DB.Api.Menu.GetMenuId(posId)).Result;
            return GetCategories(menuId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Models.Category> GetCategories(string menuId)
        {
            var osCategories = new List<Models.Category>();
            var ntMainMenus = Task.Run(async () => await Nt.Database.DB.Api.Menu.GetMainMenus(menuId)).Result;
            var ntMenus = Task.Run(async () => await Nt.Database.DB.Api.Menu.GetMenus()).Result;
            var ntMenuItems = Task.Run(async () => await Nt.Database.DB.Api.Menu.GetMenuItems()).Result;

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
        public static List<Models.ModifierGroup> GetModifierGroups()
        {
            var osModifierGroups = new List<Models.ModifierGroup>();
            var ntModifierMenus = Task.Run(async () => await Nt.Database.DB.Api.Modifier.GetModifierMenus()).Result;
            Models.ModifierGroup osModifierGroup;

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
                var ntModifierItems = Task.Run(async () => await Nt.Database.DB.Api.Modifier.GetModifierItems(ntModifierMenu.Id)).Result;
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
            osModifierGroup.Name = Resources.Dictionary.GetString("Modifier_Text");
            osModifierGroup.Type = Models.ModifierGroup.ModifierType.TextInputEnum;
            osModifierGroups.Add(osModifierGroup);

            //////////////////////////////////////
            // fax input
            //////////////////////////////////////
            osModifierGroup = new Models.ModifierGroup();
            osModifierGroup.Id = "fax";
            osModifierGroup.Name = Resources.Dictionary.GetString("Modifier_Fax");
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
        public static List<Models.ServiceArea> GetServiceAreas()
        {
            var osServiceAreas = new List<Models.ServiceArea>();
            var currentPosId = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetPosId()).Result;
            var posIds = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetAlternativePosIds(currentPosId)).Result;
            var serviceAreas = new Dictionary<string, string>();

            var pos = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetPos(currentPosId)).Result;
            var osServiceArea = new Models.ServiceArea();
            osServiceArea.Id = pos.Id;
            osServiceArea.Name = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetServiceAreaName(pos.ServiceAreaId)).Result;
            serviceAreas.Add(pos.ServiceAreaId, osServiceArea.Name);

            foreach (var posId in posIds)
            {
                pos = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetPos(posId)).Result;
                osServiceArea = new Models.ServiceArea();
                osServiceArea.Id = pos.Id;
                osServiceArea.Name = Task.Run(async () => await Nt.Database.DB.Api.Pos.GetServiceAreaName(pos.ServiceAreaId)).Result;
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
        public static string GetServiceAreaId(string posId)
        {
            return Task.Run(async () => await Nt.Database.DB.Api.Pos.GetServiceAreaId(posId)).Result;
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
            var ntWaiters = Task.Run(async () => await Nt.Database.DB.Api.Waiter.GetWaiters()).Result;
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
        public static string GetPosId(string deviceId)
        {
            return Task.Run(async () => await Nt.Database.DB.Api.Pos.GetPosId(deviceId)).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceAreaId"></param>
        /// <returns></returns>
        public static string GetPriceLevel(string serviceAreaId)
        {
            return Task.Run(async () => await Nt.Database.DB.Api.Pos.GetPriceLevel(serviceAreaId)).Result;
        }

        #endregion  //POS

        #endregion  // public methods

        #region private methods  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, Dictionary<string, Models.ArticleModifierGroup>> GetArticleModifierGroups()
        {
            var osModifierDictionary = new Dictionary<string, Dictionary<string, Models.ArticleModifierGroup>>();
            var ntMenuItems = Task.Run(async () => await Nt.Database.DB.Api.Menu.GetMenuItems()).Result;
            var ntMenuItemsModifierMenus = Task.Run(async () => await Nt.Database.DB.Api.Modifier.GetMenuItemModifierMenus()).Result;

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