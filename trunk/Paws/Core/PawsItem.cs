using Paws.Core.Conditions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Paws.Core.Utilities;

namespace Paws.Core
{
    public class PawsItem : IXmlSerializable
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The entry id of the item (Required for locale independency)
        /// </summary>
        public int Entry { get; set; }

        /// <summary>
        /// Enables or disables the use of the item.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the conditions that must be satisfied prior to use.
        /// </summary>
        public List<ItemCondition> Conditions { get; set; }

        /// <summary>
        /// Gets or sets the state the player must be in to use the item.
        /// </summary>
        public MyState MyState { get; set; }

        public PawsItem()
        {
            this.Conditions = new List<ItemCondition>();
        }

        public string GetConditionsDescription()
        {
            var description = string.Empty;

            foreach (var condition in this.Conditions)
            {
                description += condition.ToString();
                if (condition != this.Conditions.Last()) description += "; ";
            }

            if (this.Conditions.Count == 0) description = "Off Item Cooldown";

            return description;
        }

        #region IXmlSerializable

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null; // XmlSchema should almost always return null in custom xml serialization cases
        }

        /// <summary>
        /// Since our conditions implement a behavior (interface) and not a state (base class), serialization must be done manually.
        /// </summary>
        public void ReadXml(XmlReader reader)
        {
            var name = reader.GetAttribute("Name");
            var entry = reader.GetAttribute("Entry");
            var myState = reader.GetAttribute("MyState");
            var enabled = reader.GetAttribute("Enabled");

            reader.Read();

            this.Name = name;

            int outEntry = -1;
            if (!int.TryParse(entry, out outEntry))
            {
                Log.GUI(string.Format("WARNING: Unable to load item \"{0}\" because the entry id is missing. This is due to having an older version of the Paws-Items.xml settings file.  Please either delete the Paws-Items.xml file under your settings folder, or re-add items using the Items tab in the Paws user interface.", name));
                return;
            }
            this.Entry = outEntry;

            MyState outMyState = Core.MyState.NotInCombat;

            Enum.TryParse<MyState>(myState, out outMyState);

            this.MyState = outMyState;
            this.Enabled = bool.Parse(enabled);

            XmlSerializer serializer = new XmlSerializer(this.Conditions.GetType());

            this.Conditions = (List<ItemCondition>)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        /// <summary>
        /// Since our conditions implement a behavior (interface) and not a state (base class), serialization must be done manually.
        /// </summary>
        public void WriteXml(XmlWriter writer)
        {
            if (this.Conditions == null)
                return;

            writer.WriteAttributeString("Name", this.Name);
            writer.WriteAttributeString("Entry", this.Entry.ToString());
            writer.WriteAttributeString("MyState", this.MyState.ToString());
            writer.WriteAttributeString("Enabled", this.Enabled.ToString());
            XmlSerializer serializer = new XmlSerializer(this.Conditions.GetType());
            serializer.Serialize(writer, this.Conditions);
        }

        #endregion
    }
}
