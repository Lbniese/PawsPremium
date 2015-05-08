using Styx;
using Styx.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Paws.Core
{
    public class AbilityChain : IXmlSerializable
    {
        /// <summary>
        /// The list of chained abilities associated with this ability chain.
        /// </summary>
        public List<ChainedAbility> ChainedAbilities { get; set; }

        /// <summary>
        /// The name of the ability chain (also used as the name of the registered hotkey action)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The specialization required to run this ability chain.
        /// </summary>
        public WoWSpec Specialization { get; set; }

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

        public AbilityChain(string name)
        {
            this.Name = name;
            this.Specialization = WoWSpec.DruidFeral;
            this.ChainedAbilities = new List<ChainedAbility>();
            this.HotKey = Keys.None;
            this.ModiferKey = ModifierKeys.Alt;
        }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Since our abilities implement a behavior (interface) and not a state (base class), serialization must be done manually.
        /// </summary>
        public void ReadXml(XmlReader reader)
        {
            var name = reader.GetAttribute("Name");
            var specialization = reader.GetAttribute("Specialization");
            var hotKey = reader.GetAttribute("Hotkey");
            var modifierKey = reader.GetAttribute("Modifier");

            reader.Read();

            this.Name = name;

            WoWSpec outSpec = WoWSpec.DruidFeral;
            Enum.TryParse<WoWSpec>(specialization, out outSpec);
            this.Specialization = outSpec;

            Keys outHotKey = Keys.None;
            Enum.TryParse<Keys>(hotKey, out outHotKey);
            this.HotKey = outHotKey;

            ModifierKeys outModifierKey = ModifierKeys.NoRepeat;
            Enum.TryParse<ModifierKeys>(modifierKey, out outModifierKey);
            this.ModiferKey = outModifierKey;

            XmlSerializer serializer = new XmlSerializer(this.ChainedAbilities.GetType());

            this.ChainedAbilities= (List<ChainedAbility>)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (this.ChainedAbilities == null)
                return;

            writer.WriteAttributeString("Name", this.Name);
            writer.WriteAttributeString("Specialization", this.Specialization.ToString());
            writer.WriteAttributeString("Hotkey", this.HotKey.ToString());
            writer.WriteAttributeString("Modifier", this.ModiferKey.ToString());
            
            XmlSerializer serializer = new XmlSerializer(this.ChainedAbilities.GetType());
            serializer.Serialize(writer, this.ChainedAbilities);
        }

        #endregion
    }
}
