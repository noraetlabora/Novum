using System;
using System.Collections.Generic;

namespace Novum.Logic.Os
{
    public class OsData
    {
        public static List<Novum.Data.Os.CancellationReason> GetCancellationReasons(string department)
        {
            var osCReasons = new List<Novum.Data.Os.CancellationReason>();
            var novCReasons = CancellationReason.GetCancellationReasons(department);
            foreach (var novCReason in novCReasons)
            {
                var osCReason = new Novum.Data.Os.CancellationReason();
                osCReason.Id = novCReason.Id;
                osCReason.Name = novCReason.Name;
                osCReasons.Add(osCReason);
            }
            return osCReasons;
        }
    }

}