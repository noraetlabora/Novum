using System;

namespace Novum.Logic
{
    /// <summary>
    /// Helper class for Unix time conversion
    /// </summary>
    public static class Data
    {
        public static string Department
        {
            get { return Database.InterSystems.Data.Department; }
        }
    }
}