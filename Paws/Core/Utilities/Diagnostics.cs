using Paws.Core.Abilities;

namespace Paws.Core.Utilities
{
    /// <summary>
    ///     NOT IMPLEMENTED YET
    ///     Placeholder class for future implementation.
    /// </summary>
    public sealed class Diagnostics
    {
        public float CastsPerSecond { get; private set; }

        public void SuccessfulCast(IAbility ability)
        {
        }

        public void FailedCast(IAbility ability)
        {
        }

        #region Singleton stuff

        private static Diagnostics _singletonInstance;

        public static Diagnostics Instance
        {
            get { return _singletonInstance ?? (_singletonInstance = new Diagnostics()); }
        }

        public static void Initialize()
        {
            _singletonInstance = new Diagnostics();
        }

        #endregion
    }
}