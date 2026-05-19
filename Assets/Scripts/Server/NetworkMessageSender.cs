using System.Collections.Generic;
using Mirror;

namespace Server
{
    public class NetworkMessageSender : INetworkMessageSender
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public NetworkMessageSender(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public void SendToClient<T>(NetworkConnectionToClient connection, T message)
            where T : struct, NetworkMessage
        {
            if (!_subscriptionsRepository.HasSubscription<T>(connection))
            {
                return;
            }

            connection.Send(message);
        }

        public void SendToSubscribed<T>(T message)
            where T : struct, NetworkMessage
        {
            IReadOnlyCollection<NetworkConnectionToClient> subscribers =
                _subscriptionsRepository.GetSubscribers<T>();

            if (subscribers == null
             || subscribers.Count == 0)
            {
                return;
            }

            foreach (NetworkConnectionToClient connection in subscribers)
            {
                connection.Send(message);
            }
        }
    }
}
