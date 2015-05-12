﻿using Paws.Core.Managers;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's current target is greater than the specified minimum height.
    /// </summary>
    public class MyTargetIsTooHighCondition : ICondition
    {
        /// <summary>
        /// The required minimum height to satisfy the condition.
        /// </summary>
        public float MinHeight { get; set; }

        public MyTargetIsTooHighCondition(float minHeight)
        {
            this.MinHeight = minHeight;
        }

        public bool Satisfied()
        {
            if (Main.MyCurrentTarget == null || !Main.MyCurrentTarget.IsValid)
                return false;

            return (StyxWoW.Me.CurrentTarget.Z > (StyxWoW.Me.Z + this.MinHeight));
        }
    }
}
