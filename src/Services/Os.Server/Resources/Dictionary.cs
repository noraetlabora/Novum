using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Os.Server.Resources
{

    public static class Dictionary
    {
        private static CultureInfo _culturInfo;
        private static ResourceManager _resourceManager;

        public static void Initialize(string cultureName = "de-AT")
        {
            _resourceManager = new ResourceManager("Os.Server.Resources.Dictionary", Assembly.GetExecutingAssembly());
            var culturInfo = new CultureInfo(cultureName);
            _culturInfo = culturInfo;
        }

        public static string GetString(string name)
        {
            return _resourceManager.GetString(name, _culturInfo);
        }

        public static string GetString(string name, string cultureName)
        {
            var culturInfo = new CultureInfo(cultureName);
            return _resourceManager.GetString(name, culturInfo);
        }
    }
}
