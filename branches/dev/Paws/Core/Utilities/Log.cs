using Styx.Common;
using System.Windows.Media;

namespace Paws.Core.Utilities
{
    /// <summary>
    /// Logging class.
    /// </summary>
    public static class Log
    {
        public static string LastCombatMessage;

        public static void AppendLine(string message, Color color)
        {
            if (message == LastCombatMessage)
                return;

            Logging.Write(color, string.Format("[Paws]: {0}", message));

            LastCombatMessage = message;
        }

        public static void Diagnostics(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Logging.WriteDiagnostic(Colors.Firebrick, string.Format("[Paws]: {0}", message));
        }

        public static void Combat(string message)
        {
            AppendLine(message, Colors.Orange);
        }

        public static void Equipment(string message)
        {
            AppendLine(message, Colors.LightSeaGreen);
        }

        public static void GUI(string message)
        {
            AppendLine(message, Colors.Aqua);
        }

        public static string Right(this string s, int c)
        {
            return s.Substring(c > s.Length ? 0 : s.Length - c);
        }
    }
}
