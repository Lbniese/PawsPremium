using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>War Stomp (Tauren Racial)</para>
    /// <para>0.5 sec cast, 2 min cooldown</para>
    /// <para>Stuns up to 5 enemies within 8 yds for 2 seconds.</para>
    /// <para>http://www.wowhead.com/spell=20549/war-stomp</para>
    /// </summary>
    public class WarStompAbility : AbilityBase
    {
        public WarStompAbility()
            : base(WoWSpell.FromId(SpellBook.TaurenRacialWarStomp), true, true)
        {
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new MeIsFacingTargetCondition());
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.TaurenWarStompEnabled));
            base.Conditions.Add(new AttackableTargetsMinCountCondition(Settings.TaurenWarStompMinEnemies));
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MyTargetDistanceCondition(0, Settings.AOERange));
        }
    }
}
