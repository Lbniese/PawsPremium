using Paws.Core.Abilities;
using Paws.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws.Core
{
    public class AbilityChain
    {
        public List<IAbility> Abilities { get; set; }
        public TriggerType Trigger { get; set; }

        public bool Triggered { get; set; }

        /// <summary>
        /// Hold the list of conditions in order to trigger the chain.
        /// </summary>
        public List<ICondition> Conditions { get; set; }

        // This is the definition of one ability chain.
        public AbilityChain()
        {
            this.Abilities = new List<IAbility>();
            this.Conditions = new List<ICondition>();
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
