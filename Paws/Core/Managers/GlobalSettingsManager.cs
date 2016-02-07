using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Paws.Core.Utilities;
using Styx.Helpers;

namespace Paws.Core.Managers
{
    public sealed class GlobalSettingsManager : Settings
    {
        public GlobalSettingsManager()
            : base(Path.Combine(CharacterSettingsDirectory, "Paws-GlobalSettings.xml"))
        {
        }

        [Setting, DefaultValue(true)]
        public bool UseCombatReachDistanceCalculations { get; set; }

        [Setting, DefaultValue("Automated Questing")]
        public string LastUsedProfile { get; set; }

        public static string GetPawsRoutineDirectory()
        {
            var pawsDirectory = Directory.GetDirectories("Routines").FirstOrDefault(o => o.ToUpper().Contains("PAWS"));

            if (pawsDirectory == null)
                throw new Exception(
                    "Paws routine directory is missing! Ensure that the root directory in the Routines folder is named \"Paws\"");
                    // this should only ever happen if someone renamed the paws directory to something else

            return pawsDirectory;
        }

        public void Init()
        {
            // Determine if the Preset settings files exist for the routine //
            // This has been changed so that the XML files are now embedded resources //

            var presetResourceSet =
                new ResourceSet(
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("Paws.Properties.Resources.resources"));

            // Determine if the Preset settings files exist for the current character //
            var characterSettingsDirectory = Path.Combine(CharacterSettingsDirectory, "Paws");
            if (!Directory.Exists(characterSettingsDirectory))
            {
                Directory.CreateDirectory(characterSettingsDirectory);
                Log.Diagnostics("Character Settings Directory Established... generating default presets.");


                var resourceCount = 0;
                foreach (DictionaryEntry entry in presetResourceSet)
                {
                    using (
                        var streamWriter =
                            new StreamWriter(
                                Path.Combine(characterSettingsDirectory, entry.Key.ToString().Replace("_", " ") + ".xml"),
                                false))
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
            return Directory.GetFiles(Path.Combine(CharacterSettingsDirectory, "Paws"), "*.xml");
        }

        public static string GetFullPathToProfile(string profileName)
        {
            return Path.Combine(CharacterSettingsDirectory, "Paws", string.Format("{0}.xml", profileName));
        }

        #region Singleton Stuff

        private static GlobalSettingsManager _singletonInstance;

        public static GlobalSettingsManager Instance
        {
            get { return _singletonInstance ?? (_singletonInstance = new GlobalSettingsManager()); }
        }

        #endregion
    }
}