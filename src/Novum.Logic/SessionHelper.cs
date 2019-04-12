
using System;
using Novum.Data;

namespace Novum.Logic
{
    public class SessionHelper
    {
        public static Session Initialize(Session session, string serialNumber)
        {
            if (session == null)
                session = new Session();

            session.ClientId = Logic.Data.ClientId;
            session.SerialNumber = serialNumber;
            session.PosId = Database.DB.Api.Pos.GetPosId(serialNumber);
            session.ServiceAreaId = Database.DB.Api.Pos.GetServiceAreaId(session.PosId);
            session.PriceLevel = Database.DB.Api.Pos.GetPriceLevel(session.ServiceAreaId);
            session.WaiterId = "";
            return session;
        }
    }
}