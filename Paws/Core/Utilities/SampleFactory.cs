using Paws.Core.Conditions;
using Styx;
using Styx.Common;
using System.Collections.Generic;
using Feral = Paws.Core.Abilities.Feral;
using Shared = Paws.Core.Abilities.Shared;

namespace Paws.Core.Utilities
{
    public static class SampleFactory
    {
        public static List<AbilityChain> CreateAbilityChainsSampleList()
        {
            // Create a default abilities list...
            List<AbilityChain> sampleChains = new List<AbilityChain>();

            // Ability chain sample: Burst Damage
            AbilityChain sampleBurstChain = new AbilityChain("Burst Damage");

            sampleBurstChain.Specialization = WoWSpec.DruidFeral;
            sampleBurstChain.HotKey = System.Windows.Forms.Keys.F;
            sampleBurstChain.ModiferKey = ModifierKeys.Control;
            sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new Feral.IncarnationAbility(), TargetType.Me, true));
            sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new Feral.BerserkAbility(), TargetType.Me, true));

            sampleChains.Add(sampleBurstChain);

            // Ability chain sample: Burst Damage
            AbilityChain sampleDefenseChain = new AbilityChain("HotW Defense");

            sampleDefenseChain.Specialization = WoWSpec.DruidFeral;
            sampleDefenseChain.HotKey = System.Windows.Forms.Keys.D;
            sampleDefenseChain.ModiferKey = ModifierKeys.Control;
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Feral.BearFormPowerShiftAbility(), TargetType.Me, false));
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Feral.HeartOfTheWildAbility(), TargetType.Me, true));
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Shared.CenarionWardAbility(), TargetType.Me, false));
            sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Feral.SurvivalInstinctsAbility(), TargetType.Me, false));

            sampleChains.Add(sampleDefenseChain);

            // Ability chain sample: Cyclone
            AbilityChain sampleCycloneChain = new AbilityChain("Cyclone");

            sampleCycloneChain.Specialization = WoWSpec.DruidFeral;
            sampleCycloneChain.HotKey = System.Windows.Forms.Keys.C;
            sampleCycloneChain.ModiferKey = ModifierKeys.Control;
            sampleCycloneChain.ChainedAbilities.Add(new ChainedAbility(new Shared.CycloneAbility(), TargetType.MyCurrentTarget, false));

            sampleChains.Add(sampleCycloneChain);

            // Ability chain sample: Entangling Roots
            AbilityChain sampleEntanglingRootsChain = new AbilityChain("Entangling Roots");

            sampleEntanglingRootsChain.Specialization = WoWSpec.DruidFeral;
            sampleEntanglingRootsChain.HotKey = System.Windows.Forms.Keys.R;
            sampleEntanglingRootsChain.ModiferKey = ModifierKeys.Shift;
            sampleEntanglingRootsChain.ChainedAbilities.Add(new ChainedAbility(new Shared.EntanglingRootsAbility(), TargetType.MyCurrentTarget, false));

            sampleChains.Add(sampleEntanglingRootsChain);

            return sampleChains;
        }

        public static List<PawsItem> CreateItemsSampleList()
        {
            // Create a default items list...
            List<PawsItem> sampleItems = new List<PawsItem>();

            // Item sample 1: Oralius' Whispering Crystal
            PawsItem oraliusWhisperingCrystal = new PawsItem()
            {
                Name = "Oralius' Whispering Crystal",
                Entry = 118922,
                MyState = MyState.NotInCombat,
                Enabled = false
            };
            oraliusWhisperingCrystal.Conditions.Add(new ItemCondition() { Instance = new TargetDoesNotHaveNamedAuraCondition(TargetType.Me, "Whispers of Insanity") });

            // Item sample 2: Draenic Agility Potion
            PawsItem draenicAgilityPotion = new PawsItem()
            {
                Name = "Draenic Agility Potion",
                Entry = 109217,
                MyState = MyState.InCombat,
                Enabled = false
            };
            draenicAgilityPotion.Conditions.Add(new ItemCondition() { Instance = new MeIsInCatFormCondition() });
            draenicAgilityPotion.Conditions.Add(new ItemCondition() { Instance = new TargetHasNamedAuraCondition(TargetType.Me, "Berserk") });
            draenicAgilityPotion.Conditions.Add(new ItemCondition() { Instance = new MyTargetIsWithinMeleeRangeCondition() });

            sampleItems.Add(oraliusWhisperingCrystal);
            sampleItems.Add(draenicAgilityPotion);

            return sampleItems;
        }
    }
}
