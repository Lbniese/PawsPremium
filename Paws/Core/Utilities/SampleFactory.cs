using System.Collections.Generic;
using System.Windows.Forms;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Shared;
using Paws.Core.Conditions;
using Styx;
using Styx.Common;

namespace Paws.Core.Utilities
{
    public static class SampleFactory
    {
        public static List<AbilityChain> CreateAbilityChainsSampleList()
        {
            // Create a default abilities list...
            var sampleChains = new List<AbilityChain>();

            // Ability chain sample: Burst Damage
            var sampleBurstChain = new AbilityChain("Burst Damage");

            sampleBurstChain.Specialization = WoWSpec.DruidFeral;
            sampleBurstChain.HotKey = Keys.F;
            sampleBurstChain.ModiferKey = ModifierKeys.Control;
            sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new IncarnationAbility(), TargetType.Me, true));
            sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new BerserkAbility(), TargetType.Me, true));

            sampleChains.Add(sampleBurstChain);

            // Ability chain sample: Burst Damage
            var sampleDefenseChain = new AbilityChain("HotW Defense");

            sampleDefenseChain.Specialization = WoWSpec.DruidFeral;
            sampleDefenseChain.HotKey = Keys.D;
            sampleDefenseChain.ModiferKey = ModifierKeys.Control;
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new BearFormPowerShiftAbility(), TargetType.Me,
                false));
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new HeartOfTheWildAbility(), TargetType.Me, true));
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new CenarionWardAbility(), TargetType.Me, false));
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new SurvivalInstinctsAbility(), TargetType.Me,
                false));

            sampleChains.Add(sampleDefenseChain);

            // Ability chain sample: Cyclone
            var sampleCycloneChain = new AbilityChain("Cyclone");

            sampleCycloneChain.Specialization = WoWSpec.DruidFeral;
            sampleCycloneChain.HotKey = Keys.C;
            sampleCycloneChain.ModiferKey = ModifierKeys.Control;
            sampleCycloneChain.ChainedAbilities.Add(new ChainedAbility(new CycloneAbility(), TargetType.MyCurrentTarget,
                false));

            sampleChains.Add(sampleCycloneChain);

            // Ability chain sample: Entangling Roots
            var sampleEntanglingRootsChain = new AbilityChain("Entangling Roots");

            sampleEntanglingRootsChain.Specialization = WoWSpec.DruidFeral;
            sampleEntanglingRootsChain.HotKey = Keys.R;
            sampleEntanglingRootsChain.ModiferKey = ModifierKeys.Shift;
            sampleEntanglingRootsChain.ChainedAbilities.Add(new ChainedAbility(new EntanglingRootsAbility(),
                TargetType.MyCurrentTarget, false));

            sampleChains.Add(sampleEntanglingRootsChain);

            return sampleChains;
        }

        public static List<PawsItem> CreateItemsSampleList()
        {
            // Create a default items list...
            var sampleItems = new List<PawsItem>();

            // Item sample 1: Oralius' Whispering Crystal
            var oraliusWhisperingCrystal = new PawsItem
            {
                Name = "Oralius' Whispering Crystal",
                Entry = 118922,
                MyState = MyState.NotInCombat,
                Enabled = false
            };
            oraliusWhisperingCrystal.Conditions.Add(new ItemCondition
            {
                Instance = new TargetDoesNotHaveNamedAuraCondition(TargetType.Me, "Whispers of Insanity")
            });

            // Item sample 2: Draenic Agility Potion
            var draenicAgilityPotion = new PawsItem
            {
                Name = "Draenic Agility Potion",
                Entry = 109217,
                MyState = MyState.InCombat,
                Enabled = false
            };
            draenicAgilityPotion.Conditions.Add(new ItemCondition {Instance = new MeIsInCatFormCondition()});
            draenicAgilityPotion.Conditions.Add(new ItemCondition
            {
                Instance = new TargetHasNamedAuraCondition(TargetType.Me, "Berserk")
            });
            draenicAgilityPotion.Conditions.Add(new ItemCondition {Instance = new MyTargetIsWithinMeleeRangeCondition()});

            sampleItems.Add(oraliusWhisperingCrystal);
            sampleItems.Add(draenicAgilityPotion);

            return sampleItems;
        }
    }
}