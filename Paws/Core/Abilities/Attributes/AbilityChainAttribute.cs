using System;

namespace Paws.Core.Abilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AbilityChainAttribute : Attribute
    {
        public string FriendlyName { get; set; }


        //  Notes
        //      What we want to do is to identify Abilities elligible for chaining.
        //      For example, we want to chain Mighty Bash, Berserk, and Incarnation together for burst damage.
        //      1)  Add the AbilityChainAttribute to those classes.
        //      2)  Use reflection to find these classes
        //      3)  Allow these classes to be populated in the UI
        //      4)  Add these to a paws-ability-chains.xml file that is created much like the items file.
        //      5)  When a condition or trigger is met, (such as HP < %, or HOTKEY PRESSED), the CR will fork off to 
        //          a dynmaic rotation mode that attempts to perform the CR using the abilities provided, one after another
        //      6)  Abilities can be marked required or optional in the ability chain.  For example, if the stun cannot
        //          be cast for whatever reason, move on to casting Berserk, but make Incarnation dependent on Berserk
    }
}