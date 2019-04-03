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
        public Dictionary<string, Novum.Data.Article> GetArticles(string department, string menuId)
        {
            var articles = new Dictionary<string, Novum.Data.Article>();
            var sql = string.Format("SELECT M.Anr, M.UMENU, M.ROW, M.COL, M.bez1, M.bgcolor, M.fgcolor, A.vkaend, A.nameaend FROM NT.TouchUMenuZeilen M LEFT JOIN WW.ANRKassa AS A ON (A.FA=M.FA AND A.ANR=M.ANR) WHERE M.FA = {0} AND M.UMENU = '{1}' AND M.ANR <> '' ", department, menuId);
            var dataTable = Interaction.GetDataTable(sql);

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
        public Dictionary<string, Novum.Data.Article> GetArticles(string department)
        {
            var articles = new Dictionary<string, Novum.Data.Article>();
            var sql = new StringBuilder();
            sql.Append(" SELECT M.Anr, M.UMENU, M.ROW, M.COL, M.bez1, M.bgcolor, M.fgcolor,");
            sql.Append(" A.vkaend, A.nameaend, P.PLU");
            sql.Append(" FROM NT.TouchUMenuZeilen M");
            sql.Append(" LEFT JOIN WW.ANRKassa AS A ON (A.FA=M.FA AND A.ANR=M.ANR)");
            sql.Append(" LEFT JOIN NT.PLUTabDet AS P ON (P.FA = M.FA AND P.ANR = M.ANR)");
            sql.Append(" WHERE M.FA = ").Append(department);
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
    }
}