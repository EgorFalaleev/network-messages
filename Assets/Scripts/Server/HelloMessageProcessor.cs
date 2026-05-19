using Common.Messages;
using Mirror;

namespace Server
{
    public class HelloMessageProcessor : ISubscriptionObserver
    {
        private readonly INetworkMessageSender _sender;

        public HelloMessageProcessor(INetworkMessageSender sender)
        {
            _sender = sender;
        }

        public void ProcessSubscription(NetworkConnectionToClient connection, ushort typeId)
        {
            if (typeId != NetworkMessageId<HelloMessage>.Id)
            {
                return;
            }

            _sender.SendToClient(
                connection,
                new HelloMessage
                {
                    Text = "Hello Client!"
                }
            );
        }
    }
}
