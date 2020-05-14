using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Nt.Booking.Resources
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
            _resourceManager = new ResourceManager("Nt.Booking.Resources.Dictionary", Assembly.GetExecutingAssembly());
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        public static string GetString(string name, string cultureName)
        {
            var culturInfo = new CultureInfo(cultureName);
            return _resourceManager.GetString(name, culturInfo);
        }
    }
}
