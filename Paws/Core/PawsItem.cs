using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Paws.Core.Utilities;

namespace Paws.Core
{
    public class PawsItem : IXmlSerializable
    {
        public PawsItem()
        {
            Conditions = new List<ItemCondition>();
        }

        /// <summary>
        ///     Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The entry id of the item (Required for locale independency)
        /// </summary>
        public int Entry { get; set; }

        /// <summary>
        ///     Enables or disables the use of the item.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets or sets the conditions that must be satisfied prior to use.
        /// </summary>
        public List<ItemCondition> Conditions { get; set; }

        /// <summary>
        ///     Gets or sets the state the player must be in to use the item.
        /// </summary>
        public MyState MyState { get; set; }

        public string GetConditionsDescription()
        {
            var description = string.Empty;

            foreach (var condition in Conditions)
            {
                description += condition.ToString();
                if (condition != Conditions.Last()) description += "; ";
            }

            if (Conditions.Count == 0) description = "Off Item Cooldown";

            return description;
        }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null; // XmlSchema should almost always return null in custom xml serialization cases
        }

        /// <summary>
        ///     Since our conditions implement a behavior (interface) and not a state (base class), serialization must be done
        ///     manually.
        /// </summary>
        public void ReadXml(XmlReader reader)
        {
            var name = reader.GetAttribute("Name");
            var entry = reader.GetAttribute("Entry");
            var myState = reader.GetAttribute("MyState");
            var enabled = reader.GetAttribute("Enabled");

            reader.Read();

            Name = name;

            int outEntry;
            if (!int.TryParse(entry, out outEntry))
            {
                Log.Gui(
                    string.Format(
                        "WARNING: Unable to load item \"{0}\" because the entry id is missing. This is due to having an older version of the Paws-Items.xml settings file.  Please either delete the Paws-Items.xml file under your settings folder, or re-add items using the Items tab in the Paws user interface.",
                        name));
                return;
            }
            Entry = outEntry;

            MyState outMyState;

            Enum.TryParse(myState, out outMyState);

            MyState = outMyState;
            Enabled = bool.Parse(enabled);

            var serializer = new XmlSerializer(Conditions.GetType());

            Conditions = (List<ItemCondition>) serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        /// <summary>
        ///     Since our conditions implement a behavior (interface) and not a state (base class), serialization must be done
        ///     manually.
        /// </summary>
        public void WriteXml(XmlWriter writer)
        {
            if (Conditions == null)
                return;

            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Entry", Entry.ToString());
            writer.WriteAttributeString("MyState", MyState.ToString());
            writer.WriteAttributeString("Enabled", Enabled.ToString());
            var serializer = new XmlSerializer(Conditions.GetType());
            serializer.Serialize(writer, Conditions);
        }

        #endregion
    }
}