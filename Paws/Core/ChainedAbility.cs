using Paws.Core.Abilities;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Paws.Core
{
    /// <summary>
    /// A chained ability is single link within an ability chain. Mostly a wrapper class around 
    /// an IAbility that also defines some attributes required to be part of an ability chain.
    /// </summary>
    public class ChainedAbility : IXmlSerializable
    {
        private string _friendlyName;

        /// <summary>
        /// Gets or sets the class type associated with this Ability.
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// The ability must be off of any cooldowns if this flag is set.
        /// </summary>
        public bool MustBeReady { get; set; }

        /// <summary>
        /// Determines who the target is that this ability will cast on.
        /// </summary>
        public TargetType TargetType { get; set; }

        /// <summary>
        /// Gets or sets the ICondition instance associated with this item condition.
        /// When this Instance is set, the condition is available for serialization.
        /// </summary>
        public IAbility Instance { get; set; }

        public ChainedAbility()
        { }

        public ChainedAbility(IAbility ability, TargetType targetType, bool mustBeReady)
        {
            this.Instance = ability;
            this.TargetType = targetType;
            this.MustBeReady = mustBeReady;

            this.ClassType = this.Instance.GetType();
        }

        public ChainedAbility(Type typeOfAbility)
        {
            this.ClassType = typeOfAbility;
        }

        /// <summary>
        /// (Lazy-Loaded) Gets or sets the friendly name of the Ability.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (string.IsNullOrEmpty(_friendlyName))
                {
                    var thisAttribute = (AbilityChainAttribute)this.ClassType.GetCustomAttributes(typeof(AbilityChainAttribute), false)
                        .FirstOrDefault();

                    _friendlyName = thisAttribute != null ? thisAttribute.FriendlyName : "Unknown";
                }

                return _friendlyName;
            }
            set
            {
                _friendlyName = value;
            }
        }

        /// <summary>
        /// Creates an IAbility instance of the class type.
        /// </summary>
        public void CreateInstance()
        {
            this.Instance = (IAbility)Activator.CreateInstance(this.ClassType);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            var type = reader.GetAttribute("Type");
            var mustBeReady = reader.GetAttribute("MustBeReady");
            var targetType = reader.GetAttribute("TargetType");

            TargetType outTargetType = Conditions.TargetType.Me;
            Enum.TryParse<TargetType>(targetType, out outTargetType);
            this.TargetType = outTargetType;

            this.MustBeReady = Boolean.Parse(mustBeReady);

            this.ClassType = Type.GetType(type);
            this.CreateInstance();

            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (this.Instance == null)
                return;

            Type abilityType = this.Instance.GetType();
            
            writer.WriteAttributeString("Type", abilityType.FullName);
            writer.WriteAttributeString("TargetType", this.TargetType.ToString());
            writer.WriteAttributeString("MustBeReady", this.MustBeReady.ToString());
        }

        #endregion
    }
}
