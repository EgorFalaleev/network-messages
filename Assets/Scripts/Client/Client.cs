using System;
using System.Collections.Generic;
using Common;
using VContainer.Unity;

namespace Client
{
    public class Client
        : IInitializable,
          IDisposable
    {
        private readonly IEnumerable<IMessageRegistrator> _messageRegistrators;
        private readonly NetworkManagerWrapper _networkManagerWrapper;
        private readonly IHandlerRegistry _handlerRegistry;

        public Client(NetworkManagerWrapper networkManagerWrapper,
            IEnumerable<IMessageRegistrator> messageRegistrators,
            IHandlerRegistry handlerRegistry)
        {
            _networkManagerWrapper = networkManagerWrapper;
            _messageRegistrators = messageRegistrators;
            _handlerRegistry = handlerRegistry;
        }

        public void Initialize()
        {
            _networkManagerWrapper.OnClientStarted += OnClientStarted;
            _networkManagerWrapper.OnClientConnected += OnClientConnected;
        }

        public void Dispose()
        {
            _networkManagerWrapper.OnClientStarted -= OnClientStarted;
            _networkManagerWrapper.OnClientConnected -= OnClientConnected;
        }

        private void OnClientStarted()
        {
            _handlerRegistry.ReplaceAllRegistrations();
        }

        private void OnClientConnected()
        {
            foreach (IMessageRegistrator registrator in _messageRegistrators)
            {
                registrator.Register();
            }
        }
    }
}
