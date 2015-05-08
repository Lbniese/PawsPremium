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
                .Select(o => o.Name)
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
}
