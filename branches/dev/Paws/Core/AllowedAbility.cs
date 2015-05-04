﻿using Paws.Core.Abilities;
using Paws.Core.Abilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paws.Core
{
    public class AllowedAbility : IXmlSerializable
    {
        private string _friendlyName;

        /// <summary>
        /// Gets or sets the class type associated with this Ability.
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// Gets or sets the ICondition instance associated with this item condition.
        /// When this Instance is set, the condition is available for serialization.
        /// </summary>
        public IAbility Instance { get; set; }

        public AllowedAbility()
        { }

        public AllowedAbility(Type typeOfAbility)
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
        /// Creates an IConditon instance of the class type.
        /// </summary>
        public void CreateInstance()
        {
            this.Instance = (IAbility)Activator.CreateInstance(this.ClassType);
        }

        #region IXmlSerializable

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
