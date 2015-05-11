using System;
using System.Windows.Forms;
using System.Linq;

namespace Paws.Interface
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
            var useableItems = Styx.StyxWoW.Me.BagItems
                .Where(o => o.Usable)
                .Select(o => new ItemSelectionEntry()
                {
                    Entry = o.Entry,
                    Name = o.Name
                })
                .Distinct()
                .OrderBy(o => o);

            foreach (var carriedItem in useableItems)
            {
                this.carriedItemsComboBox.Items.Add(carriedItem);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }

    public class ItemSelectionEntry : IComparable<ItemSelectionEntry>
    {
        public uint Entry { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public int CompareTo(ItemSelectionEntry that)
        {
            return this.Entry.CompareTo(that.Entry);
        }
    }
}
