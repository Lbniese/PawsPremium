using Paws.Core.Managers;
using Styx;
using Styx.CommonBot;
using Styx.WoWInternals.WoWObjects;
using System.Threading.Tasks;
using System.Linq;
using Shared = Paws.Core.Abilities.Shared;
using Guardian = Paws.Core.Abilities.Guardian;
using Feral = Paws.Core.Abilities.Feral;
using Styx.CommonBot.Coroutines;

namespace Paws.Core.Routines
{
    public static class Pull
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        public static async Task<bool> Rotation()
        {
            if (Me.Mounted || Me.IsInTravelForm())
            {
                if ( await CommonCoroutines.Dismount()) return true;
            }

            return await Combat.Rotation();
        }
    }
}
