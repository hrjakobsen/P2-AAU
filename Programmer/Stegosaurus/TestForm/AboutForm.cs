using System;
using System.Windows.Forms;

namespace TestForm {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AboutForm_Load(object sender, EventArgs e) {

        }

        //'Escape' closes form
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape || keyData == Keys.Enter) {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
