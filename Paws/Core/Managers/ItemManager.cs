using Paws.Core.Conditions;
using Paws.Core.Conditions.Attributes;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot;
using Styx.CommonBot.Coroutines;
using Styx.Helpers;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Paws.Core.Managers
{
    /// <summary>
    /// Provides the management of inventory and equipped items.
    /// </summary>
    public static class ItemManager
    {
        public const int HealthstoneEntryId = 5512;

        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }

        private static Stopwatch _lossOfControlDelay = new Stopwatch();
        private static int _clearLossOfControlInMilliseconds = 800;

        /// <summary>
        /// (Non-Blocking) Attempts to use the specified item on the provided target.
        /// </summary>
        /// <returns>Returns true if the item was successfully used.</returns>
        public static async Task<bool> UseItem(WoWItem item, WoWUnit target)
        {
            if (item == null)
                return false;

            item.Use(target.Guid);
            Log.Equipment(string.Format("Item ({0}) used on {1} [{2}]", item.SafeName, item.IsMe
                ? "Me"
                : target.SafeName, UnitManager.GuidToUnitID(target.Guid)));

            await CommonCoroutines.SleepForLagDuration();

            return true;
        }

        public static async Task<bool> UseItem(PawsItem item)
        {
            if (item == null || Me.HasAura(SpellBook.Prowl) || Me.Mounted || Me.IsInTravelForm() || Me.IsDead)
                return false;

            foreach (var condition in item.Conditions)
                if (!condition.Instance.Satisfied()) 
                    return false;

            var theItem = Me.BagItems
                .SingleOrDefault(o => o.Entry == item.Entry);

            if (theItem != null && theItem.CooldownTimeLeft.TotalMilliseconds == 0)
            {
                theItem.Use();

                Log.Equipment(string.Format("Item [{0}] used.", theItem.Name));

                await CommonCoroutines.SleepForLagDuration();

                return true;
            }

            return false;
        }

        public static async Task<bool> UseEligibleItems(MyState myState)
        {
            var eligibleItems = ItemManager.Items
                .Where(o => o.MyState == myState && o.Enabled);

            foreach (var item in eligibleItems)
            {
                if (await UseItem(item)) return true;
            }

            return false;
        }

        /// <summary>
        /// (Non-Blocking) Shortcut method to use a healthstone on the player.
        /// </summary>
        /// <returns>Returns true if the item was successfully used.</returns>
        public static async Task<bool> UseHealthstone()
        {
            if (!SpellManager.GlobalCooldown && 
                Me.Specialization == WoWSpec.DruidGuardian ? 
                    (SettingsManager.Instance.GuardianHealthstoneEnabled && Me.HealthPercent <= SettingsManager.Instance.GuardianHealthstoneMinHealth) :
                    (SettingsManager.Instance.HealthstoneEnabled && Me.HealthPercent <= SettingsManager.Instance.HealthstoneMinHealth))
            {
                var healthstone = Me.BagItems
                    .SingleOrDefault(o => o.Entry == ItemManager.HealthstoneEntryId);

                if (healthstone != null && healthstone.CooldownTimeLeft.TotalMilliseconds == 0)
                {
                    healthstone.Use();
                    Log.Equipment("Used Healthstone on Me.");

                    await CommonCoroutines.SleepForLagDuration();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Attempts to clear loss of control debuffs on the player with a trinket.
        /// TODO: This method needs a bit of testing and caressing. I've noticed instances where the trinket did not get used in some situations.
        /// </summary>
        /// <returns>Returns true if the loss of control was successfully cleared.</returns>
        public static async Task<bool> ClearLossOfControlWithTrinkets()
        {
            if (SettingsManager.Instance.Trinket1Enabled &&
                SettingsManager.Instance.Trinket1UseOnMe &&
                SettingsManager.Instance.Trinket1LossOfControl)
            {
                if (Me.HasLossOfControl() && !_lossOfControlDelay.IsRunning)
                {
                    _lossOfControlDelay.Start();
                    return false;
                }

                if (_lossOfControlDelay.IsRunning)
                {
                    if (_lossOfControlDelay.ElapsedMilliseconds > _clearLossOfControlInMilliseconds)
                    {
                        if (Me.Inventory.Equipped.Trinket1 != null)
                        {
                            if (Me.Inventory.Equipped.Trinket1.CooldownTimeLeft.TotalMilliseconds == 0)
                            {
                                Me.Inventory.Equipped.Trinket1.Use();
                                Log.Equipment(string.Format("Clearing Loss of Control with Trinket #1: {0} after {1} ms", Me.Inventory.Equipped.Trinket1.SafeName, _lossOfControlDelay.ElapsedMilliseconds));
                                await CommonCoroutines.SleepForLagDuration();
                            }
                            else
                            {
                                Log.Equipment("Trinket #1 is on cooldown.");
                            }
                        }

                        _lossOfControlDelay.Reset();
                        return true;
                    }
                }
            }

            if (SettingsManager.Instance.Trinket1Enabled &&
                SettingsManager.Instance.Trinket1UseOnMe &&
                SettingsManager.Instance.Trinket1LossOfControl)
            {
                if (Me.HasLossOfControl() && !_lossOfControlDelay.IsRunning)
                {
                    _lossOfControlDelay.Start();
                    return false;
                }

                if (_lossOfControlDelay.IsRunning)
                {
                    if (_lossOfControlDelay.ElapsedMilliseconds > _clearLossOfControlInMilliseconds)
                    {
                        if (Me.Inventory.Equipped.Trinket1 != null)
                        {
                            if (Me.Inventory.Equipped.Trinket1.CooldownTimeLeft.TotalMilliseconds == 0)
                            {
                                Me.Inventory.Equipped.Trinket1.Use();
                                Log.Equipment(string.Format("Clearing Loss of Control with Trinket #1: {0} after {1} ms", Me.Inventory.Equipped.Trinket1.SafeName, _lossOfControlDelay.ElapsedMilliseconds));
                                await CommonCoroutines.SleepForLagDuration();
                            }
                            else
                            {
                                Log.Equipment("Trinket #1 is on cooldown.");
                            }
                        }

                        _lossOfControlDelay.Reset();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// (Non-Blocking) Shortcut method to use Trinket #1 on the player.
        /// </summary>
        /// <returns>Returns true if the trinket was successfully used.</returns>
        public static async Task<bool> UseTrinket1()
        {
            if (Me.HasAura(SpellBook.Prowl)) return false;

            var trinket1 = Me.Inventory.Equipped.Trinket1;
            var target = (SettingsManager.Instance.Trinket1UseOnMe ? Me : MyCurrentTarget);

            if (SettingsManager.Instance.Trinket1Enabled)
            {
                if (target != null && trinket1 != null && trinket1.Usable && trinket1.CooldownTimeLeft.TotalMilliseconds == 0)
                {
                    // Use On Cooldown //
                    if (SettingsManager.Instance.Trinket1OnCoolDown)
                    {
                        if (await UseItem(trinket1, target)) return true;
                    }
                    // Use with Additional Conditions //
                    else if (SettingsManager.Instance.Trinket1AdditionalConditions)
                    {
                        var additionalConditionsSatisfied = false;
                        if (SettingsManager.Instance.Trinket1HealthMinEnabled) additionalConditionsSatisfied = (target.HealthPercent < SettingsManager.Instance.Trinket1HealthMin);
                        if (SettingsManager.Instance.Trinket1ManaMinEnabled) additionalConditionsSatisfied = (target.ManaPercent < SettingsManager.Instance.Trinket1ManaMin);

                        if (additionalConditionsSatisfied)
                            if (await UseItem(trinket1, target)) return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// (Non-Blocking) Shortcut method to use Trinket #2 on the player.
        /// </summary>
        /// <returns>Returns true if the trinket was successfully used.</returns>
        public static async Task<bool> UseTrinket2()
        {
            if (Me.HasAura(SpellBook.Prowl)) return false;

            var trinket2 = Me.Inventory.Equipped.Trinket2;
            var target = (SettingsManager.Instance.Trinket2UseOnMe ? Me : MyCurrentTarget);

            if (SettingsManager.Instance.Trinket2Enabled)
            {
                if (target != null && trinket2 != null && trinket2.Usable && trinket2.CooldownTimeLeft.TotalMilliseconds == 0)
                {
                    // Use On Cooldown //
                    if (SettingsManager.Instance.Trinket2OnCoolDown)
                    {
                        if (await UseItem(trinket2, target)) return true;
                    }
                    // Use with Additional Conditions //
                    else if (SettingsManager.Instance.Trinket2AdditionalConditions)
                    {
                        var additionalConditionsSatisfied = false;
                        if (SettingsManager.Instance.Trinket2HealthMinEnabled) additionalConditionsSatisfied = (target.HealthPercent < SettingsManager.Instance.Trinket2HealthMin);
                        if (SettingsManager.Instance.Trinket2ManaMinEnabled) additionalConditionsSatisfied = (target.ManaPercent < SettingsManager.Instance.Trinket2ManaMin);

                        if (additionalConditionsSatisfied)
                            if (await UseItem(trinket2, target)) return true;
                    }
                }
            }

            return false;
        }

        public static List<PawsItem> Items { get; set; }

        /// <summary>
        /// Gets the list of condition types that the user is allowed to use for items.
        /// </summary>
        public static List<Type> AllowedItemConditionTypes { get; private set; }

        /// <summary>
        /// Loads the dataset of items from file.
        /// </summary>
        public static void LoadDataSet()
        {
            var pathToFile = Path.Combine(Settings.CharacterSettingsDirectory, "Paws-Items.xml");

            if (!File.Exists(pathToFile))
            {
                var itemSamples = SampleFactory.CreateItemsSampleList();

                SaveDataSet(itemSamples);
                LoadDataSet();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<PawsItem>));

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                Items = (List<PawsItem>)serializer.Deserialize(reader);
                reader.Close();
            }
        }

        /// <summary>
        /// Saves the dataset of items to file.
        /// </summary>
        /// <param name="itemList"></param>
        public static void SaveDataSet(List<PawsItem> itemList)
        {
            var pathToFile = Path.Combine(Settings.CharacterSettingsDirectory, "Paws-Items.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(List<PawsItem>));

            using (var fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                serializer.Serialize(fileStream, itemList);
                fileStream.Close();
            }
        }

        /// <summary>
        /// Retrieves a new list of allowed item conditions based on the list of allowed types.
        /// </summary>
        /// <returns></returns>
        public static List<ItemCondition> GetAllowedItemConditions()
        {
            List<ItemCondition> itemConditions = new List<ItemCondition>();

            foreach (Type conditionType in AllowedItemConditionTypes)
            {
                itemConditions.Add(new ItemCondition(conditionType));
            }

            return itemConditions;
        }

        static ItemManager()
        {
            Items = new List<PawsItem>();

            // Using Reflection here to enumerate through the conditions that allow for items. This makes it easier to add
            // new conditions quickly without having to add types to the list of allowed types each time we create a new
            // item-allowed condition.
            AllowedItemConditionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(o => o.GetTypes())
                .Where(o => o.Namespace == "Paws.Core.Conditions" && Attribute.IsDefined(o, typeof(ItemConditionAttribute)))
                .OrderBy(o => o.Name)
                .ToList();
        }
    }
}
