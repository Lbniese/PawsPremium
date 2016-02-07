using System.Threading.Tasks;
using Styx;
using Styx.CommonBot.Coroutines;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Routines
{
    public static class Pull
    {
        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        public static async Task<bool> Rotation()
        {
            if (!Me.Mounted && !Me.IsInTravelForm()) return await Combat.Rotation();
            if (await CommonCoroutines.Dismount()) return true;

            return await Combat.Rotation();
        }
    }
}