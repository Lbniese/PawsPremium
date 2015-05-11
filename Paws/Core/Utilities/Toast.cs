using Buddy.Overlay.Notifications;
using Styx;
using System;
using System.Windows;
using System.Windows.Media;

namespace Paws.Core.Utilities
{
    public static class Toast
    {
        /// <summary>
        /// Display a toast message to the user specific to ability chains notifications.
        /// </summary>
        public static void AbilityChain(string message)
        {
            StyxWoW.Overlay.AddToast(() =>
                {
                    return string.Format("The \"{0}\" Ability Chain is ready!", message);
                },
                TimeSpan.FromSeconds(3), Colors.AliceBlue, Colors.Bisque, new FontFamily("Arial"), FontWeights.Normal, 20
            );
        }
    }
}
