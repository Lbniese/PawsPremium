using Paws.Core.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws.Core
{
    public class AbilityChainSet
    {
        public List<IAbility> Abilities { get; set; }

        // This is the definition of one ability chain.
        public AbilityChainSet()
        {
            SetupTestFixture();
        }

        private void SetupTestFixture()
        {
            var berserk = new Paws.Core.Abilities.Feral.BerserkAbility();
            var incarnation = new Paws.Core.Abilities.Feral.IncarnationAbility();
        }
    }
}
