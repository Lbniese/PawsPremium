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
    public class CatFormAbility : AbilityBase
    {
        public CatFormAbility()
            : base(WoWSpell.FromId(SpellBook.CatForm))
        {
            base.Category = AbilityCategory.Buff;

            base.RequiredConditions.Add(new MeIsNotInCatFormCondition());
        }
        
        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.CatFormEnabled));
            base.Conditions.Add(
                new CombatSwitchCondition(
                    new ConditionDependencyList( // If we are in combat
                        new ConditionOrList(
                            new BooleanCondition(Settings.CatFormAlways),
                            new BooleanCondition(Settings.CatFormOnlyDuringPullOrCombat)
                        ),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.CatFormDoNotOverrideBearForm),
                            new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.BearForm)
                        ),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.CatFormDoNotOverrideTravelForm),
                            new ConditionDependencyList(
                                new MeIsNotInTravelFormCondition(),
                                new MeIsNotMovingCondition())
                        )
                    ),
                    new ConditionDependencyList( // If we are not in combat
                        new MeIsMovingCondition(),
                        new BooleanCondition(Settings.CatFormAlways),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.CatFormDoNotOverrideBearForm),
                            new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.BearForm)
                        ),
                        new BooleanCondition(!Settings.CatFormOnlyDuringPullOrCombat),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.CatFormDoNotOverrideTravelForm),
                            new MeIsNotInTravelFormCondition()
                        )
                    )
                )
            );
        }
    }
}
