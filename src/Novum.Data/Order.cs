using System;

namespace Novum.Data
{
    public class Order
    {
        public enum Status
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
        public Order()
        {

        }

        public Order(string dbString)
        {
            if (string.IsNullOrEmpty(dbString))
                return;

            var dataString = new Utils.DataString(dbString);
            var dataList = new Utils.DataList(dataString.SplitByChar96());

            var novId = dataList.GetString((int)Index.Id);
            var novIdDataString = new Utils.DataString(novId);
            var novIdDataList = new Utils.DataList(novIdDataString.SplitByDoublePipes());

        }
        #endregion

        #region Properties

        /// <summary>
        /// Id of the table which is the tablenumber with offset of service area
        /// </summary>
        /// <value>1005, 1006.1, 100002.SB.2</value>
        public string Id { get; set; }





        #endregion
    }
}