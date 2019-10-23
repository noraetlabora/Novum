﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Nt.Database.Resources
{

    /// <summary>
    /// 
    /// </summary>
    public static class Dictionary
    {
        private static CultureInfo _culturInfo;
        private static ResourceManager _resourceManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cultureName"></param>
        public static void Initialize(string cultureName)
        {
            _resourceManager = new ResourceManager("Nt.Database.Resources.Dictionary", Assembly.GetExecutingAssembly());
            var culturInfo = new CultureInfo(cultureName);
            _culturInfo = culturInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString(string name)
        {
            return _resourceManager.GetString(name, _culturInfo);
        }
    }
}
