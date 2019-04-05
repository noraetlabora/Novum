using System.Collections.Generic;
using System.Data;
using Novum.Database.Api;

namespace Novum.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Pos : IDbPos
    {
        public Pos()
        {
        }

        public string GetPosId(string deviceId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetOmanKassa", Data.Department, deviceId);
        }

        public string GetServiceAreaId(string posId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetVerkaufsort", Data.Department, posId);
        }

        public string GetServiceAreaName(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKOBez", Data.Department, sercieAreaId);
        }


    }
}