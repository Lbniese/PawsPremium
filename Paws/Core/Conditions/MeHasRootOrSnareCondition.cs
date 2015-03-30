using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// <para>Condition based on if the player has a root or snare debuff.</para>
    /// <para>Release 1.1.0</para>
    /// </summary>
    [ItemCondition(FriendlyName = "I am Rooted or Snared")]
    public class MeHasRootOrSnareCondition : ICondition
    {
        public bool Satisfied()
        {
            return StyxWoW.Me.HasRootOrSnare();
        }
    }
}
