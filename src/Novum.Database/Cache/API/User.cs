using System;
using Novum.Database.API;

namespace Novum.Database.Cache.API
{
    /// <summary>
    /// 
    /// </summary>
    public class User : IDbUser
    {
        public User()
        {
        }

        public void GetUser()
        {
            Novum.Log.Database.Info("--- start --- GetUser");
            Novum.Log.Database.Info("--- stop --- GetUser");
        }
    }
}