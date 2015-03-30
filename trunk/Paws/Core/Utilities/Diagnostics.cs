using Paws.Core.Abilities;
using Styx.Helpers;
using System.IO;

namespace Paws.Core.Managers
{
    /// <summary>
    /// NOT IMPLEMENTED YET
    /// Placeholder class for future implementation.
    /// </summary>
    public sealed class Diagnostics
    {
        #region Singleton stuff

        private static Diagnostics _singletonInstance;

        public static Diagnostics Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new Diagnostics());
            }
        }

        public static void Initialize()
        {
            _singletonInstance = new Diagnostics();
        }

        #endregion

        public float CastsPerSecond { get; private set; }

        public Diagnostics()
        { }

        public void SuccessfulCast(IAbility ability)
        {

        }

        public void FailedCast(IAbility ability)
        {

        }
    }
}
