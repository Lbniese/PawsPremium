using Paws.Core.Conditions;
using Paws.Core.Conditions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Paws.Core
{
    public class ItemCondition : IXmlSerializable
    {
        private string _friendlyName;
        private List<PropertyInfo> _requiredProperties;

        /// <summary>
        /// Gets or sets the class type associated with this Item Condition.
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// Gets or sets the ICondition instance associated with this item condition.
        /// When this Instance is set, the condition is available for serialization.
        /// </summary>
        public ICondition Instance { get; set; }

        public ItemCondition()
        { }

        public ItemCondition(Type typeOfCondition)
        {
            this.ClassType = typeOfCondition;
        }

        /// <summary>
        /// (Lazy-Loaded) Gets or sets the friendly name of the Condition Type.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (string.IsNullOrEmpty(_friendlyName))
                {
                    var thisAttribute = (ItemConditionAttribute)this.ClassType.GetCustomAttributes(typeof(ItemConditionAttribute), false)
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
        /// (Lazy-Loaded) Gets or sets the required properties of the Condition Type.
        /// </summary>
        public List<PropertyInfo> RequiredProperties
        {
            get
            {
                if (_requiredProperties == null)
                {
                    this.RequiredProperties = this.ClassType.GetProperties()
                        .Where(o => Attribute.IsDefined(o, typeof(ItemConditionParameterAttribute)))
                        .ToList();
                }

                return _requiredProperties;
            }
            set
            {
                _requiredProperties = value;
            }
        }

        /// <summary>
        /// Creates an IConditon instance of the class type.
        /// </summary>
        public void CreateInstance()
        {
            this.Instance = (ICondition)Activator.CreateInstance(this.ClassType);
        }

        /// <summary>
        /// Sets the value to the property using the expected type of the property for the ICondition instance.
        /// </summary>
        public void SetPropertyValue(PropertyInfo property, object value)
        {
            if (this.Instance != null && property != null)
            {
                property.SetValue(this.Instance, Convert.ChangeType(value, property.PropertyType));
            }
        }

        public override string ToString()
        {
            if (this.Instance == null) return this.FriendlyName;

            var properties = this.Instance.GetType().GetProperties()
                .Where(o => Attribute.IsDefined(o, typeof(ItemConditionParameterAttribute)));

            string propertyString = string.Empty;

            if (properties != null && properties.Count() > 0)
            {
                propertyString += " {";
                foreach (var property in properties)
                {
                    propertyString += string.Format("{0}: {1}", property.Name, property.GetValue(this.Instance));

                    if (property != properties.Last()) propertyString += ", ";
                }
                propertyString += "}";
            }

            return this.FriendlyName + propertyString;
        }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null; // XmlSchema should almost always return null in custom xml serialization cases
        }

        public void ReadXml(XmlReader reader)
        {
            var type = reader.GetAttribute("Type");

            reader.Read();

            if (string.IsNullOrEmpty(type))
                return;

            this.ClassType = Type.GetType(type);

            XmlSerializer serializer = new XmlSerializer(this.ClassType);

            this.Instance = (ICondition)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (this.Instance == null)
                return;

            Type conditionType = this.Instance.GetType();
            XmlSerializer serializer = new XmlSerializer(conditionType);

            writer.WriteAttributeString("Type", conditionType.FullName);
            serializer.Serialize(writer, this.Instance);
        }

        #endregion
    }
}
