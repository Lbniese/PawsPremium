using System;

namespace Paws.Core.Abilities
{
    /// <summary>
    /// Just a wrapper around an ordinary excpetion. Helps to provide contextual information while debugging.
    /// </summary>
    [Serializable]
    public class AbilityException : Exception
    {
        public AbilityException(string message)
            : base(message)
        {

        }
    }
}
