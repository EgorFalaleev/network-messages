using Mirror;

namespace Server
{
    public interface ISubscriptionObserver
    {
        void ProcessSubscription(NetworkConnectionToClient connection, ushort typeId);
    }
}
