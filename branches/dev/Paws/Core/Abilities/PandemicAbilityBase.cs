using Paws.Core.Conditions;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paws.Core.Abilities
{
    /// <summary>
    /// The base Ability class that abilities with pandemic criteria should inherit from. This class cannot be directly instantiated. 
    /// </summary>
    public abstract class PandemicAbilityBase : AbilityBase
    {
        public List<ICondition> PandemicConditions { get; set; }

        /// <summary>
        /// <para>Defines an instant use ability that is on the GCD</para>
        /// <para>mustWaitForGlobalCooldown = true</para>
        /// <para>mustWaitForSpellCooldown = false</para>
        /// </summary>
        public PandemicAbilityBase(WoWSpell spell, bool mustWaitForGlobalCooldown = true, bool mustWaitForSpellCooldown = false)
            : base(spell, mustWaitForGlobalCooldown, mustWaitForSpellCooldown)
        {
            this.PandemicConditions = new List<ICondition>();
        }

        /// <summary>
        /// (Non-Blocking) Casts the ability's spell on the specified target. The cast will only be attempted if the conditions list is completely satisfied first.
        /// </summary>
        /// <returns>Returns true on a successful cast.</returns>
        public override async Task<bool> CastOnTarget(WoWUnit target)
        {
            return
                (await CastManager.CastOnTarget(target, this, this.PandemicConditions)) ||
                (await base.CastOnTarget(target));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            this.PandemicConditions.Clear();
        }
    }
}
