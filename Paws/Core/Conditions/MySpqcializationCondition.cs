﻿using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player is in the specified specialization.
    /// </summary>
    public class MyExpectedSpecializationCondition : ICondition
    {
        public MyExpectedSpecializationCondition(WoWSpec expectedSpecialization)
        {
            ExpectedSpecialization = expectedSpecialization;
        }

        /// <summary>
        ///     The value used to determine if is in the provided specialization or not to satisfy the condition.
        /// </summary>
        public WoWSpec ExpectedSpecialization { get; set; }

        public bool Satisfied()
        {
            return StyxWoW.Me.Specialization == ExpectedSpecialization;
        }
    }
}