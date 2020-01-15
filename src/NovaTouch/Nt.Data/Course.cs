using System;

namespace Nt.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class Course
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Id 
        { 
            get
            {
                return GetId();   
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
        public int Menu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Number { get; set; }



        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public Course()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="number"></param>
        /// <param name="name"></param>
        public Course(int menu, int number, string name)
        {
            Menu = menu;
            Number = number;
            Name = name;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            return GetId(Menu.ToString(), Number.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetId(int menu, int number)
        {
            return GetId(menu.ToString(), number.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetId(string menu, string number)
        {
            return menu + "|" + number;
        }

    }

}