using Common.Messages;
using Mirror;
using Server;
using UnityEngine;
using VContainer;

namespace Demo
{
    public class SendMessageExample : MonoBehaviour
    {
        [Inject] private INetworkMessageSender _sender;

        void OnGUI()
        {
            if (!NetworkServer.active)
            {
                return;
            }

            GUILayout.BeginArea(new Rect(Screen.width - 260, 10, 250, 40));

            if (GUILayout.Button("Send HelloMessage to subscribers"))
            {
                _sender.SendToSubscribed(
                    new HelloMessage
                    {
                        Text = "Hello Client!"
                    }
                );
            }

            GUILayout.EndArea();
        }
    }
}
