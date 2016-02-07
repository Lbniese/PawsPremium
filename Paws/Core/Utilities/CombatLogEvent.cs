using System;
using Paws.Core.Managers;
using Styx.WoWInternals;

namespace Paws.Core.Utilities
{
    /// <summary>
    ///     Encapsulates the base information of a combat log event.
    /// </summary>
    public class CombatLogEvent
    {
        public CombatLogEvent(LuaEventArgs eventArgs)
        {
            EventType = eventArgs.EventName;

            BaseArgs = new CombatLogEventBaseArgs
            {
                TimeStamp = Convert.ToInt32(eventArgs.Args[0]),
                Event = Convert.ToString(eventArgs.Args[1]),
                SourceUnitId = UnitManager.GuidToUnitId((string) eventArgs.Args[3]),
                SourceName = Convert.ToString(eventArgs.Args[4]),
                DestinationUnitId = UnitManager.GuidToUnitId((string) eventArgs.Args[7]),
                DestinationName = Convert.ToString(eventArgs.Args[8])
            };
        }

        public string EventType { get; set; }

        public CombatLogEventBaseArgs BaseArgs { get; set; }

        /// <summary>
        ///     Helper method to retrieve the event type of the COMBAT_LOG_EVENT_UNFILTERED event.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetEvent(LuaEventArgs args)
        {
            return args.Args[1].ToString();
        }
    }

    /// <summary>
    ///     The base arguments that all combat log events share.
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
    ///     Provides additional information on a combat log event where the event is SPELL_CAST_SUCCESS
    /// </summary>
    public class SpellCastSuccess : CombatLogEvent
    {
        public SpellCastSuccess(LuaEventArgs eventArgs)
            : base(eventArgs)
        {
            SpellName = (string) eventArgs.Args[12];
        }

        public string SpellName { get; set; }
    }
}