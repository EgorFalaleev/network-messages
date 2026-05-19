using System.Collections.Generic;
using Mirror;

namespace Server
{
    public interface ISubscriptionsRepository
    {
        void AddSubscription(ushort typeId, NetworkConnectionToClient connection);
        void RemoveSubscription(NetworkConnectionToClient connection);

        bool HasSubscription<T>(NetworkConnectionToClient connection)
            where T : struct, NetworkMessage;

        IReadOnlyCollection<NetworkConnectionToClient> GetSubscribers<T>()
            where T : struct, NetworkMessage;
    }
}
