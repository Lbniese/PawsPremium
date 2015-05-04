using Paws.Core.Abilities;
using Paws.Core.Conditions;
using Styx.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paws.Core
{
    public class AbilityChain
    {
        public List<ChainedAbility> ChainedAbilities { get; set; }
        public TriggerType Trigger { get; set; }

        /// <summary>
        /// The name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Flag that indicates if the ability chain has been triggered. (Likely remove this)
        /// </summary>
        public bool Triggered { get; set; }

        /// <summary>
        /// Hold the list of conditions in order to trigger the chain.
        /// </summary>
        public List<ICondition> Conditions { get; set; }

        /// <summary>
        /// The Key associated with this ability chain that must be pressed in combination with the ModifierKey (if present).
        /// </summary>
        public Keys HotKey { get; set; }

        /// <summary>
        /// The Modifier Key associated with this ability chain that must be pressed in combination with the HotKey.
        /// </summary>
        public ModifierKeys ModiferKey { get; set; }

        public AbilityChain()
            : this("Not Named")
        { }

        // This is the definition of one ability chain.
        public AbilityChain(string name)
        {
            this.Name = name;
            this.ChainedAbilities = new List<ChainedAbility>();
            this.Conditions = new List<ICondition>();
            this.HotKey = Keys.None;
            this.ModiferKey = ModifierKeys.Alt;
        }

        /// <summary>
        /// Determines if the list of conditions triggers the chain.
        /// </summary>
        public bool Satisfied()
        {
            if (!this.Triggered)
            {
                if (this.Trigger == TriggerType.ListOfConditions)
                {
                    foreach (var condition in this.Conditions)
                    {
                        if (!condition.Satisfied())
                        {
                            this.Triggered = false;
                            return false;
                        }
                    }

                    this.Triggered = true;
                    return true;
                }
            }

            return false;
        }
    }

    public enum TriggerType
    {
        ListOfConditions = 0x00, 
        HotKeyButton
    }
}
