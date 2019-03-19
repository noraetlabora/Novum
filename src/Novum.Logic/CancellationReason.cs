using System;
using System.Collections.Generic;

namespace Novum.Logic
{
    public class CancellationReason
    {
        public static List<Novum.Data.CancellationResason> GetCancellationReasons(string department)
        {
            return Novum.Database.DB.Api.Misc.GetCancellationReason(department);
        }
    }
}