using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Novum.Server.Models.Os;

namespace Novum.Server.Controllers.Os
{
    /// <summary>
    /// 
    /// </summary>
    public class OsDataController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Articles")]
        public IActionResult GetArticles()
        {
            var articles = new List<Article>();
            //
            for (int i = 0; i < 20; i++)
            {
                var article = new Article();
                article.Id = i.ToString();
                article.Name = "Artikel " + i.ToString();
                article.Plu = "1000" + i.ToString();
                article.MustEnterPrice = 0;

                articles.Add(article);
            }
            //
            return new ObjectResult(articles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/CancellationReasons")]
        public IActionResult GetCancellationReasons()
        {
            var osCReasons = new List<CancellationReason>();
            List<Novum.Data.CancellationResason> novCReasons = Novum.Database.DB.Api.Misc.GetCancellationReason("1001");
            foreach (var novCReason in novCReasons)
            {
                var osCReason = new CancellationReason();
                osCReason.Id = novCReason.Id;
                osCReason.Name = novCReason.Name;
                osCReasons.Add(osCReason);
            }
            return new ObjectResult(osCReasons);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Categories")]
        public IActionResult GetCategories()
        {
            var categories = new List<Category>();
            //
            for (int i = 0; i < 20; i++)
            {
                var category = new Category();
                category.Name = "Kategorie " + i.ToString();

                categories.Add(category);
            }
            //
            return new ObjectResult(categories);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/ModifierGroups")]
        public IActionResult GetModifierGroups()
        {
            var modifierGroups = new List<ModifierGroup>();
            //
            for (int i = 0; i < 20; i++)
            {
                var modifierGroup = new ModifierGroup();
                modifierGroup.Id = i.ToString();
                modifierGroup.Name = "Änderergruppe " + i.ToString();
                modifierGroup.MinChoices = 1;
                modifierGroup.MaxChoices = 99;

                modifierGroups.Add(modifierGroup);
            }
            //
            return new ObjectResult(modifierGroups);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/OsConfiguration")]
        public IActionResult GetOsConfiguration()
        {
            var osConfiguration = new OsConfiguration();
            osConfiguration.Global = new Dictionary<string, string>();

            //
            for (int i = 0; i < 20; i++)
            {
                osConfiguration.Global["key" + i.ToString()] = "value" + i.ToString();
            }
            //
            return new ObjectResult(osConfiguration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/PaymentMedia")]
        public IActionResult GetPaymentMedia()
        {
            var paymentMedia = new List<PaymentMedium>();
            //
            for (int i = 0; i < 20; i++)
            {
                var paymentMedium = new PaymentMedium();
                paymentMedium.Id = i.ToString();
                paymentMedium.Name = "Zahlunsmedium " + i.ToString();

                paymentMedia.Add(paymentMedium);
            }
            //
            return new ObjectResult(paymentMedia);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Printers")]
        public IActionResult GetPrinters()
        {
            var printers = new List<Printer>();
            //
            for (int i = 0; i < 20; i++)
            {
                var printer = new Printer();
                printer.Name = "Drucker " + i.ToString();

                printers.Add(printer);
            }
            //
            return new ObjectResult(printers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/ServiceAreas")]
        public IActionResult GetServiceAreas()
        {
            var serviceAreas = new List<ServiceArea>();
            //
            for (int i = 0; i < 20; i++)
            {
                var serviceArea = new ServiceArea();
                serviceArea.Id = i.ToString();
                serviceArea.Name = "Ort " + i.ToString();

                serviceAreas.Add(serviceArea);
            }
            //
            return new OkObjectResult(serviceAreas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/api/v2/data/Users")]
        public IActionResult GetUsers()
        {
            var users = new List<User>();
            //
            for (int i = 0; i < 20; i++)
            {
                var user = new User();
                user.Id = i.ToString();
                user.Name = "Benutzer " + i.ToString();

                users.Add(user);
            }
            //
            return new ObjectResult(users);
        }

        /// <summary>
        /// Get the order lines possibly filtered by the sub table id. NOTE: Only commited order lines (where actions/tables/FinalizeOrder was called) will be returned by this call.
        /// </summary>
        /// <param name="subTableId">The sub table ID getting orderlines for.</param>
        /// <param name="status">[ "unknown", "ordered", "committed", "paid" ] If specified the status of the orderlines that are required. If not provided all orderlines with status Ordered and Committed are returned</param>
        /// <response code="200">Orderlines with their unpaid quantity/price</response>
        [HttpGet]
        [Route("/api/v2/data/OrderLines")]
        public IActionResult GetOrderLines([FromQuery]string subTableId, [FromQuery]string status)
        {
            var orderLines = new List<OrderLine>();
            //
            for (int i = 0; i < 20; i++)
            {
                var orderLine = new OrderLine();
                orderLine.Id = subTableId + "|" + i.ToString();
                orderLine.ArticleId = "ArtikelId " + i.ToString();
                orderLine.Quantity = 1;
                orderLine.SinglePrice = 299;
                orderLine.Status = OrderLine.OrderLineStatus.OrderedEnum;

                orderLines.Add(orderLine);
            }
            //
            return new ObjectResult(orderLines);
        }

        /// <summary>
        /// Get open tables for the service area.
        /// </summary>
        /// <param name="serviceAreaId">The service area id of the service area.</param>
        /// <response code="200">open tables array</response>
        [HttpGet]
        [Route("/api/v2/data/Tables")]
        public IActionResult GetTables([FromQuery]string serviceAreaId)
        {
            var tables = new List<TableResult>();
            //
            for (int i = 0; i < 20; i++)
            {
                var table = new TableResult();
                table.Id = i.ToString();
                table.Name = "Tisch " + i.ToString();
                table.ServiceAreaId = "0";
                table.BookedAmount = i * 100;
                table.LastActivityTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                //table.SubTables = "";

                tables.Add(table);
            }
            //
            return new ObjectResult(tables);
        }
    }
}