using System;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Paws.Core.Abilities;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;

namespace Paws.Core
{
    public class ChainedAbility : IXmlSerializable
    {
        private string _friendlyName;

        public ChainedAbility()
        {
        }

        public ChainedAbility(IAbility ability, TargetType targetType, bool mustBeReady)
        {
            Instance = ability;
            TargetType = targetType;
            MustBeReady = mustBeReady;

            ClassType = Instance.GetType();
        }

        public ChainedAbility(Type typeOfAbility)
        {
            ClassType = typeOfAbility;
        }

        /// <summary>
        ///     Gets or sets the class type associated with this Ability.
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        ///     The ability must be off of any cooldowns if this flag is set.
        /// </summary>
        public bool MustBeReady { get; set; }

        /// <summary>
        ///     Determines who the target is that this ability will cast on.
        /// </summary>
        public TargetType TargetType { get; set; }

        /// <summary>
        ///     Gets or sets the ICondition instance associated with this item condition.
        ///     When this Instance is set, the condition is available for serialization.
        /// </summary>
        public IAbility Instance { get; set; }

        /// <summary>
        ///     (Lazy-Loaded) Gets or sets the friendly name of the Ability.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (string.IsNullOrEmpty(_friendlyName))
                {
                    var thisAttribute =
                        (AbilityChainAttribute) ClassType.GetCustomAttributes(typeof (AbilityChainAttribute), false)
                            .FirstOrDefault();

                    _friendlyName = thisAttribute != null ? thisAttribute.FriendlyName : "Unknown";
                }

                return _friendlyName;
            }
            set { _friendlyName = value; }
        }

        /// <summary>
        ///     Creates an IAbility instance of the class type.
        /// </summary>
        public void CreateInstance()
        {
            Instance = (IAbility) Activator.CreateInstance(ClassType);
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

            TargetType outTargetType;
            Enum.TryParse(targetType, out outTargetType);
            TargetType = outTargetType;

            MustBeReady = bool.Parse(mustBeReady);

            ClassType = Type.GetType(type);
            CreateInstance();

            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Instance == null)
                return;

            var abilityType = Instance.GetType();

            writer.WriteAttributeString("Type", abilityType.FullName);
            writer.WriteAttributeString("TargetType", TargetType.ToString());
            writer.WriteAttributeString("MustBeReady", MustBeReady.ToString());
        }

        #endregion
    }
}