using UnityEngine;

namespace Common
{
    public class UnityDebugLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}
