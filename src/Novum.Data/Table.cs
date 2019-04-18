using System;

namespace Novum.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Table
    {

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

        /// <summary>
        /// 
        /// </summary>
        /// <value>"N"</value>
        public string AssignmentTypeId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Table()
        {

        }
        #endregion
    }
}