using System;
using System.Windows.Forms;

namespace SteGUI {
    public partial class HelpForm : Form {
        public HelpForm() {
            InitializeComponent();
            HelpBox.SelectedItem = HelpBox.Items[0];
        }

        private void HelpBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (HelpBox.SelectedItem == HelpBox.Items[0]) {
                _deselectAllOptionPanels();

                pnlHelpGeneral.Visible = true;
                pnlHelpGeneral.Enabled = true;
            } else if (HelpBox.SelectedItem == HelpBox.Items[1]) {
                _deselectAllOptionPanels();

                pnlQuality.Visible = true;
                pnlQuality.Enabled = true;
            } else if (HelpBox.SelectedItem == HelpBox.Items[2]) {
                _deselectAllOptionPanels();

                pnlHelpEncodingMethod.Visible = true;
                pnlHelpEncodingMethod.Enabled = true;
            } else if (HelpBox.SelectedItem == HelpBox.Items[3]) {
                _deselectAllOptionPanels();

                pnlHelpHuffman.Visible = true;
                pnlHelpHuffman.Enabled = true;
            } else if (HelpBox.SelectedItem == HelpBox.Items[4]) {
                _deselectAllOptionPanels();

                pnlHelpQuantization.Visible = true;
                pnlHelpQuantization.Enabled = true;
            }
        }

        private void _deselectAllOptionPanels() {
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape) {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Okay_Click(object sender, EventArgs e) {
            Close();
        }
    }


}
