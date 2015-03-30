using Paws.Core;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paws.Core.Utilities
{
    public class Events
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }

        public Events()
        {
            Lua.Events.AttachEvent("COMBAT_LOG_EVENT_UNFILTERED", new LuaEventHandlerDelegate(CombatLogHandler));
        }

        public void CombatLogHandler(object self, LuaEventArgs args)
        {
            if (CombatLogEvent.GetEvent(args) == "SPELL_CAST_SUCCESS")
            {
                //Log.GUI(string.Format("0:{0}, 1:{1}, 2:{2}, 3:{3}, 4:{4}, 5:{5}, 6:{6}, 7:{7}",
                //    args.Args[0], args.Args[1], args.Args[2], args.Args[3], args.Args[4], args.Args[5], args.Args[6], args.Args[7]));
                var combatLogEvent = new SpellCastSuccess(args);
                if (combatLogEvent.SpellName == "Rake" && combatLogEvent.BaseArgs.SourceUnitId == Me.GetUnitId())
                {
                    if (MyCurrentTarget != null && (MyCurrentTarget.GetUnitId() == combatLogEvent.BaseArgs.DestinationUnitId))
                    {
                        // Added this routine to catch instances of rake casts that the user may have initiated so that rolling
                        // bleed modifiers are properly applied.
                        SnapshotManager.Instance.AddRakedTarget(MyCurrentTarget);
                    }
                }
            }
        }
    }
}
