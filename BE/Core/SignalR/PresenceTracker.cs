using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SignalR
{
    public class PresenceTracker
    {
        public static readonly Dictionary<string, List<string>> OnlineUsers =
            new Dictionary<string, List<string>>();

        public Task<bool> UserConnected(string id, string connectionId)
        {
            bool isOnline = false;
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(id))
                {
                    OnlineUsers[id].Add(connectionId);
                }
                else
                {
                    OnlineUsers.Add(id, new List<string> { connectionId });
                    isOnline = true;
                }
            }

            return Task.FromResult(isOnline);
        }

        public Task<bool> UserDisconnected(string id, string connectionId)
        {
            bool isOffline = false;
            lock (OnlineUsers)
            {
                if (!OnlineUsers.ContainsKey(id)) return Task.FromResult(isOffline);

                OnlineUsers[id].Remove(connectionId);
                if (OnlineUsers[id].Count == 0)
                {
                    OnlineUsers.Remove(id);
                    isOffline = true;
                }
            }
            return Task.FromResult(isOffline);
        }

        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }

        public Task<List<string>> GetConnectionsForUser(string id)
        {
            var connectionIds = new List<string>();
            lock (OnlineUsers)
            {
                var ids = OnlineUsers.GetValueOrDefault(id);
                if (ids != null) connectionIds.AddRange(ids);
            }
            return Task.FromResult(connectionIds);
        }
    }
}
