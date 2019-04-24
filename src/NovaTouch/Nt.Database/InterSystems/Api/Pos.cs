using Nt.Database.Api;

namespace Nt.Database.InterSystems.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal class Pos : IDbPos
    {
        public Pos() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetClientId() {
            return InterSystemsApi.ClientId;
        }

        public string GetPosId(string deviceId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetOmanKassa", InterSystemsApi.ClientId, deviceId);
        }

        public string GetServiceAreaId(string posId)
        {
            return Interaction.CallClassMethod("cmNT.Kassa", "GetVerkaufsort", InterSystemsApi.ClientId, posId);
        }

        public string GetServiceAreaName(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKOBez", InterSystemsApi.ClientId, sercieAreaId);
        }

        public string GetPriceLevel(string sercieAreaId)
        {
            return Interaction.CallClassMethod("cmWW.VKO", "GetVKPEbene", InterSystemsApi.ClientId, sercieAreaId);
        }

    }
}