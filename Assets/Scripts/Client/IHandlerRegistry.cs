using System;
using Mirror;

namespace Client
{
    public interface IHandlerRegistry
    {
        void Register<T>(Action<T> handler) where T : struct, NetworkMessage;
        void ReplaceAllRegistrations();
    }
}
