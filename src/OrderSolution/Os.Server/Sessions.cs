using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Os.Server
{
    /// <summary>
    /// 
    /// </summary>
    public static class Sessions
    {
        private static Dictionary<string, Nt.Data.Session> _sessions = new Dictionary<string, Nt.Data.Session>();

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static Nt.Data.Session GetSession(HttpRequest request)
        {
            var sessionId = request.Cookies["sessionId"];
            if (string.IsNullOrEmpty(sessionId))
                return null;
            if (!_sessions.ContainsKey(sessionId))
                return null;
            return _sessions[sessionId];
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
            _sessions[session.Id] = session;
            return true;
        }
    }
}
