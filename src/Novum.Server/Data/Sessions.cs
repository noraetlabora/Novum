using System;
using System.Collections.Generic;
using Novum.Data;

namespace Novum.Server.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Sessions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>   
        public static Sessions Instance { get { return lazy.Value; } }
        private static readonly Lazy<Sessions> lazy = new Lazy<Sessions>(() => new Sessions());

        private Dictionary<string, Session> sessions = new Dictionary<string, Session>();

        private Sessions()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Session GetSession(string sessionId)
        {
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
        public bool SetSession(Session session)
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
        public void Add(Session session)
        {
            if (session == null)
            {
                Log.Server.Error("Sessions.Add|session is null");
                return;
            }
            if (string.IsNullOrEmpty(session.Id))
            {
                Log.Server.Error("Sessions.Add|session.Id is null or empty");
                return;
            }
            sessions.Add(session.Id, session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool Remove(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                Log.Server.Error("Sessions.Remove|sessionId is null or empty");
            }
            return sessions.Remove(sessionId);
        }
    }
}