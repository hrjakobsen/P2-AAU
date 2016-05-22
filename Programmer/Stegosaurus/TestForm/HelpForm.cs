using System;
using System.Windows.Forms;

namespace TestForm
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            HelpBox.SelectedItem = HelpBox.Items[0];
        }

        private void HelpBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HelpBox.SelectedItem == HelpBox.Items[0])
            {
                deselectAllOptionPanels();

                pnlHelpGeneral.Visible = true;
                pnlHelpGeneral.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[1])
            {
                deselectAllOptionPanels();

                pnlQuality.Visible = true;
                pnlQuality.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[2])
            {
                deselectAllOptionPanels();

                pnlHelpEncodingMethod.Visible = true;
                pnlHelpEncodingMethod.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[3])
            {
                deselectAllOptionPanels();

                pnlHelpHuffman.Visible = true;
                pnlHelpHuffman.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[4])
            {
                deselectAllOptionPanels();

                pnlHelpQuantization.Visible = true;
                pnlHelpQuantization.Enabled = true;
            }
        }

        private void deselectAllOptionPanels()
        {
            pnlHelpGeneral.Visible = false;
            pnlHelpGeneral.Enabled = false;

            pnlQuality.Visible = false;
            pnlQuality.Enabled = false;

            pnlHelpEncodingMethod.Visible = false;
            pnlHelpEncodingMethod.Enabled = false;

            pnlHelpHuffman.Visible = false;
            pnlHelpHuffman.Enabled = false;

            pnlHelpQuantization.Visible = false;
            pnlHelpQuantization.Enabled = false;
        }

        //'Escape' closes form
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Okay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grpHelpGeneral_Enter(object sender, EventArgs e) {

        }

        private void grpHelp5_Enter(object sender, EventArgs e)
        {

        }
    }


}
