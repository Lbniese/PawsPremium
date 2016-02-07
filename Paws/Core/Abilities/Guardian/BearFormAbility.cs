using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class BearFormAbility : AbilityBase
    {
        public BearFormAbility()
            : base(WoWSpell.FromId(SpellBook.BearForm))
        {
            Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.BearFormEnabled));
            Conditions.Add(new MeIsNotInBearFormCondition());
            Conditions.Add(
                new CombatSwitchCondition(
                    new ConditionDependencyList( // If we are in combat
                        new ConditionOrList(
                            new BooleanCondition(Settings.BearFormAlways),
                            new BooleanCondition(Settings.BearFormOnlyDuringPullOrCombat)
                            ),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.BearFormDoNotOverrideCatForm),
                            new MeIsNotInCatFormCondition()
                            ),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.BearFormDoNotOverrideTravelForm),
                            new ConditionDependencyList(
                                new MeIsNotInTravelFormCondition(),
                                new MeIsNotMovingCondition())
                            )
                        ),
                    new ConditionDependencyList( // If we are not in combat
                        new MeIsMovingCondition(),
                        new BooleanCondition(Settings.BearFormAlways),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.BearFormDoNotOverrideCatForm),
                            new MeIsNotInCatFormCondition()
                            ),
                        new BooleanCondition(!Settings.BearFormOnlyDuringPullOrCombat),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.BearFormDoNotOverrideTravelForm),
                            new MeIsNotInTravelFormCondition()
                            )
                        )
                    )
                );
        }
    }
}