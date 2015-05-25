using Paws.Core.Utilities;
using Styx.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Paws.Core.Managers
{
    public sealed class SettingsManager : Settings
    {
        #region Singleton Stuff

        private static SettingsManager _singletonInstance;

        public static SettingsManager Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new SettingsManager(GlobalSettingsManager.GetFullPathToProfile(GlobalSettingsManager.Instance.LastUsedProfile)));
            }
        }

        public static void InitWithLastUsedProfile()
        {
            _singletonInstance = new SettingsManager(GlobalSettingsManager.GetFullPathToProfile(GlobalSettingsManager.Instance.LastUsedProfile));
        }

        public static void InitWithProfile(string pathToProfile)
        {
            _singletonInstance = new SettingsManager(pathToProfile);
        }

        #endregion

        #region Guardian

        [Setting, DefaultValue(true)]
        public bool BearFormEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool BearFormOnlyDuringPullOrCombat { get; set; }

        [Setting, DefaultValue(true)]
        public bool BearFormAlways { get; set; }

        [Setting, DefaultValue(true)]
        public bool BearFormDoNotOverrideCatForm { get; set; }

        [Setting, DefaultValue(true)]
        public bool BearFormDoNotOverrideTravelForm { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianWildChargeEnabled { get; set; }

        [Setting, DefaultValue(10.0)]
        public double GuardianWildChargeMinDistance { get; set; }

        [Setting, DefaultValue(25.0)]
        public double GuardianWildChargeMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianDisplacerBeastEnabled { get; set; }

        [Setting, DefaultValue(25.0)]
        public double GuardianDisplacerBeastMinDistance { get; set; }

        [Setting, DefaultValue(40.0)]
        public double GuardianDisplacerBeastMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianDashEnabled { get; set; }

        [Setting, DefaultValue(20.0)]
        public double GuardianDashMinDistance { get; set; }

        [Setting, DefaultValue(60.0)]
        public double GuardianDashMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianStampedingRoarEnabled { get; set; }

        [Setting, DefaultValue(20.0)]
        public double GuardianStampedingRoarMinDistance { get; set; }

        [Setting, DefaultValue(50.0)]
        public double GuardianStampedingRoarMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianBerserkEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianBerserkOnCooldown { get; set; }

        [Setting, DefaultValue(false)]
        public bool GuardianBerserkEnemyHealthCheck { get; set; }

        [Setting, DefaultValue(2.0f)]
        public float GuardianBerserkEnemyHealthMultiplier { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianBerserkSurroundedByEnemiesEnabled { get; set; }

        [Setting, DefaultValue(4)]
        public int GuardianBerserkSurroundedByMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool MangleEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool LacerateEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool LacerateAllowClipping { get; set; }

        [Setting, DefaultValue(true)]
        public bool PulverizeEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool MaulEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool MaulOnlyDuringToothAndClawProc { get; set; }

        [Setting, DefaultValue(true)]
        public bool MaulRequireMinRage { get; set; }

        [Setting, DefaultValue(65.0)]
        public double MaulMinRage { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianThrashEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianThrashAllowClipping { get; set; }

        [Setting, DefaultValue(1)]
        public int GuardianThrashMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianSurvivalInstinctsEnabled { get; set; }

        [Setting, DefaultValue(50.0)]
        public double GuardianSurvivalInstinctsMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool BarkskinEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double BarkskinMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool SavageDefenseEnabled { get; set; }

        [Setting, DefaultValue(90.0)]
        public double SavageDefenseMinHealth { get; set; }

        [Setting, DefaultValue(60.0)]
        public double SavageDefenseMinRage { get; set; }

        [Setting, DefaultValue(true)]
        public bool BristlingFurEnabled { get; set; }

        [Setting, DefaultValue(55.0)]
        public double BristlingFurMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianSkullBashEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianTyphoonEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianMightyBashEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianIncapacitatingRoarEnabled { get; set; }

        [Setting, DefaultValue(3)]
        public int GuardianIncapacitatingRoarMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianMassEntanglementEnabled { get; set; }

        [Setting, DefaultValue(3)]
        public int GuardianMassEntanglementMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool FrenziedRegenerationEnabled { get; set; }

        [Setting, DefaultValue(65.0)]
        public double FrenziedRegenerationMinHealth { get; set; }

        [Setting, DefaultValue(60.0)]
        public double FrenziedRegenerationMinRage { get; set; }

        [Setting, DefaultValue(false)]
        public bool GuardianRejuvenationEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double GuardianRejuvenationMinHealth { get; set; }

        [Setting, DefaultValue(false)]
        public bool GuardianHealingTouchEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianHealingTouchOnlyDuringDreamOfCenarius { get; set; }

        [Setting, DefaultValue(85.0)]
        public double GuardianHealingTouchMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianRenewalEnabled { get; set; }

        [Setting, DefaultValue(45.0)]
        public double GuardianRenewalMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianHealthstoneEnabled { get; set; }

        [Setting, DefaultValue(80.0)]
        public double GuardianHealthstoneMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianCenarionWardEnabled { get; set; }

        [Setting, DefaultValue(90.0)]
        public double GuardianCenarionWardMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianNaturesVigilEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double GuardianNaturesVigilMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianFaerieFireEnabled { get; set; }

        [Setting, DefaultValue(10.0)]
        public double GuardianFaerieFireMinDistance { get; set; }

        [Setting, DefaultValue(35.0)]
        public double GuardianFaerieFireMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool GrowlEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool GrowlAnythingNotMe { get; set; }

        [Setting, DefaultValue(false)]
        public bool GrowlGroupTank { get; set; }

        [Setting, DefaultValue(true)]
        public bool GrowlGroupHealer { get; set; }

        [Setting, DefaultValue(true)]
        public bool GrowlGroupDPS { get; set; }

        [Setting, DefaultValue(false)]
        public bool GrowlGroupPlayerPet { get; set; }

        [Setting, DefaultValue(500)]
        public int GrowlReactionTimeInMS { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianIncarnationEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool GuardianIncarnationOnCooldown { get; set; }

        [Setting, DefaultValue(true)]
        public bool GuardianIncarnationEnemyHealthCheck { get; set; }

        [Setting, DefaultValue(1.5f)]
        public float GuardianIncarnationEnemyHealthMultiplier { get; set; }

        #endregion

        [Setting, DefaultValue(true)]
        public bool CatFormEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool CatFormOnlyDuringPullOrCombat { get; set; }

        [Setting, DefaultValue(true)]
        public bool CatFormAlways { get; set; }

        [Setting, DefaultValue(true)]
        public bool CatFormDoNotOverrideBearForm { get; set; }

        [Setting, DefaultValue(true)]
        public bool CatFormDoNotOverrideTravelForm { get; set; }

        [Setting, DefaultValue(true)]
        public bool ProwlEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool ProwlAlways { get; set; }

        [Setting, DefaultValue(true)]
        public bool ProwlOnlyDuringPull { get; set; }

        [Setting, DefaultValue(40.0)]
        public double ProwlMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool MarkOfTheWildEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool MarkOfTheWildCheckPartyMembers { get; set; }

        [Setting, DefaultValue(true)]
        public bool MarkOfTheWildCheckRaidMembers { get; set; }

        [Setting, DefaultValue(true)]
        public bool MarkOfTheWildDoNotApplyIfStealthed { get; set; }

        [Setting, DefaultValue(4.75)]
        public double MeleeRange { get; set; }

        [Setting, DefaultValue(7.75)]
        public double AOERange { get; set; }

        [Setting, DefaultValue(true)]
        public bool TargetHeightEnabled { get; set; }

        [Setting, DefaultValue(10.0f)]
        public float TargetHeightMinDistance { get; set; }

        [Setting, DefaultValue(false)]
        public bool ReleaseSpiritOnDeathEnabled { get; set; }

        [Setting, DefaultValue(2500)]
        public int ReleaseSpiritOnDeathIntervalInMs { get; set; }

        [Setting, DefaultValue(true)]
        public bool AutoTargetingEnabled { get; set; }

        [Setting, DefaultValue(800)]
        public int InterruptMinMilliseconds { get; set; }

        [Setting, DefaultValue(1500)]
        public int InterruptMaxMilliseconds { get; set; }

        [Setting, DefaultValue(95.0)]
        public double InterruptSuccessRate { get; set; }

        [Setting, DefaultValue(true)]
        public bool AllowMovement { get; set; }

        [Setting, DefaultValue(true)]
        public bool AllowTargetFacing { get; set; }

        [Setting, DefaultValue(false)]
        public bool AllowTargeting { get; set; }

        [Setting, DefaultValue(false)]
        public bool ForceCombat { get; set; }

        [Setting, DefaultValue(true)]
        public bool MultiDOTRotationEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool WildChargeEnabled { get; set; }

        [Setting, DefaultValue(10.0)]
        public double WildChargeMinDistance { get; set; }

        [Setting, DefaultValue(25.0)]
        public double WildChargeMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool DisplacerBeastEnabled { get; set; }

        [Setting, DefaultValue(25.0)]
        public double DisplacerBeastMinDistance { get; set; }

        [Setting, DefaultValue(40.0)]
        public double DisplacerBeastMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool DashEnabled { get; set; }

        [Setting, DefaultValue(20.0)]
        public double DashMinDistance { get; set; }

        [Setting, DefaultValue(60.0)]
        public double DashMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool StampedingRoarEnabled { get; set; }

        [Setting, DefaultValue(20.0)]
        public double StampedingRoarMinDistance { get; set; }

        [Setting, DefaultValue(50.0)]
        public double StampedingRoarMaxDistance { get; set; }

        [Setting, DefaultValue(true)]
        public bool SavageRoarEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool SavageRoarAllowClipping { get; set; }

        [Setting, DefaultValue(1)]
        public int SavageRoarMinComboPoints { get; set; }

        [Setting, DefaultValue(true)]
        public bool TigersFuryEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool TigersFuryOnCooldown { get; set; }

        [Setting, DefaultValue(false)]
        public bool TigersFurySyncWithBerserk { get; set; }

        [Setting, DefaultValue(false)]
        public bool TigersFuryUseMaxEnergy { get; set; }

        [Setting, DefaultValue(40.0)]
        public double TigersFuryMaxEnergy { get; set; }

        [Setting, DefaultValue(true)]
        public bool BerserkEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool BerserkOnCooldown { get; set; }

        [Setting, DefaultValue(true)]
        public bool BerserkEnemyHealthCheck { get; set; }

        [Setting, DefaultValue(1.3f)]
        public float BerserkEnemyHealthMultiplier { get; set; }

        [Setting, DefaultValue(true)]
        public bool BerserkSurroundedByEnemiesEnabled { get; set; }

        [Setting, DefaultValue(4)]
        public int BerserkSurroundedByMinEnemies { get; set; }

        [Setting, DefaultValue(true)] 
        public bool IncarnationEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool IncarnationOnCooldown { get; set; }

        [Setting, DefaultValue(true)]
        public bool IncarnationEnemyHealthCheck { get; set; }

        [Setting, DefaultValue(1.3f)]
        public float IncarnationEnemyHealthMultiplier { get; set; }

        [Setting, DefaultValue(true)]
        public bool IncarnationSurroundedByEnemiesEnabled { get; set; }

        [Setting, DefaultValue(4)]
        public int IncarnationSurroundedByMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool MoonfireEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool MoonfireAllowClipping { get; set; }

        [Setting, DefaultValue(true)]
        public bool MoonfireOnlyWithLunarInspiration { get; set; }

        [Setting, DefaultValue(true)]
        public bool RakeEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool RakeAllowClipping { get; set; }

        [Setting, DefaultValue(true)]
        public bool RakeAllowMultiplierClipping { get; set; }

        [Setting, DefaultValue(true)]
        public bool RakeStealthOpener { get; set; }

        [Setting, DefaultValue(4)]
        public int RakeMaxEnemies { get; set; }
        
        [Setting, DefaultValue(true)]
        public bool ShredEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool ShredStealthOpener { get; set; }

        [Setting, DefaultValue(true)]
        public bool RipEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool RipAllowClipping { get; set; }

        [Setting, DefaultValue(true)]
        public bool RipEnemyHealthCheck { get; set; }

        [Setting, DefaultValue(1.3f)]
        public float RipEnemyHealthMultiplier { get; set; }

        [Setting, DefaultValue(true)]
        public bool FerociousBiteEnabled { get; set; }

        [Setting, DefaultValue(50.0)]
        public double FerociousBiteMinEnergy { get; set; }

        [Setting, DefaultValue(true)]
        public bool ThrashEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool ThrashAllowClipping { get; set; }

        [Setting, DefaultValue(2)]
        public int ThrashMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool ThrashClearcastingProcEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool SwipeEnabled { get; set; }

        [Setting, DefaultValue(3)]
        public int SwipeMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool ForceOfNatureEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool BloodtalonsEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool BloodtalonsApplyImmediately { get; set; }

        [Setting, DefaultValue(true)]
        public bool BloodtalonsApplyToFinishers { get; set; }

        [Setting, DefaultValue(true)]
        public bool RejuvenationEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double RejuvenationMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealingTouchEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealingTouchOnlyDuringPredatorySwiftness { get; set; }

        [Setting, DefaultValue(85.0)]
        public double HealingTouchMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool RenewalEnabled { get; set; }

        [Setting, DefaultValue(45.0)]
        public double RenewalMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealthstoneEnabled { get; set; }

        [Setting, DefaultValue(80.0)]
        public double HealthstoneMinHealth { get; set; }

        [Setting, DefaultValue(false)]
        public bool RebirthOnlyDuringPredatorySwiftness { get; set; }

        [Setting, DefaultValue(false)]
        public bool RebirthAnyAlly { get; set; }

        [Setting, DefaultValue(true)]
        public bool RebirthTank{ get; set; }

        [Setting, DefaultValue(true)]
        public bool RebirthHealer { get; set; }

        [Setting, DefaultValue(false)]
        public bool RebirthDPS { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesMyHealthCheckEnabled { get; set; }

        [Setting, DefaultValue(50.0)]
        public double HealMyAlliesMyMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesWithHealingTouchEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesWithHealingTouchOnlyDuringPSEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double HealMyAlliesWithHealingTouchMinHealth { get; set; }

        [Setting, DefaultValue(false)]
        public bool HealMyAlliesWithRejuvenationEnabled { get; set; }

        [Setting, DefaultValue(90.0)]
        public double HealMyAlliesWithRejuvenationMinHealth { get; set; }

        [Setting, DefaultValue(2)]
        public int HealMyAlliesWithRejuvenationMaxAllies { get; set; }

        [Setting, DefaultValue(false)]
        public bool HealMyAlliesAnyAlly { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesTank { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesHealer { get; set; }

        [Setting, DefaultValue(false)]
        public bool HealMyAlliesDPS { get; set; }

        [Setting, DefaultValue(true)]
        public bool HealMyAlliesApplyWeightsEnabled { get; set; }

        [Setting, DefaultValue(1.6f)]
        public float HealMyAlliesTankWeight { get; set; }

        [Setting, DefaultValue(1.3f)]
        public float HealMyAlliesHealerWeight { get; set; }

        [Setting, DefaultValue(1.0f)]
        public float HealMyAlliesDPSWeight { get; set; }

        [Setting, DefaultValue(true)]
        public bool CenarionWardEnabled { get; set; }

        [Setting, DefaultValue(90.0)]
        public double CenarionWardMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool HeartOfTheWildEnabled { get; set; }

        [Setting, DefaultValue(65.0)]
        public double HeartOfTheWildMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool HeartOfTheWildSyncWithCenarionWard { get; set; }

        [Setting, DefaultValue(true)]
        public bool IncapacitatingRoarEnabled { get; set; }

        [Setting, DefaultValue(3)]
        public int IncapacitatingRoarMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool MassEntanglementEnabled { get; set; }

        [Setting, DefaultValue(3)]
        public int MassEntanglementMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool NaturesVigilEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double NaturesVigilMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool SurvivalInstinctsEnabled { get; set; }

        [Setting, DefaultValue(50.0)]
        public double SurvivalInstinctsMinHealth { get; set; }

        [Setting, DefaultValue(true)]
        public bool SkullBashEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool TyphoonEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool MightyBashEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool MaimEnabled { get; set; }

        [Setting, DefaultValue(1)]
        public int MaimMinComboPoints { get; set; }

        [Setting, DefaultValue(true)]
        public bool FaerieFireRogueEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool FaerieFireDruidEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireWarriorEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFirePaladinEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireMageEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireMonkEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireHunterEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFirePriestEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireDeathKnightEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireShamanEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool FaerieFireWarlockEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool BearFormPowerShiftEnabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool RemoveSnareWithStampedingRoar { get; set; }

        [Setting, DefaultValue(false)]
        public bool RemoveSnareWithDash{ get; set; }

        [Setting, DefaultValue(500)]
        public int SnareReactionTimeInMs { get; set; }

        [Setting, DefaultValue(true)]
        public bool SootheEnabled { get; set; }

        [Setting, DefaultValue(500)]
        public int SootheReactionTimeInMs { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket1Enabled { get; set; }
        
        [Setting, DefaultValue(true)]
        public bool Trinket1UseOnMe { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket1UseOnEnemy { get; set; }

        [Setting, DefaultValue(true)]
        public bool Trinket1OnCoolDown { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket1LossOfControl { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket1AdditionalConditions { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket1HealthMinEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double Trinket1HealthMin { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket1ManaMinEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double Trinket1ManaMin { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket2Enabled { get; set; }

        [Setting, DefaultValue(true)]
        public bool Trinket2UseOnMe { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket2UseOnEnemy { get; set; }

        [Setting, DefaultValue(true)]
        public bool Trinket2OnCoolDown { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket2LossOfControl { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket2AdditionalConditions { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket2HealthMinEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double Trinket2HealthMin { get; set; }

        [Setting, DefaultValue(false)]
        public bool Trinket2ManaMinEnabled { get; set; }

        [Setting, DefaultValue(75.0)]
        public double Trinket2ManaMin { get; set; }

        [Setting, DefaultValue(true)]
        public bool TaurenWarStompEnabled { get; set; }

        [Setting, DefaultValue(2)]
        public int TaurenWarStompMinEnemies { get; set; }

        [Setting, DefaultValue(true)]
        public bool TrollBerserkingEnabled { get; set; }

        [Setting, DefaultValue(false)]
        public bool TrollBerserkingOnCooldown { get; set; }

        [Setting, DefaultValue(true)]
        public bool TrollBerserkingEnemyHealthCheck { get; set; }

        [Setting, DefaultValue(1.3f)]
        public float TrollBerserkingEnemyHealthMultiplier { get; set; }

        [Setting, DefaultValue(true)]
        public bool TrollBerserkingSurroundedByEnemiesEnabled { get; set; }

        [Setting, DefaultValue(4)]
        public int TrollBerserkingSurroundedByMinEnemies { get; set; }

        public SettingsManager(string profilePathToFile)
            : base(profilePathToFile)
        { }

        /// <summary>
        /// Dumps the current settings into the Diagnostics Log.
        /// </summary>
        public void LogDump()
        {
            var strBuilder = new StringBuilder();
            var properties = this.GetType().GetProperties().Where(o => Attribute.IsDefined(o, typeof(SettingAttribute)))
                .OrderBy(o => o.Name)
                .ToList();

            strBuilder.Append("Applied Settings: ");

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(this);

                strBuilder.AppendFormat("{0}: {1}", propertyName, propertyValue);

                strBuilder.Append(property == properties.Last() ? string.Empty : ", "); 
            }

            Log.Diagnostics(strBuilder.ToString());
        }
    }
}
