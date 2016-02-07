using System.Collections.Generic;
using System.Threading.Tasks;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Abilities
{
    /// <summary>
    ///     The base Ability class that abilities with pandemic criteria should inherit from. This class cannot be directly
    ///     instantiated.
    /// </summary>
    public abstract class PandemicAbilityBase : AbilityBase
    {
        /// <summary>
        ///     <para>Defines an instant use ability that is on the GCD</para>
        ///     <para>mustWaitForGlobalCooldown = true</para>
        ///     <para>mustWaitForSpellCooldown = false</para>
        /// </summary>
        public PandemicAbilityBase(WoWSpell spell, bool mustWaitForGlobalCooldown = true,
            bool mustWaitForSpellCooldown = false)
            : base(spell, mustWaitForGlobalCooldown, mustWaitForSpellCooldown)
        {
            PandemicConditions = new List<ICondition>();
        }

        public List<ICondition> PandemicConditions { get; set; }

        /// <summary>
        ///     (Non-Blocking) Casts the ability's spell on the specified target. The cast will only be attempted if the conditions
        ///     list is completely satisfied first.
        /// </summary>
        /// <returns>Returns true on a successful cast.</returns>
        public override async Task<bool> CastOnTarget(WoWUnit target)
        {
            return
                await CastManager.CastOnTarget(target, this, PandemicConditions) ||
                await base.CastOnTarget(target);
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            PandemicConditions.Clear();
            PandemicConditions.AddRange(RequiredConditions);
        }
    }
}