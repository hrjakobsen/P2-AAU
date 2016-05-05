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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
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

                pnlHelp2.Visible = true;
                pnlHelp2.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[2])
            {
                deselectAllOptionPanels();

                pnlHelp3.Visible = true;
                pnlHelp3.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[3])
            {
                deselectAllOptionPanels();

                pnlHelp4.Visible = true;
                pnlHelp4.Enabled = true;
            }
            else if (HelpBox.SelectedItem == HelpBox.Items[4])
            {
                deselectAllOptionPanels();

                pnlHelp5.Visible = true;
                pnlHelp5.Enabled = true;
            }
        }

        private void deselectAllOptionPanels()
        {
            pnlHelpGeneral.Visible = false;
            pnlHelpGeneral.Enabled = false;

            pnlHelp2.Visible = false;
            pnlHelp2.Enabled = false;

            pnlHelp3.Visible = false;
            pnlHelp3.Enabled = false;

            pnlHelp4.Visible = false;
            pnlHelp4.Enabled = false;

            pnlHelp5.Visible = false;
            pnlHelp5.Enabled = false;
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
    }


}
