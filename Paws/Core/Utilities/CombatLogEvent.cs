using Paws.Core.Managers;
using Styx.WoWInternals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws.Core.Utilities
{
    /// <summary>
    /// Encapsulates the base information of a combat log event.
    /// </summary>
    public class CombatLogEvent
    {
        public string EventType { get; set; }

        public CombatLogEventBaseArgs BaseArgs { get; set; }

        public CombatLogEvent(LuaEventArgs eventArgs)
        {
            this.EventType = eventArgs.EventName;

            BaseArgs = new CombatLogEventBaseArgs()
            {
                TimeStamp = Convert.ToInt32(eventArgs.Args[0]),
                Event = Convert.ToString(eventArgs.Args[1]),
                SourceUnitId = UnitManager.GuidToUnitID((string)eventArgs.Args[3]),
                SourceName = Convert.ToString(eventArgs.Args[4]),
                DestinationUnitId = UnitManager.GuidToUnitID((string)eventArgs.Args[7]),
                DestinationName = Convert.ToString(eventArgs.Args[8]),
            };
        }

        /// <summary>
        /// Helper method to retrieve the event type of the COMBAT_LOG_EVENT_UNFILTERED event.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetEvent(LuaEventArgs args)
        {
            return args.Args[1].ToString();
        }
    }

    /// <summary>
    /// The base arguments that all combat log events share.
    /// </summary>
    public struct CombatLogEventBaseArgs
    {
        public int TimeStamp { get; set; }
        public string Event { get; set; }
        public string SourceUnitId { get; set; }
        public string SourceName { get; set; }
        public string DestinationUnitId { get; set; }
        public string DestinationName { get; set; }
    }

    /// <summary>
    /// Provides additional information on a combat log event where the event is SPELL_CAST_SUCCESS
    /// </summary>
    public class SpellCastSuccess : CombatLogEvent
    {
        public string SpellName { get; set; }

        public SpellCastSuccess(LuaEventArgs eventArgs)
            : base(eventArgs)
        {
            this.SpellName = (string)eventArgs.Args[12];
        }
    }
}
