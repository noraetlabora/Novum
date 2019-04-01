using System;

namespace Novum.Data
{
    public class Table
    {
        enum Index
        {
            Id = 0,
            Name = 1,
            Amount = 2,
            Comment = 3,
            AmmountWithPrebookings = 5,
            Status = 6,
            WaiterId = 7,
            WaiterName = 8,
            Opened = 9,
            Updated = 10,
            Room = 11,
            Guests = 13,
            LeftTableId = 21,
            RightTableId = 22,
            AssignmentType = 23
        }

        #region Constructor
        public Table()
        {

        }

        public Table(string dbString)
        {
            if (string.IsNullOrEmpty(dbString))
                return;

            var dataString = new Utils.DataString(dbString);
            var dataList = new Utils.DataList(dataString.SplitByChar96());

            this.Id = dataList.GetString((int)Index.Id);
            this.Name = dataList.GetString((int)Index.Name);
            this.Amount = dataList.GetDecimal((int)Index.Amount);
            this.Comment = dataList.GetString((int)Index.Comment);
            this.WaiterId = dataList.GetString((int)Index.WaiterId);
            this.WaiterName = dataList.GetString((int)Index.WaiterName);
            this.Opend = dataList.GetDateTime((int)Index.Opened);
            this.Updated = dataList.GetDateTime((int)Index.Updated);
            this.Room = dataList.GetString((int)Index.Room);
            this.Guests = dataList.GetUInt((int)Index.Guests);
            this.LeftTableId = dataList.GetString((int)Index.LeftTableId);
            this.RightTableId = dataList.GetString((int)Index.RightTableId);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Id of the table which is the tablenumber with offset of service area
        /// </summary>
        /// <value>1005, 1006.1, 100002.SB.2</value>
        public string Id { get; set; }

        /// <summary>
        /// Name of the table
        /// </summary>
        /// <value>5, 6.1</value>
        public string Name { get; set; }

        /// <summary>
        /// Amount of the table which is the sum of the amount of all orderlines on the table
        /// </summary>
        /// <value>12,99</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// id of the waiter assigned to the table
        /// </summary>
        /// <value>1, 987</value>
        public string WaiterId { get; set; }

        /// <summary>
        /// name of the waiter assigned to the table
        /// </summary>
        /// <value>Chef de range</value>
        public string WaiterName { get; set; }

        /// <summary>
        /// comment of the table
        /// </summary>
        /// <value>company XYZ, birthday</value>
        public string Comment { get; set; }

        /// <summary>
        /// date and time where the table has been opend
        /// </summary>
        /// <value>2019-12-31 23:59:59</value>
        public DateTime Opend { get; set; }

        /// <summary>
        /// date and time where the table has been updated
        /// </summary>
        /// <value>2019-12-31 23:59:59</value>
        public DateTime Updated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Room { get; set; }

        /// <summary>
        /// number of guests on the table
        /// </summary>
        /// <value></value>
        public uint Guests { get; set; }

        /// <summary>
        /// left/previous table id if existing
        /// 1001.4 if current table is 1001.5
        /// 1001 if current table is 1001.1
        /// </summary>
        /// <value>1001.4, 1001</value>
        public string LeftTableId { get; set; }

        /// <summary>
        /// right/next table id if existing
        /// 1001.6 if current table is 1001.5
        /// </summary>
        /// <value>1001.6, 1008.1</value>
        public string RightTableId { get; set; }


        #endregion
    }
}