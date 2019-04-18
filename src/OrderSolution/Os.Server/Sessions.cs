using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Os.Server
{
    /// <summary>
    /// 
    /// </summary>
    public static class Sessions
    {
        private static Dictionary<string, Nt.Data.Session> sessions = new Dictionary<string, Nt.Data.Session>();

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Nt.Data.Session GetSession(HttpRequest request)
        {
            var sessionId = request.Cookies["sessionId"];
            if (string.IsNullOrEmpty(sessionId))
                return null;
            if (!sessions.ContainsKey(sessionId))
                return null;
            return sessions[sessionId];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static bool SetSession(Nt.Data.Session session)
        {
            if (session == null)
                return false;
            if (string.IsNullOrEmpty(session.Id))
                return false;
            sessions[session.Id] = session;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static void Add(Nt.Data.Session session)
        {
            if (session == null)
            {
                Nt.Logging.Log.Server.Error("Sessions.Add|session is null");
                return;
            }
            if (string.IsNullOrEmpty(session.Id))
            {
                Nt.Logging.Log.Server.Error("Sessions.Add|session.Id is null or empty");
                return;
            }
            sessions.Add(session.Id, session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static bool Remove(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                Nt.Logging.Log.Server.Error("Sessions.Remove|sessionId is null or empty");
            }
            return sessions.Remove(sessionId);
        }
    }
}
