using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class GuiLogger
        : MonoBehaviour,
          ILogger
    {
        private readonly List<string> _messages = new();

        public void Log(string message)
        {
            if (_messages.Count > 15)
            {
                _messages.Clear();
            }
            
            _messages.Add(message);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(500, 20, 300, 400));

            foreach (string message in _messages)
            {
                GUILayout.Label(message);
            }

            GUILayout.EndArea();
        }
    }
}
