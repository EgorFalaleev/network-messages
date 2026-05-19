using System;
using Mirror;

namespace Client
{
    public abstract class BaseMessageHandler
    {
        public abstract void Replace();
        public abstract void Unregister();
    }

    public class MessageHandler<T> : BaseMessageHandler
        where T : struct, NetworkMessage
    {
        private readonly Action<T> _handler;

        public MessageHandler(Action<T> handler)
        {
            _handler = handler;
        }

        public override void Replace()
        {
            NetworkClient.ReplaceHandler(_handler, requireAuthentication: false);
        }

        public override void Unregister()
        {
            NetworkClient.UnregisterHandler<T>();
        }
    }
}
