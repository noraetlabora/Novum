using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Nt.Data
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

        #endregion

        #region private fields

        private List<Nt.Data.Modifier> _modifiers;
        private const string _pipe = "|";

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string AssignmentTypeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public string ArticleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CourseMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CourseNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string CourseName { get; set; }

        /// <summary>
        /// Id of the order
        /// </summary>
        /// <value></value>
        public string Id
        {
            get
            {
                var id = new StringBuilder();
                id.Append(TableId).Append(_pipe);
                id.Append((int)Status).Append(_pipe);
                id.Append(ArticleId).Append(_pipe);
                id.Append(Line);
                return id.ToString();
            }
        }

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
        public string ReferenceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string SequenceNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public OrderStatus Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public string TableId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public int Line { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal TotalPrice
        {
            get { return decimal.Multiply(Quantity, UnitPrice); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public decimal UnitPrice { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Order()
        {

        }

        #endregion

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifier"></param>
        public void AddModifier(Nt.Data.Modifier modifier) 
        {
            if (_modifiers == null)
                _modifiers = new List<Modifier>();

            _modifiers.Add(modifier);
        }

        /// <summary>
        /// Remove All 
        /// </summary>
        public void ClearModifiers() 
        {
            _modifiers?.Clear();
        }

        #endregion
    }
}