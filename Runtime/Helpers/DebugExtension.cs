using UnityEngine;

namespace Sandbox.Helper
{
    public static class DebugExtension
    {
        const string Prefix = "<color={0}>";
        const string Suffix = "</color>";

        public static void Log(this object message) => Debug.Log(message);
        public static void LogWarning(this object message) => Debug.LogWarning(message);
        public static void LogError(this object message) => Debug.LogError(message);

        public static string Color(this object message, MessageColor color) =>
            string.Format(Prefix, color) + message.ToString() + Suffix;
    }

    public enum MessageColor
    { 
        red, 
        yellow, 
        green, 
        blue, 
        cyan, 
        magenta 
    }
}