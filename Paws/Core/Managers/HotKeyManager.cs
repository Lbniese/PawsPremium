using Paws.Core.Utilities;
using Styx;
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
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys keys);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public Keys LastPressedKey { get; set; }

        public Keys BoundKey { get { return Keys.F1; } }

        public const int UPDATE_TIMER_INTERVAL_MS = 200;

        private Stopwatch UpdateTimer = new Stopwatch();

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
            
        }

        public bool Update()
        {
            #region Premium Content Only

            if (Main.Product == Product.Premium)
            {
                if (!this.UpdateTimer.IsRunning) this.UpdateTimer.Start();
                if (this.UpdateTimer.ElapsedMilliseconds >= UPDATE_TIMER_INTERVAL_MS)
                {
                    this.UpdateTimer.Restart();

                    if (GetForegroundWindow() == StyxWoW.Memory.Process.MainWindowHandle)
                    {
                        if (KeyIsPressed(this.BoundKey) && this.LastPressedKey != this.BoundKey)
                        {
                            this.LastPressedKey = this.BoundKey;

                            Log.GUI("F1 Pressed!");

                            return true;
                        }
                    }

                    if (!KeyIsPressed(this.BoundKey)) this.LastPressedKey = Keys.None;
                }
            }

            #endregion

            return false;
        }

        public static bool KeyIsPressed(Keys key)
        {
            return GetAsyncKeyState(key) != 0;
        }
    }
}
