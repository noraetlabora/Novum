using System;

namespace Novum.Data
{
    public class Article
    {

        #region Constructor
        public Article()
        {

        }

        public Article(string id, string name, string bgColor, string fgColor)
        {
            Id = id;
            Name = name;
            BackgroundColor = bgColor;
            ForegroundColor = fgColor;
        }
        #endregion

        #region Properties

        public string Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }

        #endregion


    }

}