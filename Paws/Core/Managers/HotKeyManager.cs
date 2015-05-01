using Paws.Core.Utilities;
using Styx;
using Styx.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paws.Core.Managers
{
    public sealed class HotKeyManager
    {
        public enum HotKeyFunction
        {
            AbilityChain = 0x00
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys keys);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public Keys LastPressedKey { get; set; }

        public Keys BoundKey { get { return Keys.F1; } }

        public const int UPDATE_TIMER_INTERVAL_MS = 200;

        private Stopwatch UpdateTimer = new Stopwatch();

        public Dictionary<Keys, HotKeyFunction> HotKeyMap { get; set; }

        #region Singleton Stuff

        private static HotKeyManager _singletonInstance;

        public static HotKeyManager Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new HotKeyManager());
            }
        }

        #endregion

        public HotKeyManager()
        {
            if (Main.Product == Product.Premium)
            {
                HotkeysManager.Register("Burst", Keys.F1, ModifierKeys.Alt, KeyIsPressed);
            }
        }

        public void Update()
        {
            // need to determine if we even need to do anything here...
        }

        public void KeyIsPressed(Hotkey hotKey)
        {
            //Log.GUI(string.Format("Key pressed: {0}, {1}, {2}, {3}", hotKey.Id, hotKey.Name, hotKey.ModifierKeys, hotKey.Key));

            // Ability Chain Check...
            var abilityChain = AbilityChainsManager.Instance.AbilityChains.SingleOrDefault(o => o.Trigger == TriggerType.HotKeyButton && o.RegisteredHotKeyName == hotKey.Name);
            if (abilityChain != null)
            {
                // We have a triggered Ability Chain
                AbilityChainsManager.Instance.Trigger(abilityChain);
            }
        }
    }
}
