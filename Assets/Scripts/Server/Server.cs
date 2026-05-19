using System;
using System.Collections.Generic;
using Common;
using Common.Messages;
using Mirror;
using VContainer.Unity;

namespace Server
{
    public class Server
        : IInitializable,
          IDisposable
    {
        private readonly IEnumerable<ISubscriptionObserver> _observers;
        private readonly NetworkManagerWrapper _networkManagerWrapper;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public Server(NetworkManagerWrapper networkManagerWrapper,
            ISubscriptionsRepository subscriptionsRepository,
            IEnumerable<ISubscriptionObserver> observers)
        {
            _networkManagerWrapper = networkManagerWrapper;
            _subscriptionsRepository = subscriptionsRepository;
            _observers = observers;
        }

        public void Initialize()
        {
            _networkManagerWrapper.OnServerStarted += OnServerStarted;
            _networkManagerWrapper.OnClientDisconnected += OnClientDisconnected;

            NetworkServer.RegisterHandler<SubscriptionMessage>(
                OnSubscriptionMessageReceived
            );
        }

        public void Dispose()
        {
            _networkManagerWrapper.OnServerStarted -= OnServerStarted;
            _networkManagerWrapper.OnClientDisconnected -= OnClientDisconnected;
            NetworkServer.UnregisterHandler<SubscriptionMessage>();
        }

        private void OnServerStarted()
        {
            NetworkServer.ReplaceHandler<SubscriptionMessage>(
                OnSubscriptionMessageReceived
            );
        }

        private void OnSubscriptionMessageReceived(NetworkConnectionToClient connection,
            SubscriptionMessage message)
        {
            _subscriptionsRepository.AddSubscription(message.MessageTypeId, connection);

            foreach (ISubscriptionObserver observer in _observers)
            {
                observer.ProcessSubscription(connection, message.MessageTypeId);
            }
        }

        private void OnClientDisconnected(NetworkConnectionToClient conn)
        {
            _subscriptionsRepository.RemoveSubscription(conn);
        }
    }
}
