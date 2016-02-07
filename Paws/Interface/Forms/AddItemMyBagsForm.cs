using System;
using System.Linq;
using System.Windows.Forms;
using Styx;

namespace Paws.Interface.Forms
{
    public partial class AddItemMyBagsForm : Form
    {
        public AddItemMyBagsForm()
        {
            InitializeComponent();
        }

        private void AddItemMyBagsForm_Load(object sender, EventArgs e)
        {
            // load the items from my bags...
            var useableItems = StyxWoW.Me.BagItems
                .Where(o => o.Usable)
                .Select(o => new ItemSelectionEntry
                {
                    Entry = o.Entry,
                    Name = o.Name
                })
                .Distinct()
                .OrderBy(o => o);

            foreach (var carriedItem in useableItems)
            {
                carriedItemsComboBox.Items.Add(carriedItem);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }

    public class ItemSelectionEntry : IComparable<ItemSelectionEntry>
    {
        public uint Entry { get; set; }
        public string Name { get; set; }

        public int CompareTo(ItemSelectionEntry that)
        {
            return Entry.CompareTo(that.Entry);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}