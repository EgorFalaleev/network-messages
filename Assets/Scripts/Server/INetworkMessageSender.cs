using Mirror;

namespace Server
{
    public interface INetworkMessageSender
    {
        void SendToClient<T>(NetworkConnectionToClient connection, T message) where T : struct, NetworkMessage;
        void SendToSubscribed<T>(T message) where T : struct, NetworkMessage;
    }
}
