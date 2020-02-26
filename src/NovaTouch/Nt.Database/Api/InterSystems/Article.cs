using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Database.Api.InterSystems
{
    /// <summary>
    /// 
    /// </summary>
    internal class Article : IDbArticle
    {
        /// <summary>
        /// 
        /// </summary>
        public Article() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, Nt.Data.Article>> GetArticles()
        {
            var articles = new Dictionary<string, Nt.Data.Article>();
            var sql = new StringBuilder();
            sql.Append(" SELECT A.ANR, A.vkbez, AK.vkaend, AK.nameaend ");
            sql.Append(" FROM WW.ANR A ");
            sql.Append(" LEFT JOIN WW.ANRKassa AK ON (AK.FA=A.FA AND AK.ANR=A.ANR) ");
            sql.Append(" WHERE A.passiv > ").Append(Intersystems.SqlToday);
            var dataTable = await Intersystems.Instance.GetDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var article = new Nt.Data.Article();
                article.Id = DataObject.GetString(dataRow, "Anr");
                article.Name = DataObject.GetString(dataRow, "vkbez");
                article.AskForPrice = DataObject.GetBool(dataRow, "vkaend");
                article.AskForName = DataObject.GetBool(dataRow, "nameaend");

                if (!articles.ContainsKey(article.Id))
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
        public async Task CheckEnteredPrice(Nt.Data.Session session, string articleId, decimal price)
        {
            var dbString = await Intersystems.Instance.CallClassMethod("cmNT.BonOman", "CheckArtikelpreis", session.ClientId, session.PosId, session.WaiterId, "tableId", articleId, price);
            var checkPriceString = new DataString(dbString);
            var checkPriceArray = checkPriceString.SplitByChar96();
            var checkPriceList = new DataList(checkPriceArray);

            switch (checkPriceList.GetString(0))
            {
                // 0 - entered price is ok
                case "0":
                    break;
                // 1 - entered price is lower than min price
                case "1":
                    throw new Exception(string.Format("entered price {0} for article {1} is lower than the min. price {2}", price, articleId, checkPriceList.GetString(1)));
                // 2 - entered price is higher than max price
                case "2":
                    throw new Exception(string.Format("entered price {0} for article {1} is higher than the max. price {2}", price, articleId, checkPriceList.GetString(1)));
                default:
                    break;
            }
        }

        public Task<bool> IsAvailable(Nt.Data.Session session, string articleId)
        {
            //Interaction.CallVoidClassMethod("cmNT.BonOman", "CheckArtikelVerfuegbarkeit", session.Department, session.PosId, session.WaiterId, "", articleId, notOrderedQuantity, request.Quantity);
            return Task.Run(() => true);
        }

        internal static void CheckAvailibility(string availability, string articleName)
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
                    throw new Exception(string.Format(Resources.Dictionary.GetString("Article_NotAvailable"), articleName));
            }
        }
    }
}