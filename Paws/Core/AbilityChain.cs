using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Styx;
using Styx.Common;

namespace Paws.Core
{
    public class AbilityChain : IXmlSerializable
    {
        public AbilityChain()
            : this("Not Named")
        {
        }

        public AbilityChain(string name)
        {
            Name = name;
            Specialization = WoWSpec.DruidFeral;
            ChainedAbilities = new List<ChainedAbility>();
            HotKey = Keys.None;
            ModiferKey = ModifierKeys.Alt;
        }

        /// <summary>
        ///     The list of chained abilities associated with this ability chain.
        /// </summary>
        public List<ChainedAbility> ChainedAbilities { get; set; }

        /// <summary>
        ///     The name of the ability chain (also used as the name of the registered hotkey action)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The specialization required to run this ability chain.
        /// </summary>
        public WoWSpec Specialization { get; set; }

        /// <summary>
        ///     The Key associated with this ability chain that must be pressed in combination with the ModifierKey (if present).
        /// </summary>
        public Keys HotKey { get; set; }

        /// <summary>
        ///     The Modifier Key associated with this ability chain that must be pressed in combination with the HotKey.
        /// </summary>
        public ModifierKeys ModiferKey { get; set; }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        ///     Since our abilities implement a behavior (interface) and not a state (base class), serialization must be done
        ///     manually.
        /// </summary>
        public void ReadXml(XmlReader reader)
        {
            var name = reader.GetAttribute("Name");
            var specialization = reader.GetAttribute("Specialization");
            var hotKey = reader.GetAttribute("Hotkey");
            var modifierKey = reader.GetAttribute("Modifier");

            reader.Read();

            Name = name;

            WoWSpec outSpec;
            Enum.TryParse(specialization, out outSpec);
            Specialization = outSpec;

            Keys outHotKey;
            Enum.TryParse(hotKey, out outHotKey);
            HotKey = outHotKey;

            ModifierKeys outModifierKey;
            Enum.TryParse(modifierKey, out outModifierKey);
            ModiferKey = outModifierKey;

            var serializer = new XmlSerializer(ChainedAbilities.GetType());

            ChainedAbilities = (List<ChainedAbility>) serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (ChainedAbilities == null)
                return;

            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Specialization", Specialization.ToString());
            writer.WriteAttributeString("Hotkey", HotKey.ToString());
            writer.WriteAttributeString("Modifier", ModiferKey.ToString());

            var serializer = new XmlSerializer(ChainedAbilities.GetType());
            serializer.Serialize(writer, ChainedAbilities);
        }

        #endregion
    }
}