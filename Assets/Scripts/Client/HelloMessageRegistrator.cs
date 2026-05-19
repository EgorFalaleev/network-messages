using Common;
using Common.Messages;
using Mirror;

namespace Client
{
    public class HelloMessageRegistrator : IMessageRegistrator
    {
        private readonly IHandlerRegistry _handlerRegistry;
        private readonly ILogger _logger;

        public HelloMessageRegistrator(IHandlerRegistry handlerRegistry,
            ILogger logger)
        {
            _handlerRegistry = handlerRegistry;
            _logger = logger;
        }

        public void Register()
        {
            _handlerRegistry.Register<HelloMessage>(message => _logger.Log(message.Text));

            NetworkClient.Send(
                new SubscriptionMessage
                {
                    MessageTypeId = NetworkMessageId<HelloMessage>.Id
                }
            );
        }
    }
}
