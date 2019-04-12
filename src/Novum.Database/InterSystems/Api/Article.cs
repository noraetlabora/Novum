using System;
using System.Text;
using System.Data;
using System.Reflection;
using Novum.Database.Api;
using Novum.Data;
using System.Collections.Generic;
using InterSystems.Data.IRISClient;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Article : IDbArticle
    {
        /// <summary>
        /// 
        /// </summary>
        public Article()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Article> GetArticles(string menuId)
        {
            var articles = new Dictionary<string, Novum.Data.Article>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.Anr, M.UMENU, M.ROW, M.COL, M.bez1, M.bgcolor, M.fgcolor, A.vkaend, A.nameaend ");
            sql.Append(" FROM NT.TouchUMenuZeilen M ");
            sql.Append(" LEFT JOIN WW.ANRKassa AS A ON (A.FA=M.FA AND A.ANR=M.ANR) ");
            sql.Append(" WHERE M.FA = ").Append(Data.ClientId);
            sql.Append(" AND M.UMENU = ").Append(Interaction.SqlQuote(menuId));
            sql.Append(" AND M.ANR <> '' ");
            var dataTable = Interaction.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var article = new Novum.Data.Article();
                article.Id = DataObject.GetString(dataRow, "ANR");
                if (articles.ContainsKey(article.Id))
                {
                    Log.Database.Debug(MethodBase.GetCurrentMethod().Name + " already processed Id " + article.Id);
                    continue;
                }

                article.MenuId = DataObject.GetString(dataRow, "UMENU");
                article.Name = DataObject.GetString(dataRow, "bez1");
                article.Row = DataObject.GetUInt(dataRow, "ROW");
                article.Column = DataObject.GetUInt(dataRow, "COL");
                article.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                article.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                article.AskForPrice = DataObject.GetBool(dataRow, "vkaend");
                article.AskForName = DataObject.GetBool(dataRow, "nameaend");

                articles.Add(article.Id, article);
            }

            return articles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Dictionary<string, Novum.Data.Article> GetArticles()
        {
            var articles = new Dictionary<string, Novum.Data.Article>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.Anr, M.UMENU, M.ROW, M.COL, M.bez1, M.bgcolor, M.fgcolor,");
            sql.Append(" A.vkaend, A.nameaend, P.PLU");
            sql.Append(" FROM NT.TouchUMenuZeilen M");
            sql.Append(" LEFT JOIN WW.ANRKassa AS A ON (A.FA=M.FA AND A.ANR=M.ANR)");
            sql.Append(" LEFT JOIN NT.PLUTabDet AS P ON (P.FA = M.FA AND P.ANR = M.ANR)");
            sql.Append(" WHERE M.FA = ").Append(Data.ClientId);
            sql.Append(" AND ISNUMERIC(M.Anr) = 1");
            var dataTable = Interaction.GetDataTable(sql.ToString());

            var lastArticleId = "";

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var article = new Novum.Data.Article();

                article.Id = DataObject.GetString(dataRow, "Anr");

                if (article.Id.Equals(lastArticleId))
                    continue;
                lastArticleId = article.Id;

                if (articles.ContainsKey(article.Id))
                {
                    Log.Database.Debug(MethodBase.GetCurrentMethod().Name + " already processed Id " + article.Id);
                    continue;
                }

                article.Name = DataObject.GetString(dataRow, "bez1");
                article.ReceiptName = article.Name;
                article.MenuId = DataObject.GetString(dataRow, "UMENU");
                article.Row = DataObject.GetUInt(dataRow, "ROW");
                article.Column = DataObject.GetUInt(dataRow, "COL");
                article.BackgroundColor = DataObject.GetString(dataRow, "bgcolor");
                article.ForegroundColor = DataObject.GetString(dataRow, "fgcolor");
                article.AskForPrice = DataObject.GetBool(dataRow, "vkaend");
                article.AskForName = DataObject.GetBool(dataRow, "nameaend");
                article.Plu = DataObject.GetString(dataRow, "PLU");

                articles.Add(article.Id, article);
            }

            return articles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="articleId"></param>
        /// <param name="price"></param>
        public void CheckEnteredPrice(Session session, string articleId, decimal price)
        {
            var dbString = Interaction.CallClassMethod("cmNT.BonOman", "CheckArtikelpreis", session.ClientId, session.PosId, session.WaiterId, "tableId", articleId, price);
            var chPriceString = new Novum.Data.Utils.DataString(dbString);
            var chPriceArray = chPriceString.SplitByChar96();
            var chPriceList = new Novum.Data.Utils.DataList(chPriceArray);

            switch (chPriceList.GetString(0))
            {
                // 0 - entered price is ok
                case "0":
                    break;
                // 1 - entered price is lower than min price
                case "1":
                    throw new Exception(string.Format("entered price {0} for article {1} is lower than the min. price {2}", price, articleId, chPriceList.GetString(1)));
                // 2 - entered price is higher than max price
                case "2":
                    throw new Exception(string.Format("entered price {0} for article {1} is higher than the max. price {2}", price, articleId, chPriceList.GetString(1)));
                default:
                    break;
            }
        }

        public bool IsAvailable(Session session, string articleId)
        {
            //Interaction.CallVoidClassMethod("cmNT.BonOman", "CheckArtikelVerfuegbarkeit", session.Department, session.PosId, session.WaiterId, "", articleId, notOrderedQuantity, request.Quantity);
            return true;
        }

        internal static void CheckAvailibility(string availability)
        {
            switch (availability)
            {
                // 0 = available
                case "0":
                    break;
                // 1 = available, warning?
                case "1":
                    break;
                //2 = not available, error
                //3 = not available, ?
                default:
                    throw new Exception("article not available");
            }
        }
    }
}