using System;
using Mirror;

namespace Common
{
    public class NetworkManagerWrapper : NetworkManager
    {
        public event Action OnServerStarted;
        public event Action OnClientStarted;
        public event Action OnClientConnected;
        public event Action<NetworkConnectionToClient> OnClientDisconnected;

        public override void OnStartServer()
        {
            base.OnStartServer();

            OnServerStarted?.Invoke();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            OnClientStarted?.Invoke();
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            OnClientConnected?.Invoke();
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);

            OnClientDisconnected?.Invoke(conn);
        }
    }
}
