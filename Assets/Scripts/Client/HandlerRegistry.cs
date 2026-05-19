using System;
using System.Collections.Generic;
using Mirror;

namespace Client
{
    public class HandlerRegistry
        : IHandlerRegistry,
          IDisposable
    {
        private readonly Dictionary<ushort, BaseMessageHandler> _messageHandlers = new();

        public void Register<T>(Action<T> handler)
            where T : struct, NetworkMessage
        {
            ushort typeId = NetworkMessageId<T>.Id;

            if (_messageHandlers.ContainsKey(typeId))
            {
                NetworkClient.ReplaceHandler(handler, requireAuthentication: false);
            }
            else
            {
                NetworkClient.RegisterHandler(handler, requireAuthentication: false);
            }

            _messageHandlers[typeId] = new MessageHandler<T>(handler);
        }

        public void ReplaceAllRegistrations()
        {
            foreach (BaseMessageHandler entry in _messageHandlers.Values)
            {
                entry.Replace();
            }
        }

        public void Dispose()
        {
            foreach (BaseMessageHandler entry in _messageHandlers.Values)
            {
                entry.Unregister();
            }
        }
    }
}
