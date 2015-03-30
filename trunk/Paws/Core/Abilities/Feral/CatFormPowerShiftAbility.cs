using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para></para>
    /// <para>Cat Form</para>
    /// <para>7.4% of base mana</para>
    /// <para>Instant</para>
    /// <para>Requires Druid</para>
    /// <para>Requires Level 6</para>
    /// <para>Shapeshift into Cat Form, increasing movement speed by 30%</para>
    /// <para>and allowing the use of Cat Form abilities. Also protects the</para>
    /// <para>caster from Polymorph effects and reduces damage taken from</para>
    /// <para>falling.</para>
    /// <para></para>
    /// <para>http://www.wowhead.com/spell=768/cat-form"</para>
    /// </summary>
    public class CatFormPowerShiftAbility : AbilityBase
    {
        public CatFormPowerShiftAbility()
            : base(WoWSpell.FromId(SpellBook.CatForm))
        {
            base.Category = AbilityCategory.Buff;

            // No conditions.
        }
    }
}
