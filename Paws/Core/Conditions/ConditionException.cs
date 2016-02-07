using System;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Just a wrapper around a normal exception to help understand where the exception is coming from.
    /// </summary>
    [Serializable]
    public class ConditionException : Exception
    {
        public ConditionException(string message)
            : base(message)
        {
        }
    }
}