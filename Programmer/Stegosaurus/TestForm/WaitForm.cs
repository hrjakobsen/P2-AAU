using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class WaitForm : Form
    {
        public static bool Done;

        public WaitForm()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.WaitForm_Shown);
        }

        private void centerLabel(Label label)
        {
            label.AutoSize = false;
            //label.TextAlign = ContentAlignment.MiddleCenter;
            //label.Dock = DockStyle.Fill;
        }

        private void WaitForm_Shown(object sender, EventArgs e)
        {
            string selectedMethod;
            if (StegosaurusForm.LSBMethodSelected)
            {
                selectedMethod = "Least Significant Bit";
            }
            else
            {
                selectedMethod = "Graph Theoretical";
            }
            centerLabel(lblWaitMessage);
            centerLabel(lblPleaseWait);
            lblWaitMessage.Text = "Encoding using the " + selectedMethod + " method.";
            Done = true;
        }
    }
}
