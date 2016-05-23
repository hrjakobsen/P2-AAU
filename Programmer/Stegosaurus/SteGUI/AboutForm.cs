using System;
using System.Windows.Forms;

namespace SteGUI {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e) {

        }

        //'Escape' closes form
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape || keyData == Keys.Enter) {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
