using System.Collections.Generic;
using Mirror;

namespace Server
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly Dictionary<ushort, HashSet<NetworkConnectionToClient>>
            _subscriptions = new();

        public void AddSubscription(ushort typeId, NetworkConnectionToClient connection)
        {
            if (!_subscriptions.ContainsKey(typeId))
            {
                _subscriptions[typeId] = new HashSet<NetworkConnectionToClient>();
            }

            _subscriptions[typeId]
               .Add(connection);
        }

        public bool HasSubscription<T>(NetworkConnectionToClient connection)
            where T : struct, NetworkMessage
        {
            HashSet<NetworkConnectionToClient> connections = GetConnections<T>();

            return connections != null && connections.Contains(connection);
        }

        public IReadOnlyCollection<NetworkConnectionToClient> GetSubscribers<T>()
            where T : struct, NetworkMessage
        {
            return GetConnections<T>();
        }

        public void RemoveSubscription(NetworkConnectionToClient connection)
        {
            foreach (HashSet<NetworkConnectionToClient> set in _subscriptions.Values)
            {
                set.Remove(connection);
            }
        }

        private HashSet<NetworkConnectionToClient> GetConnections<T>()
            where T : struct, NetworkMessage
        {
            _subscriptions.TryGetValue(
                NetworkMessageId<T>.Id,
                out HashSet<NetworkConnectionToClient> connections
            );

            return connections;
        }
    }
}
