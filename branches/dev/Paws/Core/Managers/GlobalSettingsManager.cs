using Paws.Core.Utilities;
using Styx.Helpers;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

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
            // This has been changed so that the XML files are now embedded resources //



            // Determine if the Preset settings files exist for the current character //
            var characterSettingsDirectory = Path.Combine(Settings.CharacterSettingsDirectory, "Paws");
            if (!Directory.Exists(characterSettingsDirectory))
            {
                Directory.CreateDirectory(characterSettingsDirectory);
                Log.Diagnostics("Character Settings Directory Established... generating default presets.");

                var presetResourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);


                int resourceCount = 0;
                foreach (DictionaryEntry entry in presetResourceSet)
                {
                    using (StreamWriter streamWriter = new StreamWriter(Path.Combine(characterSettingsDirectory, entry.Key.ToString().Replace("_", " ") + ".xml"), false))
                    {
                        streamWriter.Write(entry.Value);
                        resourceCount++;
                    }
                }

                Log.Diagnostics(string.Format("...Finished generating {0} preset files", resourceCount));
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
