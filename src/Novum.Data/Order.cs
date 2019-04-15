using System;
using System.Globalization;
using System.Text;

namespace Novum.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        #region Enums

        /// <summary>
        /// OrderStatus defines the status of an order (ordered, new order, prebooking)
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// The order is confirmed.
            /// </summary>
            Ordered = 0,
            /// <summary>
            /// The order is not yet confirmed, the kitchen or bar has not received the order.
            /// </summary>
            NewOrder = 10,
            /// <summary>
            /// The order keeps on the table, but the kitchen or bar has not received the order.
            /// </summary>
            Prebooked = 20
        }

        private enum Index
        {
            Id = 0,
            Quantity = 1,
            Status = 2,
            ArticleId = 3,
            Name = 4,
            UnitPrice = 5,
            Course = 8,
            CourseName = 16,
        }

        private enum IndexId
        {
            AssignmentType = 0,
            ArticleId = 1,
            UnitPrice = 2,
            Sort = 3
        }

        #endregion

        private const string Pipe = "|";

        #region Properties

        /// <summary>
        /// Id of the order
        /// </summary>
        /// <value></value>
        public string Id
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append((int)Status).Append(Pipe);
                sb.Append(CourseId.ToString()).Append(Pipe);
                //sb.Append(Sort.ToString()).Append(Pipe);
                sb.Append(ArticleId).Append(Pipe);
                sb.Append(UnitPrice.ToString(CultureInfo.InvariantCulture)).Append(Pipe);
                sb.Append(Name);
                return sb.ToString();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public OrderStatus Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public string ArticleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal TotalPrice
        {
            get { return decimal.Multiply(Quantity, UnitPrice); }
            set
            {
                if (value.Equals(decimal.Zero))
                    UnitPrice = decimal.Zero;
                else
                    UnitPrice = decimal.Divide(value, Quantity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CourseId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CourseName { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Order()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbString"></param>
        public Order(string dbString)
        {
            if (string.IsNullOrEmpty(dbString))
                return;

            var dataString = new Utils.DataString(dbString);
            var dataList = new Utils.DataList(dataString.SplitByChar96());

            // parsing the Id
            // var novId = dataList.GetString((int)Index.Id);
            // var novIdDataString = new Utils.DataString(novId);
            // var novIdDataList = new Utils.DataList(novIdDataString.SplitByDoublePipes());

            this.ArticleId = dataList.GetString((int)Index.ArticleId);
            this.Name = dataList.GetString((int)Index.Name);
            this.Quantity = dataList.GetDecimal((int)Index.Quantity);
            this.UnitPrice = dataList.GetDecimal((int)Index.UnitPrice);
            this.CourseId = dataList.GetString((int)Index.Course);
            this.CourseName = dataList.GetString((int)Index.CourseName);
            SetStatus(dataList.GetUInt((int)Index.Status));
        }

        #endregion        

        #region private methods

        private void SetStatus(uint status)
        {
            switch (status)
            {
                case (uint)OrderStatus.Ordered:
                    this.Status = OrderStatus.Ordered;
                    break;
                case (uint)OrderStatus.NewOrder:
                    this.Status = OrderStatus.NewOrder;
                    break;
                case (uint)OrderStatus.Prebooked:
                    this.Status = OrderStatus.Prebooked;
                    break;
                default:
                    this.Status = OrderStatus.Ordered;
                    break;
            }
        }

        #endregion
    }
}