using System;
using System.Text;

namespace Novum.Data
{
    public class Order
    {
        public enum OrderStatus
        {
            Ordered = 0,
            NewOrder = 10,
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

        #region Constructor
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
            this.Course = dataList.GetString((int)Index.Course);
            this.CourseName = dataList.GetString((int)Index.CourseName);
            SetStatus(dataList.GetUInt((int)Index.Status));
            SetId();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id of the order
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

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
        public string Course { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CourseName { get; set; }

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

        private const string Pipe = "|";
        private void SetId()
        {
            var sb = new StringBuilder();
            sb.Append((int)Status).Append(Pipe);
            sb.Append(Course.ToString()).Append(Pipe);
            //sb.Append(Sort.ToString()).Append(Pipe);
            sb.Append(ArticleId).Append(Pipe);
            sb.Append(UnitPrice).Append(Pipe);
            sb.Append(Name);
            this.Id = sb.ToString();
        }

        #endregion
    }
}