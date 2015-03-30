using Paws.Core.Utilities;
using Styx.Helpers;
using System;
using System.IO;
using System.Linq;

namespace Paws.Core.Managers
{
    public sealed class GlobalSettingsManager : Settings
    {
        #region Singleton Stuff

        private static GlobalSettingsManager _singletonInstance;

        public static GlobalSettingsManager Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new GlobalSettingsManager());
            }
        }

        #endregion

        [Setting, DefaultValue(true)]
        public bool UseCombatReachDistanceCalculations { get; set; }

        [Setting, DefaultValue("Automated Questing")]
        public string LastUsedProfile { get; set; }

        public GlobalSettingsManager()
            : base(Path.Combine(Settings.CharacterSettingsDirectory, "Paws-GlobalSettings.xml"))
        { }

        public static string GetPawsRoutineDirectory()
        {
            var pawsDirectory = Directory.GetDirectories("Routines").FirstOrDefault(o => o.ToUpper().Contains("PAWS"));

            if (pawsDirectory == null)
                throw new Exception("Paws routine directory is missing! Ensure that the root directory in the Routines folder is named \"Paws\""); // this should only ever happen if someone renamed the paws directory to something else

            return pawsDirectory;
        }

        public void Init()
        {
            // Determine if the Preset settings files exist for the routine //
            var presetsDirectory = Path.Combine(GetPawsRoutineDirectory(), @"Paws\Presets");
            if (!Directory.Exists(presetsDirectory))
                throw new Exception("Presets directory is missing!");

            // Determine if the Preset settings files exist for the current character //
            var characterSettingsDirectory = Path.Combine(Settings.CharacterSettingsDirectory, "Paws");
            if (!Directory.Exists(characterSettingsDirectory))
            {
                Directory.CreateDirectory(characterSettingsDirectory);
                Log.Diagnostics("Character Settings Directory Established... loading default presets.");

                // Copy the presets file to the character settings directory
                string[] presetFiles = Directory.GetFiles(presetsDirectory, "*.xml");
                foreach (var file in presetFiles)
                {
                    File.Copy(file, Path.Combine(characterSettingsDirectory, Path.GetFileName(file)));
                }

                Log.Diagnostics(string.Format("...Finished copying {0} preset files", presetFiles.Length));
            }
        }

        public static string[] GetCharacterProfileFiles()
        {
            return Directory.GetFiles(Path.Combine(Settings.CharacterSettingsDirectory, "Paws"), "*.xml");
        }

        public static string GetFullPathToProfile(string profileName)
        {
            return Path.Combine(Settings.CharacterSettingsDirectory, "Paws", string.Format("{0}.xml", profileName));
        }
    }
}
