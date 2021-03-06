﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dragon.Common.Util;
using Dragon.Context.ReverseIPLookup;
using Dragon.Interfaces;

namespace Dragon.Context.Sessions
{
    public class InMemorySessionStore : SessionStoreBase
    {
        private static readonly ConcurrentDictionary<Guid, SessionRecord> s_sessions;

        static InMemorySessionStore()
        {
            s_sessions = new ConcurrentDictionary<Guid, SessionRecord>();
        }

        public InMemorySessionStore(ISession session, IReverseIPLookupService reverseLookupService)
            : base(session, reverseLookupService)
        {
        }

        protected override SessionRecord GetSessionRecord()
        {
            SessionRecord sessionRecord = null;
            while (sessionRecord == null)
            {
                // try to get record from memory
                sessionRecord = null;

                TryGetSessionRecord(m_session.ID, out sessionRecord);
                
                // if expired remove record
                if (sessionRecord.Expires <= DateTime.UtcNow)
                {
                    RemoveSessionRecord(m_session.ID);
                }
            }

            return sessionRecord;
        }

        protected virtual bool TryGetSessionRecord(Guid sessionID, out SessionRecord sessionRecord)
        {
            sessionRecord = new SessionRecord() { SessionID = m_session.ID };

            while (s_sessions.ContainsKey(sessionID))
            {
                if (s_sessions.TryGetValue(sessionID, out sessionRecord))
                {
                    return true;
                    break;
                }
            }

            return false;
        }

        protected virtual void RemoveSessionRecord(Guid sessionID)
        {
            SessionRecord dummy;
            while (s_sessions.ContainsKey(sessionID))
            {
                if (s_sessions.TryRemove(sessionID, out dummy))
                {
                    dummy = null;
                    break;
                }
            }
        }

        protected override void SaveSessionRecord(SessionRecord sessionRecord)
        {

            while (s_sessions.ContainsKey(sessionRecord.SessionID))
            {
                SessionRecord oldSessionRecord;
                s_sessions.TryRemove(sessionRecord.SessionID, out oldSessionRecord);
            }
            while (!s_sessions.ContainsKey(sessionRecord.SessionID))
            {
                s_sessions.TryAdd(sessionRecord.SessionID, sessionRecord);
            }
        }

    }
}
