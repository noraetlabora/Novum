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
            return Interaction.CallClassMethod("cmNT.Kassa", "GetOmanKassa", Data.ClientId, deviceId);
        }

        public string GetServiceAreaId(string posId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetVerkaufsort", Data.ClientId, posId);
        }

        public string GetServiceAreaName(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKOBez", Data.ClientId, sercieAreaId);
        }

        public string GetPriceLevel(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKPEbene", Data.ClientId, sercieAreaId);
        }


    }
}