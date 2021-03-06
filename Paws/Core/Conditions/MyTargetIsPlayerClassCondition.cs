﻿using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player's current target is a player and the specified class.
    /// </summary>
    public class MyTargetIsPlayerClassCondition : ICondition
    {
        public MyTargetIsPlayerClassCondition(WoWClass playerClass)
        {
            PlayerClass = playerClass;
        }

        /// <summary>
        ///     The player class required to satisfy the condition.
        /// </summary>
        public WoWClass PlayerClass { get; protected set; }

        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("Current target cannot be null or invalid.");

            return
                StyxWoW.Me.CurrentTarget.IsPlayer &&
                StyxWoW.Me.CurrentTarget.Class == PlayerClass;
        }
    }
}