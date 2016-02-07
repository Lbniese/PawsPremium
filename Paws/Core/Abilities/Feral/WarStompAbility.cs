using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>War Stomp (Tauren Racial)</para>
    ///     <para>0.5 sec cast, 2 min cooldown</para>
    ///     <para>Stuns up to 5 enemies within 8 yds for 2 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=20549/war-stomp</para>
    /// </summary>
    public class WarStompAbility : AbilityBase
    {
        public WarStompAbility()
            : base(WoWSpell.FromId(SpellBook.TaurenRacialWarStomp), true, true)
        {
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new MeIsFacingTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.TaurenWarStompEnabled));
            Conditions.Add(new AttackableTargetsMinCountCondition(Settings.TaurenWarStompMinEnemies));
            Conditions.Add(new MeIsInCatFormCondition());
            Conditions.Add(new MyTargetDistanceCondition(0, Settings.AoeRange));
        }
    }
}